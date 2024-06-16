using DataAccess;
using Logic.DTO;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Numerics;

namespace Logic
{
    public class ManageGame
    {
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
    }
    

}
