﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fate.WebServiceLayer
{
    public static class ContentURL
    {
        private static readonly Dictionary<string, string> _heroIconDic = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _ghIconDic = new Dictionary<string, string>();

        static ContentURL()
        {
            _heroIconDic.Add("H028", @"/Content/icons/heroes/BTNAvenger.jpg");
            _heroIconDic.Add("H000",@"/Content/icons/heroes/BTNSaber.jpg");
            _heroIconDic.Add("H002",@"/Content/icons/heroes/BTNChulainn.jpg");
            _heroIconDic.Add("H004", @"/Content/icons/heroes/BTNCaster.jpg");
            _heroIconDic.Add("H001", @"/Content/icons/heroes/BTNArcherIcon.jpg");
            _heroIconDic.Add("H005", @"/Content/icons/heroes/BTNAssassinIcon.jpg");
            _heroIconDic.Add("H006", @"/Content/icons/heroes/BTNBerserker.jpg");
            _heroIconDic.Add("H007", @"/Content/icons/heroes/BTNDarkSaber.jpg");
            _heroIconDic.Add("H00Y", @"/Content/icons/heroes/BTNDrake.jpg");
            _heroIconDic.Add("H00I", @"/Content/icons/heroes/BTNGawain.jpg");
            _heroIconDic.Add("H009", @"/Content/icons/heroes/BTNGilgamesh.jpg");
            _heroIconDic.Add("E002", @"/Content/icons/heroes/BTNGilles.jpg");
            _heroIconDic.Add("H00A", @"/Content/icons/heroes/BTNIskander.jpg");
            _heroIconDic.Add("H021", @"/Content/icons/heroes/BTNKarna.jpg");
            _heroIconDic.Add("H03M", @"/Content/icons/heroes/BTNLancelot.jpg");
            _heroIconDic.Add("H01X", @"/Content/icons/heroes/BTNNero.jpg");
            _heroIconDic.Add("H024", @"/Content/icons/heroes/BTNNursery.jpg");
            _heroIconDic.Add("H003", @"/Content/icons/heroes/BTNRider.jpg");
            _heroIconDic.Add("H01T", @"/Content/icons/heroes/BTNRobin.jpg");
            _heroIconDic.Add("H00E", @"/Content/icons/heroes/BTNTamamo.jpg");
            _heroIconDic.Add("H008", @"/Content/icons/heroes/BTNTrueAssassin.jpg");
            _heroIconDic.Add("H01F", @"/Content/icons/heroes/BTNVlad.jpg");
            _heroIconDic.Add("H01Q", @"/Content/icons/heroes/BTNYeopo.jpg");
            _heroIconDic.Add("H04D", @"/Content/icons/heroes/BTNZeroLancer.jpg");
            _heroIconDic.Add("H01A", @"/Content/icons/heroes/BTNLi.jpg");
            _ghIconDic.Add("A04B", @"/Content/icons/GHGold.jpg");
            _ghIconDic.Add("A04F", @"/Content/icons/GHLevelUp.jpg");
            _ghIconDic.Add("A0DD", @"/Content/icons/GHStats.jpg");
            _ghIconDic.Add("A04C", @"/Content/icons/GHAMP.jpg");
            _ghIconDic.Add("A04D", @"/Content/icons/GHFullHealPot.jpg");
            _ghIconDic.Add("A00U", @"/Content/icons/GHInvulBird.jpg");
        }

        public static string GetHeroIconURL(string heroUnitTypeId)
        {
            string url;
            _heroIconDic.TryGetValue(heroUnitTypeId, out url);
            return url;
        }

        public static string GetGodsHelpIconURL(string godsHelpAbilId)
        {
            string url;
            _ghIconDic.TryGetValue(godsHelpAbilId, out url);
            return url;
        }

        public static string GetLocalReplayPath(string replayPath)
        {
            return "Content\\replays\\" + Path.GetFileName(replayPath);
        }
    }
}
