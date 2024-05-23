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
        public int id_player { get; set; }

        [DataMember]
        public String username { get; set; }

        [DataMember]
        public String name { get; set; }

        [DataMember]
        public DateTime birthdate  { get; set; }

        [DataMember]
        public int phonenumber { get; set; }

        [DataMember]
        public String email { get; set; }

        [DataMember]
        public String password { get; set; }

        [DataMember]
        public int score { get; set; }
    }
}
