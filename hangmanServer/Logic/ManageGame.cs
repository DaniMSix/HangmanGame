using DataAccess;
using Logic.DTO;
using System;
using System.Linq;

namespace Logic
{
    public class ManageGame
    {
        public Game_Match RegisterGame(string language)
        {
            Game_Match registeredGame = null;

            try
            {
                using (var context = new hangman_dbEntities())
                {
                    
                    Game_Match gameToRegister = new Game_Match()
                    {
                        language = language,
                        code = new Random().Next(0, 100000).ToString("D5")
                     };
                    context.Game_Match.Add(gameToRegister);
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

            using (var context = new hangman_dbEntities())
            {
                Player recordPlayer = new Player()
                {
                    username = newPlayer.username,
                    name = newPlayer.name,
                    birthdate = newPlayer.birthdate,
                    phonenumber = newPlayer.phonenumber,
                    email = newPlayer.email,
                    password = newPlayer.password,
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
                using (var context = new hangman_dbEntities())
                {
                    var juegoAEliminar = context.Game_Match.FirstOrDefault(Game_Match => Game_Match.id_gamematch == id_gamematch);
                    if (juegoAEliminar != null)
                    {
                        context.Game_Match.Remove(juegoAEliminar); 
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
    }
    // gameStatus = GameStatus.Waitting.ToString(),

}
