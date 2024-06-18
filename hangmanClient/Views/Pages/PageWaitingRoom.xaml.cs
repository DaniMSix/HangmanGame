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



            if (manageGame.gameMatch.idGuesser > 0)
            {
                lbGuestPlayer.Visibility = Visibility.Collapsed;
                lbTitle.Content = "Estás a punto de jugar con " + manageGame.gameMatch.idChallenger;
                btnStartGame.Content = "Salir";
            }
            else
            {
                lbTitle.Content = "Código de la partida";
                lbAccessCode.Content = manageGame.gameMatch.code;
                lbGuestPlayer.Visibility = Visibility.Visible;
                btnStartGame.Content = "Cancelar";
            }
        }

        private void OnPlayerJoined(string namePlayerGuesser)
        {
            lbGuestPlayer.Content = $"{namePlayerGuesser} se ha unido a la partida";
            lbGuestPlayer.Visibility = Visibility.Visible;
            btnStartGame.Content = "Empezar";
        }

        private void OnPlayerDisconnected(string namePlayerGuesser)
        {
            lbGuestPlayer.Content = $"{namePlayerGuesser} se ha desconectado de la partida";
            lbGuestPlayer.Visibility = Visibility.Visible;
            btnStartGame.Content = "Cancelar";
        }

        private void BtnClickManageGame(object sender, RoutedEventArgs e)
        {
            manageGame.FinishGameConnectionn();
            var pageHome = new PageHome(activePlayer);
            frCurrentFrame.Navigate(pageHome);
        }
    }

}
