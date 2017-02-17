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
        public string PlayerName { get; set; }
        public string Server { get; set; }
        public string Ping { get; set; }
        public bool IsConnected { get; set; }
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

    public class GameProgressData
    {
        [JsonProperty(PropertyName = "ProgressPlayers")]
        public List<GamePlayerData> PlayerDataList { get; set; }
        [JsonProperty(PropertyName = "FRSEvents")]
        public List<string> ServantSelectionInfoList { get; set; }
        public int GameNumber { get; set; }
        public int PlayerCount { get; set; }
        public int SlotSize { get; set; }
        public string Server { get; set; }
        public string GameName { get; set; }
        public int GameDuration { get; set; }
    }
}
