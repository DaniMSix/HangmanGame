using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Views.Pages;
using Views.SRIManageGameService;

namespace ServerManageGame
{
    public class ManageGame : IManageGameServiceCallback
    {
        private ManageGameServiceClient manageGameServiceClient;
        private Domain.DTOPlayer activePlayer;
        public Gamematch gameMatch = new Gamematch();
        private PageWaitingRoom waitingRoomPage;
        Frame frCurrentFrame;
        public event Action<int> PlayerJoined;

        public ManageGame(Domain.DTOPlayer activePlayer, Frame frame)
        {
            this.activePlayer = activePlayer;
            this.frCurrentFrame = frame;
            InstanceContext context = new InstanceContext(this);
            manageGameServiceClient = new ManageGameServiceClient(context);
        }

        public void StartGameConection(Gamematch gameMatch)
        {
            manageGameServiceClient.NewGame(gameMatch);
        }

        public void StartJoinGame(Gamematch gamematch)
        {
            manageGameServiceClient.JoinGame(gamematch);
        }

        public void FinishGameConnectionn(int idPlayer, int IdGame)
        {
            manageGameServiceClient.DisconnectGame(idPlayer, IdGame);
        }

        public void AccessCodeNotFound()
        {
            MessageBox.Show("No se encontro el código de acceso");
        }

        public void CanceledGame()
        {
            var canceledGame = new PageHome(activePlayer);
            frCurrentFrame.Navigate(canceledGame);
        }

        public void CompleteRoom()
        {
            MessageBox.Show("La sala ya esta completa");
        }

        public void StartGameRoom(Gamematch game)
        {
            this.gameMatch = game;
            var lobby = new PageWaitingRoom(activePlayer, this, frCurrentFrame);
            frCurrentFrame.Navigate(lobby);
        }

        public void UserConnectionNotification(Gamematch game)
        {
            this.gameMatch = game;
            Application.Current.Dispatcher.Invoke(() =>
            {
                PlayerJoined?.Invoke(game.idGuesser.Value);  // Invocar el evento cuando un jugador se una
            });
            /*
            this.gameMatch = game;
            Console.WriteLine("id ");
            MessageBox.Show("se unio un jugador");*/
        }
    }
}
