using Domain;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Views.Pages
{
    
    public partial class Menu : Page
    {
        private Frame homeFrame;
        private DTOPlayer playerActive;
        public Menu(Frame frHome, DTOPlayer playerActive)
        {
            InitializeComponent();
            homeFrame = frHome;
            this.playerActive = playerActive;
            Console.WriteLine("MENU------------------");
            Console.WriteLine("ID JUGADOR" + playerActive.IdPlayer);
        }

        private void BtnClicHideMenu(object sender, RoutedEventArgs e)
        {
            var parentFrame = this.Parent as Frame;
            if (parentFrame != null)
            {
                parentFrame.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickEditProfile(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("EDITAR");
            Console.WriteLine("ID JUGADOR" + playerActive.IdPlayer);
            var registerPage = new PageCreateProfile(playerActive);
            homeFrame.Navigate(registerPage);
        }

        private void ClickViewStatistics(object sender, RoutedEventArgs e)
        {
            var statistics = new PageStatistics();
            homeFrame.Navigate(statistics);
        }

        private void ClickChangePassword(object sender, RoutedEventArgs e)
        {

        }

        private void ClickChangeLanguage(object sender, RoutedEventArgs e)
        {

        }

        private void ClickSignOut(object sender, RoutedEventArgs e)
        {

        }
    }
}
