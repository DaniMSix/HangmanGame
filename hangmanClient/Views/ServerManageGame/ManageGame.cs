using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Views.Pages;
using Views.SRIManageGameService;

namespace Views.ServerManageGame
{
    public class ManageGame : IManageGameServiceCallback
    {
        private ManageGameServiceClient manageGameServiceClient;
        Domain.DTOPlayer activePlayer;
        Gamematch gameMatch;

        Frame frame;

        public ManageGame(Domain.DTOPlayer activePlayer, Frame frame)
        {
            this.activePlayer = activePlayer;
            this.frame = frame;
            InstanceContext context = new InstanceContext(this);
            manageGameServiceClient = new ManageGameServiceClient(context);
        }

        public void StartGameConection(String gameName)
        {
            manageGameServiceClient.NewGame(activePlayer.IdPlayer, gameName);
        }

        public void StartJoinGame(String accessCode)
        {
            manageGameServiceClient.JoinGame(activePlayer.IdPlayer, accessCode);
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
            frame.Navigate(canceledGame);
        }

        public void CompleteRoom()
        {
            MessageBox.Show("La sala ya esta completa");
        }

        public void StartGame(Gamematch game)
        {
            throw new NotImplementedException();
        }

        public void StartGameRoom(Gamematch game)
        {
            this.gameMatch = game;

            var lobby = new PageWaitingRoom(activePlayer);
            frame.Navigate(lobby);
        }
    }
}
