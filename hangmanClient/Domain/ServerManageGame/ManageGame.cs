using System;
using System.ServiceModel;
using System.Windows;
using Views;
using Views.SRManageGame;
using Views.SRIManageGameService;

namespace Domain.ServerManageGame
{
    public class ManageGame : IManageGameServiceCallback
    {
        private ManageGameServiceClient manageGameServiceClient;
        private Domain.DTOPlayer loggedPlayerData { get; set; }
        public Game game { get; set; }
        private Window window { get; set; }

        public ManageGame(Domain.DTOPlayer loggedPlayerData, Window window)
        {
            this.loggedPlayerData = loggedPlayerData;
            this.window = window;
            InstanceContext context = new InstanceContext(this);
            manageGameServiceClient = new ManageGameServiceClient(context);
        }

        public void StartGameConection(String gameName)
        {
            manageGameServiceClient.NewGame(loggedPlayerData.UserId, gameName);
        }

        public void StartJoinGame(String accessCode)
        {
            manageGameServiceClient.JoinGame(loggedPlayerData.UserId, accessCode);
        }

        public void FinishGameConnectionn(int userId, int gameId)
        {
            manageGameServiceClient.DisconnectGame(userId, gameId);
        }

        public void StartGameRoom(Game game)
        {
            this.game = game;
            Lobby lobby = new Lobby(loggedPlayerData, this)
            {
                WindowState = window.WindowState,
                Left = window.Left
            };
            lobby.Show();
            Window currentWindow = window;
            window = lobby;
            currentWindow.Close();
        }

        public void AccessCodeNotFound()
        {
            MessageBox.Show("No se encontro el código de acceso");
        }

        public void CanceledGame()
        {
            HomeScreen homeScreen = new HomeScreen(loggedPlayerData)
            {
                WindowState = window.WindowState,
                Left = window.Left
            };
            homeScreen.Show();
            window.Close();
        }

        public void CompleteRoom()
        {
            MessageBox.Show("La sala ya esta completa");
        }
    }
