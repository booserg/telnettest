using Prism.Commands;
using System;
using System.ComponentModel;
using TelnetClientApp.Models;

namespace TelnetClientApp.ViewModels
{
    public class ConnectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand StartCommand { get; private set; }
        public DelegateCommand StopCommand { get; private set; }

        public TelnetClient Client { get; private set; }

        public string Status
        {
            get
            {
                if (Client.IsWorking)
                    return "Connected";
                else
                    return "Disconnected";
            }
        }

        public ConnectionViewModel(TelnetClient client)
        {
            Client = client;

            StartCommand = new DelegateCommand(() => 
            {
                Client.Connect();
                StartCommand.RaiseCanExecuteChanged();
                StopCommand.RaiseCanExecuteChanged();
            },
            () => !Client.IsWorking);

            StopCommand = new DelegateCommand(() =>
            {
                Client.Disconnect();
                StartCommand.RaiseCanExecuteChanged();
                StopCommand.RaiseCanExecuteChanged();
            },
            () => Client.IsWorking);

            Client.ClientConnected += Client_ConnectedChanged;
            Client.ClientDisconnected += Client_ConnectedChanged;
        }

        private void Client_ConnectedChanged(object sender, EventArgs e)
        {
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
        }
    }
}
