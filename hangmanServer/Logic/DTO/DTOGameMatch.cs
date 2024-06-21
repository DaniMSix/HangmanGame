using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class DTOGameMatch
    {
        public List<DTOGameMatch> Gamematches { get; set; }
        public int idGamematch { get; set; }
        public string code { get; set; }
        public string language { get; set; }
        public int idWord { get; set; }
        public int idChallenger { get; set; }
        public int idMatchStatus { get; set; }
        public DateTime creationDate { get; set; }
        public string ChallengerUsername { get; set; }
        public string ChallengerName { get; set; }
        public DateTime ChallengerBirthdate { get; set; }
        public string ChallengerPhoneNumber { get; set; }
        public string ChallengerEmail { get; set; }
        public string ChallengerPassword { get; set; }
        public int ChallengerScore { get; set; }
        public string Category { get; set; }
    }
}
