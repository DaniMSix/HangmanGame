using Domain;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class Home : Page
    {
        DTOPlayer activePlayer;

        public Home(DTOPlayer activePlayer)
        {
            InitializeComponent();
            this.activePlayer = activePlayer;
            ShowPlayerGreeting();
        }

        public void ShowPlayerGreeting()
        {
            lbPlayerGreeting.Content = "Bienvenido " + activePlayer.Name;
        }

        private void BtnClicShowMenu(object sender, RoutedEventArgs e)
        {
            var menuPage = new Menu();
            frMenu.Navigate(menuPage);
            frMenu.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;
        }

        private void BtnClicHideMenu(object sender, RoutedEventArgs e)
        {
            frMenu.Visibility = Visibility.Hidden;
            btnExit.Visibility = Visibility.Hidden;
        }
    }
}
