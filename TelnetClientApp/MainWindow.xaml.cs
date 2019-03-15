using System.Windows;
using TelnetClientApp.StartUp;

namespace TelnetClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Construct();

            DataContext = bootstrapper.MainWindowViewModel;
        }
    }
}
