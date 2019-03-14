using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TelnetServerApp
{
    public class TelnetServer : INotifyPropertyChanged
    {
        private TcpListener server;

        private bool isWorking;
        public bool  IsWorking
        {
            get { return isWorking; }
            private set
            {
                isWorking = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsWorking"));
            }
        }

        private int connectionQty;

        public int ConnectionQuantity
        {
            get { return connectionQty; }
            set { connectionQty = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public async Task Start(string ip, int port)
        {
            var ipAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(ipAddr, port);
            server.Start();

            IsWorking = true;
            while (IsWorking)
            {
                try
                {
                    var client = await server.AcceptTcpClientAsync();
                    var telnetSession = new TelnetSession(client);
                    telnetSession.Start();
                }
                catch(Exception exc)
                {

                }
            }
        }

        public void Stop()
        {
            IsWorking = false;
            if (server != null)
            {
                try
                {
                    server.Stop();
                }
                catch(Exception exc)
                {

                }
            }
        }
    }
}
