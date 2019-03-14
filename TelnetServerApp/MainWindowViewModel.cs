using Prism.Commands;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TelnetServerApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Start { get; private set; }

        public ICommand Stop { get; private set; }

        private TelnetServer server;

        public MainWindowViewModel()
        {
            server = new TelnetServer();

            Start = new DelegateCommand(async () => { await startServer(); });
            Stop = new DelegateCommand(() => { server.Stop(); });
        }

        private async Task startServer()
        {
            await server.Start("127.0.0.1", 2323);
        }
    }
}
