using System.Collections.Generic;

namespace Fate.Common.Data
{
    public class GameDetailData
    {
        public List<GamePlayerDetailData> Team1Data { get; set; }
        public List<GamePlayerDetailData> Team2Data { get; set; }
        public int Team1Kills { get; set; }
        public int Team1Deaths { get; set; }
        public int Team1Assists { get; set; }
        public int Team1Gold { get; set; }
        public int Team2Kills { get; set; }
        public int Team2Deaths { get; set; }
        public int Team2Assists { get; set; }
        public int Team2Gold { get; set; }
    }
}
