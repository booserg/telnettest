using Common.Interfaces;
using Prism.Commands;
using System;
using System.ComponentModel;
using TelnetServerApp.Models;

namespace TelnetServerApp.ViewModels
{
    public class ConnectionViewModel : INotifyPropertyChanged
    {
        private TelnetServer server { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Status
        {
            get
            {
                if (server.IsWorking)
                    return "Connected";
                else
                    return "Disconnected";
            }
        }

        public int ConnectionQuantity
        {
            get
            {
                return server.ConnectionQuantity;
            }
        }

        public DelegateCommand StartCommand { get; private set; }

        public DelegateCommand StopCommand { get; private set; }


        public ConnectionViewModel(TelnetServer server, IValueUpdatable objToUpdate)
        {
            if (server == null)
                throw new ArgumentNullException();

            this.server = server;

            this.server.StatusChanged += Server_StatusChanged;
            this.server.ConnectionQuantityChanged += Server_ConnectionQuantityChanged;

            StartCommand = new DelegateCommand(async () => { await this.server.Start(objToUpdate); }, () => !server.IsWorking);
            StopCommand = new DelegateCommand(() => { this.server.Stop(); }, () => server.IsWorking);
        }

        private void Server_ConnectionQuantityChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConnectionQuantity"));
        }

        private void Server_StatusChanged(object sender, EventArgs e)
        {
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
        }
    }
}
