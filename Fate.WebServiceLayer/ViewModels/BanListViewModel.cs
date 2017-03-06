using System.Collections.Generic;
using Fate.Common.Data;

namespace Fate.WebServiceLayer.ViewModels
{
    public class BanListViewModel
    {
        public IEnumerable<PlayerBanData> PermanentBans { get; set; }
        public IEnumerable<PlayerBanData> TimeBans { get; set; }
    }
}
