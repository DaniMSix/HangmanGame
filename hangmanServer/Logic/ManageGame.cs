using DataAccess;
using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Logic
{
    public class ManageGame
    {
        public List<DTOGameMatch> Gamematches { get; set; }

        public Gamematch RegisterGame(Gamematch gameMatch)
        {
            Gamematch registeredGame = null;

            try
            {
                using (var context = new HangmanDbEntities())
                {
                    
                    Gamematch gameToRegister = new Gamematch()
                    {
                        code = new Random().Next(0, 100000).ToString("D5"),
                        language = gameMatch.language,
                        idWord = gameMatch.idWord,
                        idChallenger = gameMatch.idChallenger,
                        idMatchStatus = gameMatch.idMatchStatus

                    };
                    context.Gamematch.Add(gameToRegister);
                    context.SaveChanges();
                    registeredGame = gameToRegister;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el juego: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    Console.WriteLine("Inner Inner Exception: " + ex.InnerException.InnerException.Message);
                }

            }

            return registeredGame;
        }
        
        public bool RegisterPlayer(DTOPlayer newPlayer)
        {
            bool status = false;

            using (var context = new HangmanDbEntities())
            {
                Player recordPlayer = new Player()
                {
                    username = newPlayer.Username,
                    name = newPlayer.Name,
                    birthdate = newPlayer.Birthdate,
                    phonenumber = newPlayer.Phonenumber,
                    email = newPlayer.Email,
                    password = newPlayer.Password,
                };

                context.Player.Add(recordPlayer);
                status = context.SaveChanges() > 0;
            }
            return status;
        }
         
        public bool DeleteGameId(int id_gamematch)
        {
            bool status = false;
            try
            {
                using (var context = new HangmanDbEntities())
                {
                    var juegoAEliminar = context.Gamematch.FirstOrDefault(Game_Match => Game_Match.idGamematch == id_gamematch);
                    if (juegoAEliminar != null)
                    {
                        context.Gamematch.Remove(juegoAEliminar); 
                        context.SaveChanges(); 
                        status = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el juego: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    Console.WriteLine("Inner Inner Exception: " + ex.InnerException.InnerException.Message);
                }
            }
            return status;
        }

        public bool RegisterGuestPlayerByAccessCode(Gamematch gamematch)
        {
            bool status = false;

            using (var context = new HangmanDbEntities())
            {
                var gameMatch = context.Gamematch.SingleOrDefault(g => g.code == gamematch.code);

                if (gameMatch != null)
                {
                    gameMatch.idGuesser = gamematch.idGuesser;

                    context.SaveChanges();
                    status = true;
                    Console.WriteLine("El idGuesser ha sido actualizado.");
                }
                else
                {
                    Console.WriteLine("No se encontró un Gamematch con el código especificado.");
                }
            }
            return status;
        }

        public string GetPlayerName(int idPlayer)
        {
            string playerName = "";
            using (var context = new HangmanDbEntities())
            {
                var playerNameContext = (from p in context.Player
                                      where p.idPlayer == idPlayer
                                  select p.name).FirstOrDefault();
                if (playerNameContext != null)
                {
                    playerName = playerNameContext;
                }
                else
                {
                    Console.WriteLine("No se encontró un jugador con el id especificado.");
                }
                return playerName;
            }
        }

        public List<DTOGameMatch> GetGamematches()
        {
            try
            {
                using (var context = new HangmanDbEntities())
                {
                    var gamematches = (from g in context.Gamematch
                                       join p in context.Player on g.idChallenger equals p.idPlayer
                                       select new DTOGameMatch
                                       {
                                           idGamematch = g.idGamematch,
                                           code = g.code,
                                           language = g.language,
                                           idWord = g.idWord.Value,
                                           idChallenger = g.idChallenger.Value,
                                           idMatchStatus = g.idMatchStatus.Value,
                                           creationDate = g.creationDate.Value,
                                           ChallengerUsername = p.username,
                                           ChallengerName = p.name,
                                       }).ToList();
                    return gamematches;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los juegos: " + ex.Message);
                throw; 
            }
        }

        public List<DTOStatistics> GetStatistics(int idChallenger)
        {
            try
            {
                using (var context = new HangmanDbEntities())
                {
                    var gamematches = (from g in context.Gamematch
                                       join c in context.Player on g.idChallenger equals c.idPlayer
                                       join u in context.Player on g.idGuesser equals u.idPlayer
                                       join s in context.MatchStatus on g.idMatchStatus equals s.idMatchStatus
                                       where g.idChallenger == idChallenger
                                       select new
                                       {
                                           usernameGuesser = u.username,
                                           dateGame = g.creationDate.Value,
                                           score = c.score.Value,
                                       }).AsEnumerable() // This will execute the query and bring the results into memory
                     .Select(x => new DTOStatistics
                     {
                         usernameGuesser = x.usernameGuesser,
                         dateGame = x.dateGame.ToString("yyyy-MM-dd"),
                         score = x.score,
                     }).ToList();

                    return gamematches;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los juegos: " + ex.Message);
                throw;
            }
        }

        public void UpdateGameMatchStatus(int gameId, int status)
        {
            using (var context = new HangmanDbEntities())
            {
                var gameMatch = context.Gamematch.FirstOrDefault(game => game.idGamematch == gameId);

                if (gameMatch != null)
                {
                    gameMatch.idMatchStatus = status;

                    context.Entry(gameMatch).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
        }

        public void UpdateGameWinChallenger(int gameId, bool win)
        {
            using (var context = new HangmanDbEntities())
            {
                var gameMatch = context.Gamematch.FirstOrDefault(game => game.idGamematch == gameId);

                if (gameMatch != null)
                {
                    gameMatch.winChallenger = win;

                    context.Entry(gameMatch).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
        }

        public int UpdatePlayerScore(int playerId, bool win)
        {
            using (var context = new HangmanDbEntities())
            {
                int score = 0;
                var playerToUpdate = context.Player.FirstOrDefault(player => player.idPlayer == playerId);

                if (playerToUpdate != null)
                {
                    if (win)
                    {
                        score = playerToUpdate.score.Value + 10;
                        playerToUpdate.score = score;
                    }
                    else
                    {
                        score = playerToUpdate.score.Value - 10;
                        playerToUpdate.score = score;
                    }

                    context.Entry(playerToUpdate).State = EntityState.Modified;

                    context.SaveChanges();
                }

                return score;
            }
        }

        public DTOWord GetWord(int wordId)
        {
            using (var context = new HangmanDbEntities())
            {
                var word = context.Word.FirstOrDefault(w => w.idWord == wordId);

                return new DTOWord
                {
                    IdWord = wordId,
                    Name = word.name,
                    NameEn = word.nameEN,
                    Hint = word.hint,
                    HintEn = word.hintEN
                };
            }
        }
    }
}
