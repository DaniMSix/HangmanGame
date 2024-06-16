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

            Console.WriteLine("------------------ PageWaitingRoom -------------------");
            Console.WriteLine("idGuesser" + manageGame?.gameMatch.idGuesser);

            this.manageGame = manageGame;
            this.manageGame.PlayerJoined += OnPlayerJoined;  // Subscribirse al evento

            if (manageGame.gameMatch.idGuesser > 0)
            {
                Console.WriteLine("------------------ GUESSER -------------------");
                lbGuestPlayer.Visibility = Visibility.Collapsed;
                lbTitle.Content = "Estás a punto de jugar con " + manageGame.gameMatch.idChallenger;
                btnStartGame.Content = "Salir";
            }
            else
            {
                lbTitle.Content = "Código de la partida";
                lbAccessCode.Content = manageGame.gameMatch.code;
                lbGuestPlayer.Visibility = Visibility.Visible;
                btnStartGame.Content = "Empezar";
            }
        }

        private void OnPlayerJoined(int idPlayer)
        {
            lbGuestPlayer.Content = $"{idPlayer} se ha unido a la partida";
            lbGuestPlayer.Visibility = Visibility.Visible;
        }

        private void BtnClickCancelGame(object sender, RoutedEventArgs e)
        {
            var pageHome = new PageHome(activePlayer);
            frCurrentFrame.Navigate(pageHome);
        }
    }

}
