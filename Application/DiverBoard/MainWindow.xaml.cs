using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DiverBoard.Services;
using DiverBoard.Views;
namespace DiverBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string BEGINTEXT = "<";
        const string ENDTEXT = ">";
        StringBuilder _command;

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(string.Equals(e.Text, BEGINTEXT, StringComparison.InvariantCultureIgnoreCase))
            {
                _command = new StringBuilder();
            }
            else if(_command != null)
            {
                if(string.Equals(e.Text, ENDTEXT, StringComparison.InvariantCultureIgnoreCase))
                {
                    NavigationService.Instance.Command = _command.ToString().ToUpper();
                    MainFrame.Navigate(new MainPage());
                    _command = null;
                }
                else
                {
                    _command.Append(e.Text);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            //this.Height = 800;
            //this.Width = 1280;
            ////this.Height = 1280;
            ////this.Width = 800;
            //this.WindowState = WindowState.Normal;
            //this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
#endif

            NavigationService.Instance.MainFrame = MainFrame;
            if (DataService.Instance.Trip == null)
            {
                MainFrame.Navigate(new TripDatePage());
            }
            else
            {
                MainFrame.Navigate(new MainPage());
            }
        }
    }
}
