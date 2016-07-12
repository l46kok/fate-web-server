using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fate.Common.Data
{
    public class PlayerStatSummaryData
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public int TotalPlayerKills { get; set; }
        public int TotalPlayerDeaths { get; set; }
        public int TotalPlayerAssists { get; set; }
        public int TotalPlayerGameCount { get; set; }
        public DateTime LastGamePlayed { get; set; }
    }
}
