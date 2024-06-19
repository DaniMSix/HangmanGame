using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class DTOStatistics
    {
        public string usernameGuesser { get; set; }
        public string state { get; set; }
        public string dateGame { get; set; }
        public bool win { get; set; }
        public float score { get; set; }
    }
}
