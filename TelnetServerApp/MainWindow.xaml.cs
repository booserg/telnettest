using System.Windows;
using TelnetServerApp.StartUp;

namespace TelnetServerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Bootstrapper creator = new Bootstrapper();
            creator.Constract();
            DataContext = creator.MainViewModel;
        }
    }
}
