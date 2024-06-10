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

        public bool CheckIfEmailAndUsernameExist(System.String username, String email)
        {
            bool thereAreRecords = false;
            using (var context = new HangmanDbEntities())
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
