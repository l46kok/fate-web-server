using System;

namespace Fate.Common.Data
{
    public class GameData
    {
        public string GameName { get; set; }
        public DateTime PlayedDate { get; set; }
        public string ServerName { get; set; }
        public TimeSpan Duration { get; set; }
        public string Result { get; set; }
        public string MapVersion { get; set; }
        public string ReplayUrl { get; set; }
        public string MatchType { get; set; }
        public string Log { get; set; }
        public int TeamOneWinCount { get; set; }
        public int TeamTwoWinCount { get; set; }
    }
}
