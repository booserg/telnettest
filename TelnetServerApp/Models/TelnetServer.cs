using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TelnetServerApp.Models
{
    public class TelnetServer
    {
        private TcpListener server;

        public event EventHandler StatusChanged;
        private bool isWorking;
        public bool  IsWorking
        {
            get
            {
                return isWorking;
            }
            private set
            {
                isWorking = value;
                StatusChanged?.Invoke(this, new EventArgs());
            }
        }

        List<TelnetSession> sessions = new List<TelnetSession>();
        public event EventHandler ConnectionQuantityChanged;
        public int ConnectionQuantity
        {
            get
            {
                return sessions.Count;
            }
        }
        
        private IPAddress ip;
        public string IP
        {
            get
            {
                return ip.ToString();
            }
            set
            {
                ip = IPAddress.Parse(value);
            }
        }

        public TelnetServer()
        {
            IP = "127.0.0.1";
        }

        public async Task Start(IValueUpdatable objToUpdate)
        {
            if (objToUpdate == null)
                throw new ArgumentNullException();

            server = new TcpListener(ip, 2323);
            server.Start();

            IsWorking = true;
            while (IsWorking)
            {
                try
                {
                    var client = await server.AcceptTcpClientAsync();
                    var telnetSession = new TelnetSession(client);
                    sessions.Add(telnetSession);
                    ConnectionQuantityChanged?.Invoke(this, new EventArgs());
                    telnetSession.ConnectionClosed += TelnetSession_ConnectionClosed;
                    telnetSession.Start(objToUpdate);
                }
                catch
                {
                    IsWorking = false;
                }
            }
        }

        private void TelnetSession_ConnectionClosed(object sender, EventArgs e)
        {
            sessions.Remove(sender as TelnetSession);
            ConnectionQuantityChanged?.Invoke(this, new EventArgs());
        }

        public void Stop()
        {
            IsWorking = false;
            if (server != null)
            {
                try
                {
                    foreach (var session in sessions)
                        session.Stop();

                    server.Stop();
                }
                catch
                {
                    IsWorking = false;
                }
            }
        }
    }
}
