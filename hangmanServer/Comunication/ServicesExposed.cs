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
                players.Add(gamematch.idChallenger.Value, callBackGameService);
                callBackGameService.StartGameRoom(registeredGame);
            }
            
        }

        public void JoinGame(Gamematch gamematch, bool withAccessCode)
        {
            var error = 1;
            bool guessRegistered = false;
            ManageGame manageGame = new ManageGame();
            string namePlayer = "";

            callBackGameService = OperationContext.Current.GetCallbackChannel<IManageGameServiceCallback>();
            if (callBackGameService != null)
            {
                foreach (Room room in globalRooms)
                {
                    if (room.GameMatch != null && ((withAccessCode && room.GameMatch.code == gamematch.code) || !withAccessCode))
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
                            namePlayer = manageGame.GetPlayerName(gamematch.idGuesser.Value);
                            players[room.GameMatch.idChallenger.Value].UserConnectionNotification(room.GameMatch, namePlayer);
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
            ManageGame manageGame = new ManageGame();
            foreach (Room room in globalRooms)
            {
                if (room.GameMatch.idGamematch == gameId)
                {
                    if (room.GameMatch.idChallenger.Value == userId)
                    {
                        manageGame.DeleteGameId(gameId);

                        if (room.GameMatch.idGuesser != null) {
                            players[room.GameMatch.idGuesser.Value].CanceledGame();
                            players.Remove(room.GameMatch.idGuesser.Value);
                        }

                        players.Remove(userId);

                        globalRooms.Remove(room);
                    }
                    else
                    {
                        string namePlayer = "";
                        room.Players.Remove(userId);
                        players.Remove(userId);
                        room.GameMatch.idGuesser = null;
                        namePlayer = manageGame.GetPlayerName(userId);
                        players[room.GameMatch.idChallenger.Value].UserDisconectionNotification(room.GameMatch, namePlayer);
                    }
                    break;
                }
            }
        }

        public void StartGame(int gameId)
        {
            ManageGame manageGame = new ManageGame();
            foreach (Room room in globalRooms)
            {
                if (room.GameMatch.idGamematch == gameId)
                {
                    manageGame.UpdateGameMatchStatus(gameId, 3);
                    DTOWord dtoWord = manageGame.GetWord(room.GameMatch.idWord.Value);

                    string word = "";
                    string hint = "";

                    if (room.GameMatch.language == "Ingles")
                    {
                        word = dtoWord.NameEn;
                        hint = dtoWord.HintEn;
                        room.Word = word;
                    }
                    else
                    {
                        word = dtoWord.Name;
                        hint = dtoWord.Hint;
                        room.Word = word;
                    }
                    room.LettersGuessed = new char[room.Word.Length];
                    room.FailedAttempts = 0;

                    string letters = ""; 
                    foreach(char letter in word)
                    {
                        letters += "_";
                    }

                    players[room.GameMatch.idChallenger.Value].StartGameChallenger(word, hint, letters);
                    players[room.GameMatch.idGuesser.Value].StartGameGuesser(hint, letters);

                    break;
                }
            }
        }

        public void ValidateLetter(int gameId, char letter)
        {
            ManageGame manageGame = new ManageGame();
            foreach (Room room in globalRooms)
            {
                if (room.GameMatch.idGamematch == gameId)
                {
                    bool isGuess = false;
                    int index = 0;
                    foreach (char wordLetter in room.Word)
                    {
                        if (Char.ToUpper(wordLetter) == Char.ToUpper(letter))
                        {
                            room.LettersGuessed[index] = letter;
                            
                            isGuess = true;
                        }
                        index++;
                    }

                    if (!isGuess)
                    {
                        room.FailedAttempts++;
                    }

                    if (room.FailedAttempts < room.MAXATTEMPTS)
                    {
                        if ((new string(room.LettersGuessed)).ToUpper() == room.Word.ToUpper())
                        {
                            manageGame.UpdateGameMatchStatus(gameId, 5);
                            manageGame.UpdateGameWinChallenger(gameId, false);

                            int challengerScore = manageGame.UpdatePlayerScore(room.GameMatch.idChallenger.Value, false);
                            int guesserScore = manageGame.UpdatePlayerScore(room.GameMatch.idGuesser.Value, true);

                            players[room.GameMatch.idChallenger.Value].FinishGame(room.Word, challengerScore, false, true);
                            players[room.GameMatch.idGuesser.Value].FinishGame(room.Word, guesserScore, true, false);

                            players.Remove(room.GameMatch.idChallenger.Value);
                            players.Remove(room.GameMatch.idGuesser.Value);

                            globalRooms.Remove(room);
                        }
                        else
                        {
                            players[room.GameMatch.idChallenger.Value].NotificationIfGuessed(room.LettersGuessed, room.FailedAttempts, false);
                            players[room.GameMatch.idGuesser.Value].NotificationIfGuessed(room.LettersGuessed, room.FailedAttempts, true);
                        }
                    }
                    else
                    {
                        manageGame.UpdateGameMatchStatus(gameId, 5);
                        manageGame.UpdateGameWinChallenger(gameId, true);

                        int challengerScore = manageGame.UpdatePlayerScore(room.GameMatch.idChallenger.Value, true);
                        int guesserScore = manageGame.UpdatePlayerScore(room.GameMatch.idGuesser.Value, false);

                        players[room.GameMatch.idChallenger.Value].FinishGame(room.Word, challengerScore, true, true);
                        players[room.GameMatch.idGuesser.Value].FinishGame(room.Word, guesserScore, false, false);

                        players.Remove(room.GameMatch.idChallenger.Value);
                        players.Remove(room.GameMatch.idGuesser.Value);

                        globalRooms.Remove(room);
                    }

                    break;
                }
            }
        }

        public void Disconnect(int userId, int gameId)
        {
            ManageGame manageGame = new ManageGame();
            foreach (Room room in globalRooms)
            {
                if (room.GameMatch.idGamematch == gameId)
                {
                    if(userId == room.GameMatch.idChallenger.Value)
                    {
                        players[room.GameMatch.idGuesser.Value].UserDisconected();
                        players.Remove(room.GameMatch.idGuesser.Value);
                    }
                    else
                    {
                        players[room.GameMatch.idChallenger.Value].UserDisconected();
                        players.Remove(room.GameMatch.idChallenger.Value);
                    }
                    players.Remove(userId);
                    manageGame.DeleteGameId(gameId);
                    globalRooms.Remove(room);

                    break;
                }
            }
        }

        public List<DTOGameMatch> GetGamematches()
        {
            ManageGame manageGame = new ManageGame();
            return manageGame.GetGamematches();
        }

        public List<DTOStatistics> GetStatistics(int idChallenger)
        {
            ManageGame manageGame = new ManageGame();
            return manageGame.GetStatistics(idChallenger);
        }

        public List<DTOWord> GetWords(int idCategory)
        {
            ManageGame manageGame = new ManageGame();
            return manageGame.GetWordsForCategory(idCategory);
        }

        public int GetScorePlayer(int idPlayer)
        {
            ManageGame manageGame = new ManageGame();
            return manageGame.GetScorePlayer(idPlayer);
        }
    }
}
