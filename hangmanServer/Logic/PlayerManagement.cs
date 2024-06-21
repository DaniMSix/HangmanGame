using DataAccess;
using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PlayerManagement
    {
        public DTOPlayer AuthenticationLogin(string username, string password)
        {

            DTOPlayer playerDTO = null;

            using (var context = new HangmanDbEntities())
            {
                var player = context.Player.FirstOrDefault(p => p.username == username && p.password == password);

                if (player != null)
                {
                    playerDTO = new DTOPlayer
                    {
                        IdPlayer = player.idPlayer,
                        Username = player.username,
                        Name = player.name,
                        Phonenumber = player.phonenumber,
                        Email = player.email,
                    };
                }
            }

            return playerDTO;
        }

        public bool UpdateEmailPassword(DTOPlayer dataPlayer)
        {
            int playerIdToUpdate = dataPlayer.IdPlayer;

            using (var context = new HangmanDbEntities())
            {
                var player = context.Player.FirstOrDefault(user => user.idPlayer == playerIdToUpdate);

                if (player != null)
                {
                    player.phonenumber = dataPlayer.Phonenumber;
                    player.email = dataPlayer.Email;

                    context.Entry(player).State = EntityState.Modified;

                    context.SaveChanges();
                }
                return true;
            }
        }

        public bool UpdateFullProfile(DTOPlayer dataPlayer)
        {
            int playerIdToUpdate = dataPlayer.IdPlayer;
            Console.WriteLine("ACTUALIZAR PERFIL");
            Console.WriteLine("Id " + dataPlayer.IdPlayer);

            using (var context = new HangmanDbEntities())
            {
                var player = context.Player.FirstOrDefault(user => user.idPlayer == playerIdToUpdate);

                if (player == null)
                {
                    Console.WriteLine("Jugador no encontrado.");
                    return false;
                }

                player.username = dataPlayer.Username;
                player.name = dataPlayer.Name;
                player.birthdate = dataPlayer.Birthdate;
                player.phonenumber = dataPlayer.Phonenumber;

                context.Entry(player).State = EntityState.Modified;

                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar los cambios: {ex.Message}");
                    return false;
                }
            }
        }


        public bool RegisterPlayer(DTOPlayer newPlayer)
        {

            Console.WriteLine("Register server");
            bool status = false;

            using (var context = new HangmanDbEntities())
            {
                Player recordPlayer = new Player()
                {
                    username = newPlayer.Username,
                    name = newPlayer.Name,
                    birthdate = newPlayer.Birthdate,
                    password = newPlayer.Password,
                    email = newPlayer.Email,
                    phonenumber = newPlayer.Phonenumber,
                    score = 0
                };

                context.Player.Add(recordPlayer);
                status = context.SaveChanges() > 0;
            }
            return status;
        }
    }
}

