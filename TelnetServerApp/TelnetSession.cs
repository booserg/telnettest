using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TelnetServerApp
{
    public class TelnetSession
    {
        private TcpClient client;

        public TelnetSession(TcpClient client)
        {
            this.client = client;
        }

        public void Start()
        {
            var task = new Task(() =>
            {
                var stream = client.GetStream();

                int bytesCnt = 0;
                var rawData = new byte[256];
                var isDone = false;

                var isInitData = true;
                while ((bytesCnt = stream.Read(rawData, 0, rawData.Length)) != 0 && !isDone)
                {
                    if (!isInitData)
                    {
                        string data = Encoding.ASCII.GetString(rawData);
                        data = data.Substring(0, bytesCnt);

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
                    else
                        isInitData = false;
                }

                client.Close();
            });

            task.Start();
        }
    }
}
