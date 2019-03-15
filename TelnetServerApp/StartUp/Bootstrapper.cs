using Common;
using Common.Interfaces;
using TelnetServerApp.Models;
using TelnetServerApp.ViewModels;

namespace TelnetServerApp.StartUp
{
    public class Bootstrapper
    {
        public TelnetServer Server;

        public MainWindowViewModel MainViewModel;

        public IValueUpdatable ValueObject;

        public void Constract()
        {
            this.Server = new TelnetServer();

            this.ValueObject = new ValueObject();

            this.MainViewModel = new MainWindowViewModel(Server, ValueObject);
        }
    }
}
