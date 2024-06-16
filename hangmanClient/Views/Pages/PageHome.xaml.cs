using Domain;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageHome : Page
    {
        DTOPlayer activePlayer;

        public PageHome(DTOPlayer activePlayer)
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
            var menuPage = new FrameMenu(frHome, activePlayer);
            frMenu.Navigate(menuPage);
            frMenu.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;
        }

        private void BtnClicHideMenu(object sender, RoutedEventArgs e)
        {
            frMenu.Visibility = Visibility.Hidden;
            btnExit.Visibility = Visibility.Hidden;
        }

        private void BtnClickJoinGameWithCode(object sender, RoutedEventArgs e)
        {
            var joinGame = new PageAccessCode(activePlayer, frHome);
            frHome.Navigate(joinGame);
        }

        private void BtnClickCreateGame(object sender, RoutedEventArgs e)
        {
            var pageSelectWord = new PageSelectWord(activePlayer, frHome);
            frHome.Navigate(pageSelectWord);
        }
    }
}
