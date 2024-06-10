using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class DTOPlayer
    {
        [DataMember]
        public int IdPlayer { get; set; }

        [DataMember]
        public String Username { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public DateTime Birthdate  { get; set; }

        [DataMember]
        public string Phonenumber { get; set; }

        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public String Password { get; set; }

        [DataMember]
        public int Score { get; set; }
    }
}
