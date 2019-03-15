using Common;
using TelnetClientApp.Models;

namespace TelnetClientApp.StartUp
{
    public class Bootstrapper
    {
        public TelnetClient Client { get; private set; }

        public MainWindowViewModel MainWindowViewModel { get; private set; }

        public void Construct()
        {
            ValueObject obj = new ValueObject();

            Client = new TelnetClient(obj);

            MainWindowViewModel = new MainWindowViewModel(obj, Client);
        }
    }
}
