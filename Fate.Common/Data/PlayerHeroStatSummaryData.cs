using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fate.Common.Data
{
    public class PlayerHeroStatSummaryData
    {
        public string HeroName { get; set; }
        public string HeroUnitTypeID { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int HeroTotalKills { get; set; }
        public int HeroTotalDeaths { get; set; }
        public int HeroTotalAssists { get; set; }
        public int HeroTotalPlayCount { get; set; }
    }
}
