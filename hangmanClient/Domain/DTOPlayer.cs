using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DTOPlayer
    {
        public int IdPlayer { get; set; }

        public String Username { get; set; }

        public String Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string Phonenumber { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }

        public int Score { get; set; }
    }
}
