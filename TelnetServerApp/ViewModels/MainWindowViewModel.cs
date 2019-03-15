using Common.Interfaces;
using System.ComponentModel;
using TelnetServerApp.Models;

namespace TelnetServerApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ValueViewModel ValueViewModel { get; set; }

        public ConnectionViewModel ConnectionViewModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel(TelnetServer server, IValueUpdatable obj)
        {
            ConnectionViewModel = new ConnectionViewModel(server, obj);

            ValueViewModel = new ValueViewModel(obj);
        }
    }
}
