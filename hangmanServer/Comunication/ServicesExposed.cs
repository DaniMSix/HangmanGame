using DataAccess;
using Logic;
using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.ServiceModel;

namespace Comunication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public partial class ServicesExposed : IPlayerManagement
    {
        public event Action<int> ProgressChanged;
        public DTOPlayer AuthenticateLogin(string username, string password)
        {
            DTOPlayer player = new DTOPlayer();

            try
            {
                PlayerManagement authentication = new PlayerManagement();

                player = authentication.AuthenticationLogin(username, password);

            }
            catch (EntityException entityException)
            {
                Console.WriteLine(entityException.Message);
            };
            return player;
        }

        public bool RegisterPlayer(DTOPlayer newPlayer)
        {
            var status = false;
            try
            {
                Register register = new Register();
                status = register.RegisterPlayer(newPlayer);
            }
            catch (EntityException entityException)
            {
                Console.WriteLine(entityException.Message);
            }
            return status;
        }

        public bool UpdateEmailPassword(DTOPlayer dataPlayer)
        {
            var status = false;
            try
            {
                PlayerManagement userManagement = new PlayerManagement();
                status = userManagement.UpdateEmailPassword(dataPlayer);
            }
            catch (EntityException entityException)
            {
                Console.WriteLine(entityException.Message);
            }
            return status;
        }

        public bool UpdateFullProfile(DTOPlayer dataPlayer)
        {
            var status = false;
            try
            {
                PlayerManagement userManagement = new PlayerManagement();
                status = userManagement.UpdateFullProfile(dataPlayer);
            }
            catch (EntityException entityException)
            {
                Console.WriteLine(entityException.Message);
            }
            return status;
        }

    }

    public partial class ServicesExposed : IManageGameService
    {
        private IManageGameServiceCallback callBackGameService = null;
        private List<Room> globalRooms = new List<Room>();
        private Dictionary<int, IManageGameServiceCallback> players = new Dictionary<int, IManageGameServiceCallback>();

        public void NewGame(Gamematch gamematch)
        {
            ManageGame manageGame = new ManageGame();
            Gamematch registeredGame = null;
            try
            {
                registeredGame = manageGame.RegisterGame(gamematch);
                Console.WriteLine("Code1 " +  registeredGame.code);

                Console.WriteLine("RegisterLanguage " + registeredGame.language);
            }
            catch (EntityException entityException)
            {
                Console.WriteLine($"{entityException.Message}");
            }

            if (registeredGame != null)
            {
                Room room = new Room()
                {
                    GameMatch = registeredGame,
                    HostUserId = gamematch.idChallenger.Value,
                    Players = new List<int>()
                };
                
                room.Players.Add(gamematch.idChallenger.Value);
                globalRooms.Add(room);
            }
            callBackGameService = OperationContext.Current.GetCallbackChannel<IManageGameServiceCallback>();
            if (callBackGameService != null)
            {
                Console.WriteLine("Code " + gamematch.code);
                players.Add(gamematch.idChallenger.Value, callBackGameService);
                callBackGameService.StartGameRoom(registeredGame);
            }
            
        }

        public void JoinGame(Gamematch gamematch)
        {
            var error = 1;
            bool guessRegistered = false;
            ManageGame manageGame = new ManageGame();


            callBackGameService = OperationContext.Current.GetCallbackChannel<IManageGameServiceCallback>();
            if (callBackGameService != null)
            {
                foreach (Room room in globalRooms)
                {
                    if (room.GameMatch != null && room.GameMatch.code == gamematch.code)
                    {
                        if (room.Players.Count >= room.MAXPLAYERS)
                        {
                            error = 2;
                        }
                        else
                        {
                            error = 0;
                            room.Players.Add(gamematch.idGuesser.Value);
                            room.GameMatch.idGuesser = gamematch.idGuesser.Value;
                            players.Add(gamematch.idGuesser.Value, callBackGameService);
                            guessRegistered = manageGame.RegisterGuestPlayerByAccessCode(gamematch);
                            callBackGameService.StartGameRoom(room.GameMatch);

                            players[room.GameMatch.idChallenger.Value].UserConnectionNotification(room.GameMatch);
                        }
                        break;
                    }
                }

                if (error == 1)
                {
                    callBackGameService.AccessCodeNotFound();
                }

                if (error == 2)
                {
                    callBackGameService.CompleteRoom();
                }
            }
        }

        public void DisconnectGame(int userId, int gameId)
        {
            foreach (Room room in globalRooms)
            {
                if (room.GameMatch.idGamematch == gameId)
                {
                    if (room.HostUserId == userId)
                    {
                        ManageGame manageGame = new ManageGame();
                        manageGame.DeleteGameId(gameId);

                        List<int> roomPlayers = room.Players;
                        globalRooms.Remove(room);

                        foreach (var roomPlayer in roomPlayers)
                        {
                            if (players.ContainsKey(roomPlayer))
                            {
                                if (roomPlayer != userId)
                                {
                                    players[roomPlayer].CanceledGame();
                                }
                                players.Remove(roomPlayer);
                            }
                        }
                    }
                    else
                    {
                        room.Players.Remove(userId);

                        if (players.ContainsKey(userId))
                        {
                            players.Remove(userId);
                        }
                    }
                    break;
                }
            }
        }
    }
}
