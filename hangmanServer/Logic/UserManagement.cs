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
    public class UserManagement
    {
        public DTOPlayer AuthenticationLogin(string username, string password)
        {
            DTOPlayer playerDTO = null;

            using (var context = new hangman_dbEntities())
            {
                // Verificar las credenciales en la base de datos
                var player = context.Player.FirstOrDefault(p => p.username == username && p.password == password);

                if (player != null)
                {
                    playerDTO = new DTOPlayer
                    {
                        id_player = player.id_player,
                        username = player.username,
                        phonenumber = (int)player.phonenumber,
                        email = player.email,
                    };
                }
            }

            return playerDTO;
        }


        public bool UpdateEmailPassword(DTOPlayer dataPlayer)
        {
            int playerIdToUpdate = dataPlayer.id_player;

            using (var context = new hangman_dbEntities())
            {
                var player = context.Player.FirstOrDefault(user => user.id_player == playerIdToUpdate);

                if (player != null)
                {
                    player.phonenumber = dataPlayer.phonenumber;
                    player.email = dataPlayer.email;

                    context.Entry(player).State = EntityState.Modified;

                    context.SaveChanges();
                }
                return true;
            }
        }

        public bool UpdateFullProfile(DTOPlayer dataPlayer)
        {
            int playerIdToUpdate = dataPlayer.id_player;

            using (var context = new hangman_dbEntities())
            {
                var player = context.Player.FirstOrDefault(user => user.id_player == playerIdToUpdate);

                if (player != null)
                {
                    player.username = dataPlayer.username;
                    player.name = dataPlayer.name;
                    player.birthdate = dataPlayer.birthdate;
                    player.email = dataPlayer.email;
                    player.phonenumber = dataPlayer.phonenumber;
                    player.password = dataPlayer.password;

                    context.Entry(player).State = EntityState.Modified;

                    context.SaveChanges();
                }
                return true;
            }
        }
    }
}
}
