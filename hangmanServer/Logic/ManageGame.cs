using DataAccess;
using Logic.DTO;
using System;
using System.Linq;

namespace Logic
{
    public class ManageGame
    {
        public Gamematch RegisterGame(string language)
        {
            Gamematch registeredGame = null;

            try
            {
                using (var context = new HangmanDbEntities())
                {
                    
                    Gamematch gameToRegister = new Gamematch()
                    {
                        language = language,
                        code = new Random().Next(0, 100000).ToString("D5")
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
    }
    // gameStatus = GameStatus.Waitting.ToString(),

}
