using Domain;
using System.Windows;
using System.Windows.Controls;
using ServerManageGame;

namespace Views.Pages
{

    public partial class PageWaitingRoom : Page
    {
        DTOPlayer activePlayer;
        private ManageGame manageGame;
        Frame frCurrentFrame;

        public PageWaitingRoom(DTOPlayer activePlayer, ManageGame manageGame, Frame homeFrame)
        {
            this.activePlayer = activePlayer;
            this.frCurrentFrame = homeFrame;
            InitializeComponent();

            this.manageGame = manageGame;
            this.manageGame.PlayerJoined += OnPlayerJoined;  // Subscribirse al evento
            this.manageGame.PlayerDisconnected += OnPlayerDisconnected;
            btnStartGame.Visibility = Visibility.Collapsed;


            if (manageGame.gameMatch.idGuesser > 0)
            {
                lbGuestPlayer.Visibility = Visibility.Collapsed;
                lbTitle.Content = "Estás a punto de jugar con " + manageGame.gameMatch.idChallenger;
                btnCancel.Content = "Salir";
                
            }
            else
            {
                lbTitle.Content = "Código de la partida";
                lbAccessCode.Content = manageGame.gameMatch.code;
                lbGuestPlayer.Visibility = Visibility.Visible;
                btnCancel.Content = "Cancelar";
            }
        }

        private void OnPlayerJoined(string namePlayerGuesser)
        {
            lbGuestPlayer.Content = $"{namePlayerGuesser} se ha unido a la partida";
            lbGuestPlayer.Visibility = Visibility.Visible;
            btnStartGame.Visibility = Visibility.Visible;
        }

        private void OnPlayerDisconnected(string namePlayerGuesser)
        {
            lbGuestPlayer.Content = $"{namePlayerGuesser} se ha desconectado de la partida";
            lbGuestPlayer.Visibility = Visibility.Visible;
            btnStartGame.Visibility = Visibility.Collapsed;
        }

        private void BtnClickOutGame(object sender, RoutedEventArgs e)
        {
            manageGame.FinishGameConnectionn();
            var pageHome = new PageHome(activePlayer);
            frCurrentFrame.Navigate(pageHome);
        }

        private void BtnStartManageGame(object sender, RoutedEventArgs e)
        {
            manageGame.StartGame();
        }
    }

}
