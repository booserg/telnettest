using Common.Interfaces;
using TelnetClientApp.Models;
using TelnetClientApp.ViewModels;

namespace TelnetClientApp
{
    public class MainWindowViewModel
    {
        public ConnectionViewModel ConnectionViewModel { get; set; }

        public ValueViewModel ValueViewModel { get; set; }

        public MainWindowViewModel(IValueUpdatable obj, TelnetClient client)
        {
            ConnectionViewModel = new ConnectionViewModel(client);

            ValueViewModel = new ValueViewModel(obj, client);
        }
    }
}
