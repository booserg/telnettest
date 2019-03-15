using Common.Interfaces;
using Prism.Commands;
using System.ComponentModel;
using TelnetClientApp.Models;

namespace TelnetClientApp.ViewModels
{
    public class ValueViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand SendValueCommand { get; set; }

        public ValueViewModel(IValueUpdatable obj, TelnetClient client)
        {
            ValueObject = obj;

            Client = client;
            Client.ClientConnected += Client_ClientConnectedStateChanged;
            Client.ClientDisconnected += Client_ClientConnectedStateChanged;

            SendValueCommand = new DelegateCommand(()=> { Client.SendValue(); }, () => Client.IsWorking);
        }

        private void Client_ClientConnectedStateChanged(object sender, System.EventArgs e)
        {
            SendValueCommand.RaiseCanExecuteChanged();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsWorking"));
        }

        public bool IsWorking
        {
            get
            {
                return Client.IsWorking;
            }
        }

        public IValueUpdatable ValueObject { get; private set; }

        public TelnetClient Client { get; private set; }
    }
}
