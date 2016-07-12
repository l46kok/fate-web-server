using System;
using System.Collections.Generic;
using Fate.Common.Data;

namespace Fate.WebServiceLayer.ViewModels
{
    public class MainPageViewModel
    {
        public IEnumerable<GameData> RecentGameDataList { get; set; }
        public DateTime CurrentBotTime { get; set; }
         
    }
}
