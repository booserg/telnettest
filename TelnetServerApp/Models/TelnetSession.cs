using Common.Interfaces;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TelnetServerApp.Models
{
    public class TelnetSession
    {
        public TcpClient Client { get; private set; }

        public TelnetSession(TcpClient client)
        {
            this.Client = client;
        }

        public void Start(IValueUpdatable objToUpdate)
        {
            var task = new Task(() =>
            {
                var stream = Client.GetStream();

                int bytesCnt = 0;
                var rawData = new byte[256];
                var isDone = false;

                try
                {
                    while ((bytesCnt = stream.Read(rawData, 0, rawData.Length)) != 0 && !isDone)
                    {
                        string data = Encoding.ASCII.GetString(rawData);
                        data = data.Substring(0, bytesCnt);

                        if (data != "\r\n")
                            objToUpdate.Value = data;

                        if (data == "exit")
                        {
                            isDone = true;
                        }
                        else
                        {
                            var answer = Encoding.ASCII.GetBytes(data.ToUpper());
                            stream.Write(answer, 0, answer.Length);
                        }
                    }

                    Client.Close();
                }
                catch
                {
                    ConnectionClosed?.Invoke(this, new EventArgs());
                }
            });

            task.Start();
        }

        public void Stop()
        {
            Client.Close();
        }

        public event EventHandler ConnectionClosed;
    }
}
