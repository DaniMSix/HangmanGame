using DataAccess;
using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PlayerManagement
    {
        public DTOPlayer AuthenticationLogin(string username, string password)
        {
            Console.WriteLine("LLEGUE A LA AUTENTICACIÓN");
            Console.WriteLine($"{username}:{password}");

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

            using (var context = new HangmanDbEntities())
            {
                var player = context.Player.FirstOrDefault(user => user.idPlayer == playerIdToUpdate);

                if (player != null)
                {
                    player.username = dataPlayer.Username;
                    player.name = dataPlayer.Name;
                    player.birthdate = dataPlayer.Birthdate;
                    player.email = dataPlayer.Email;
                    player.phonenumber = dataPlayer.Phonenumber;
                    player.password = dataPlayer.Password;

                    context.Entry(player).State = EntityState.Modified;

                    context.SaveChanges();
                }
                return true;
            }
        }
    }
}

