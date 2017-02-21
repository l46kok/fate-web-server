using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fate.Common.Data
{
    public class GameListData
    {
        [JsonProperty(PropertyName = "Lobby")]
        public GameLobbyData Lobby { get; set; }
        [JsonProperty(PropertyName = "Progress")]
        public List<GameProgressData> ProgressList { get; set; } 
        public string Server { get; set; }
    }

    public class GamePlayerData
    {
        public string HeroIconURL { get; set; }
        public int SlotNumber { get; set; }
        public int TeamNumber { get; set; }
        public string PlayerName { get; set; }
        public string Server { get; set; }
        public string Ping { get; set; }
        public bool IsConnected { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
    }

    public class GameLobbyData
    {
        [JsonProperty(PropertyName = "LobbyPlayers")]
        public List<GamePlayerData> PlayerDataList { get; set; }
        public bool IsAvailable { get; set; }
        public int PlayerCount { get; set; }
        public int SlotSize { get; set; }
        public string GameName { get; set; }
        public string Owner { get; set; }
    }

    public class GameKDAData
    {
        public int PlayerID { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
    }

    public class GameProgressData
    {
        [JsonProperty(PropertyName = "ProgressPlayers")]
        public List<GamePlayerData> PlayerDataList { get; set; }
        public List<GamePlayerData> Team1DataList { get; set; } = new List<GamePlayerData>();
        public List<GamePlayerData> Team2DataList { get; set; } = new List<GamePlayerData>();
        [JsonProperty(PropertyName = "FRSKills")]
        public List<GameKDAData> PlayerKills { get; set; }
        [JsonProperty(PropertyName = "FRSDeaths")]
        public List<GameKDAData> PlayerDeaths { get; set; }
        [JsonProperty(PropertyName = "FRSAssists")]
        public List<GameKDAData> PlayerAssists { get; set; }
        public int Team1Wins { get; set; }
        public int Team2Wins { get; set; }
        [JsonProperty(PropertyName = "FRSEvents")]
        public List<string> FRSEventList { get; set; }
        public int GameNumber { get; set; }
        public int PlayerCount { get; set; }
        public int SlotSize { get; set; }
        public string Server { get; set; }
        public string GameName { get; set; }
        public int GameDuration { get; set; }
    }
}
