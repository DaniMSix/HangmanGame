using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Room
    {
        private Gamematch game;
        private int hostUserId;
        private const int MAX_PLAYERS = 4;
        private const int MIN_PLAYERS = 1;
        private List<int> players;

        [DataMember]
        public Gamematch GameMatch { get; set; }

        [DataMember]
        public int HostUserId { get; set; }

        [DataMember]
        public int MAXPLAYERS => MAX_PLAYERS;

        [DataMember]
        public int MINPLAYERS => MIN_PLAYERS;

        [DataMember]
        public List<int> Players { get; set; }
    }
}
