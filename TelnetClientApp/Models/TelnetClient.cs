using Common.Interfaces;
using System;
using System.Net.Sockets;
using System.Text;

namespace TelnetClientApp.Models
{
    public class TelnetClient
    {
        private IValueUpdatable obj;

        public event EventHandler ClientConnected;
        public event EventHandler ClientDisconnected;

        private TcpClient client;
        private NetworkStream nwStream;

        public TelnetClient(IValueUpdatable obj)
        {
            this.obj = obj;
        }

        public bool IsWorking { get; private set; }

        public void Connect()
        {
            client = new TcpClient();
            try
            {
                client.Connect("127.0.0.1", 2323);
                nwStream = client.GetStream();

                IsWorking = true;
                ClientConnected?.Invoke(this, new EventArgs());
            }
            catch
            {
                IsWorking = false;
                ClientDisconnected?.Invoke(this, new EventArgs());
            }
        }

        public void Disconnect()
        {
            if (client != null && client.Connected)
            {
                client.Close();
            }

            IsWorking = false;
            ClientDisconnected?.Invoke(this, new EventArgs());
        }

        public void SendValue()
        {
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(obj.Value);
            try
            {
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch(Exception exc)
            {
                Disconnect();
            }
        }
    }
}
