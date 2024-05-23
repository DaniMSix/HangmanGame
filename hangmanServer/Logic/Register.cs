using DataAccess;
using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Register
    {
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

        public bool CheckIfEmailAndUsernameExist(System.String username, String email)
        {
            bool thereAreRecords = false;
            using (var context = new hangman_dbEntities())
            {
                var accounts = (from player in context.Player
                                where
                                player.username == username || player.email == email
                                select player).Count();

                if (accounts > 0)
                {
                    thereAreRecords = true;
                }
            }
            return thereAreRecords;
        }
    }
}
