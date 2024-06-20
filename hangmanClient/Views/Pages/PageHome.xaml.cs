using Domain;
using ServerManageGame;
using System.Windows;
using System.Windows.Controls;
using Views.SRIManageGameService;


namespace Views.Pages
{
    public partial class PageHome : Page
    {
        Domain.DTOPlayer activePlayer;
        ManageGame manageGame;
        DataGrid datagridGames;
        private Gamematch gamematch = new Gamematch();
        string language;

        public PageHome(Domain.DTOPlayer activePlayer, string language)
        {
            InitializeComponent();
            this.activePlayer = activePlayer;
            manageGame = new ManageGame(activePlayer, frHome, language);
            ShowPlayerGreeting();
            LoadGames();
            datagridGames = dataGridItemsGames;
            this.language = language;
        }

        public void ShowPlayerGreeting()
        {
            lbPlayerGreeting.Content = "Bienvenido " + activePlayer.Name;
        }

        private void BtnClicShowMenu(object sender, RoutedEventArgs e)
        {
            var menuPage = new FrameMenu(frHome, activePlayer, language);
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
            var pageSelectWord = new PageSelectWord(activePlayer, frHome, language);
            frHome.Navigate(pageSelectWord);
        }

        public void LoadGames()
        {
            var games = manageGame.RecoveringGames();
            dataGridItemsGames.ItemsSource = games;
        }

        private void BtnJoinGame(object sender, RoutedEventArgs e)
        {
            if (dataGridItemsGames.SelectedItem != null)
            {
                var selectedGame = (SRIManageGameService.DTOGameMatch)dataGridItemsGames.SelectedItem;

                int gameId = selectedGame.idGamematch;
                gamematch.idGuesser = activePlayer.IdPlayer;
                manageGame.StartJoinGame(gamematch, false);
            }
            
        }
    }
}
