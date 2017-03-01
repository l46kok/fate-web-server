using System;
using System.Collections.Generic;

namespace Fate.Common.Data
{
    public class PlayerBanData
    {
        public string PlayerName { get; set; }
        public List<string> IpAddresses { get; set; }
        public bool IsPermanentBan { get; set; }
        public DateTime? BannedUntil { get; set; }
        public string Reason { get; set; }
        public string Admin { get; set; }
    }
}
