using System;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public int userId { get; set; }

        [DataMember]
        public String username { get; set; }

        [DataMember]
        public String password { get; set; }

        [DataMember]
        public String phone { get; set; }

        [DataMember]
        public String email { get; set; }

        [DataMember]
        public bool isActive { get; set; }

        [DataMember]
        public int numberGamesWon { get; set; }

        [DataMember]
        public int pointsEarned { get; set; }
    }
}
