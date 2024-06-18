using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DTOStatistics
    {
        public List<DTOStatistics> statistics { get; set; }
        public string usernameGuesser { get; set; }
        public string state { get; set; }
        public string dateGame { get; set; }
        public float score { get; set; }
    }
}
