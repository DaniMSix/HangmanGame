using DataAccess;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Logic
{
    public class Room
    {
        private Gamematch game;
        private int hostUserId;
        private const int MAX_PLAYERS = 2;
        private const int MIN_PLAYERS = 1;
        private List<int> players;
        private string word;
        private char[] lettersGuessed;
        private int failedAttempts;
        private const int MAX_ATTEMPTS = 6;

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

        [DataMember]
        public string Word { get; set; }

        [DataMember]
        public char[] LettersGuessed {  get; set; }

        [DataMember]
        public int FailedAttempts {  get; set; }

        [DataMember]
        public int MAXATTEMPTS => MAX_ATTEMPTS;
    }
}
