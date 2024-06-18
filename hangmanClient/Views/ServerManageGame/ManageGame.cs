using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Views.Notifications;
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
        public event Action<string> PlayerJoined;
        public event Action<string> PlayerDisconnected;
        public event Action<char[], int, bool> IfGuessed;
        public event Action<Domain.DTOPlayer, Frame, string, int, bool, bool> GameFinished;
        public DTOGameMatch[] gamematches { get; set; }
        public DTOStatistics[] statistics { get; set; } 
        private IManageGameService iManageGame;

        public ManageGame()
        {
            // Inicialización básica, podría ser útil dependiendo de cómo lo uses.
            manageGameServiceClient = new ManageGameServiceClient(new InstanceContext(this));
        }

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

        public void FinishGameConnectionn()
        {
            manageGameServiceClient.DisconnectGame(activePlayer.IdPlayer, gameMatch.idGamematch);
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

        public void UserConnectionNotification(Gamematch gamematch, string namePlayerGuesser)
        {
            this.gameMatch = gamematch;
            Application.Current.Dispatcher.Invoke(() =>
            {
                PlayerJoined?.Invoke(namePlayerGuesser);  // Invocar el evento cuando un jugador se una
            });
        }

        public void UserDisconectionNotification(Gamematch gamematch, string namePlayerGuesser)
        {
            this.gameMatch = gamematch;
            Application.Current.Dispatcher.Invoke(() =>
            {
                PlayerDisconnected?.Invoke(namePlayerGuesser);
            });
        }

        public DTOGameMatch[] RecoveringGames()
        {
            if (manageGameServiceClient == null)
            {
                throw new InvalidOperationException("manageGameServiceClient no ha sido inicializado correctamente.");
            }
            return gamematches = manageGameServiceClient.GetGamematches();
        }

        public DTOStatistics[] RecoveringStatistics(int idPlayer)
        {
            if (manageGameServiceClient == null)
            {
                throw new InvalidOperationException("manageGameServiceClient no ha sido inicializado correctamente.");
            }
            return statistics = manageGameServiceClient.GetStatistics(idPlayer);
        }

        public void StartGame()
        {
            manageGameServiceClient.StartGame(gameMatch.idGamematch);
        }

        public void StartGameChallenger(string word, string hint)
        {
            var pageGame = new PageGamePlay(this, frCurrentFrame, word, hint);
            frCurrentFrame.Navigate(pageGame);
        }

        public void StartGameGuesser(string hint)
        {
            var pageGame = new PageGamePlay(this, frCurrentFrame, hint);
            frCurrentFrame.Navigate(pageGame);

        }

        public void ValidateLetter(char letter)
        {
            manageGameServiceClient.ValidateLetter(gameMatch.idGamematch, letter);
        }

        public void NotificationIfGuessed(char[] letters, int failedAttempts, bool isGuess)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IfGuessed?.Invoke(letters, failedAttempts, isGuess);
            });
        }

        public void FinishGame(string word, int score, bool win, bool challenger)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                GameFinished?.Invoke(activePlayer, frCurrentFrame, word, score, win, challenger);
            });
        }
    }
}
