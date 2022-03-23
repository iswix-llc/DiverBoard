using System;
using System.Windows;
using System.Windows.Controls;
using DiverBoard.Models;
using DiverBoard.Services;
using DiverBoard.Enums;
using System.Media;

namespace DiverBoard.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            dynamic dc = button.DataContext;
            Bunk bunk = dc.Value;
            tripViewModel.ProcessButtonClick(bunk, false);
        }


        private void WrapPanel_Loaded(object sender, RoutedEventArgs e)
        {
            DiverBoard.Services.NavigationService navigationService = DiverBoard.Services.NavigationService.Instance;
            if (navigationService.Command != null)
            {
                tripViewModel.ProcessCommand(navigationService.Command);
                navigationService.Command = null;
                //if(navigationService.DiveMode==DiveMode.Splash)
                //{
                //    tripViewModel.Splash();
                //}
                //else if(navigationService.DiveMode==DiveMode.Climb)
                //{
                //    tripViewModel.Climb();
                //}
            }

        }
    }
}