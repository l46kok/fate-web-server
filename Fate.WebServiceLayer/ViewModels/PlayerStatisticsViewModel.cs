using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fate.WebServiceLayer.ViewModels
{
    public class PlayerStatisticsViewModel
    {
        public string PlayerName { get; set; }
        public string WinRatio { get; set; }
        public string WinRatioPBColor { get; set; }
        public string AvgKDA { get; set; }
        public string KDAColor { get; set; }
        public string AvgGoldSpent { get; set; }
        public string AvgDamageDealt { get; set; }
        public string AvgDamageTaken { get; set; }
    }
}
