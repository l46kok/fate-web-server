using System;
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

        static ContentURL()
        {
            _heroIconDic.Add("H028", @"/Content/icons/BTNAvenger.jpg");
            _heroIconDic.Add("H000",@"/Content/icons/BTNSaber.jpg");
            _heroIconDic.Add("H002",@"/Content/icons/BTNChulainn.jpg");
            _heroIconDic.Add("H004", @"/Content/icons/BTNCaster.jpg");
            _heroIconDic.Add("H001", @"/Content/icons/BTNArcherIcon.jpg");
            _heroIconDic.Add("H005", @"/Content/icons/BTNAssassinIcon.jpg");
            _heroIconDic.Add("H006", @"/Content/icons/BTNBerserker.jpg");
            _heroIconDic.Add("H007", @"/Content/icons/BTNDarkSaber.jpg");
            _heroIconDic.Add("H00Y", @"/Content/icons/BTNDrake.jpg");
            _heroIconDic.Add("H00I", @"/Content/icons/BTNGawain.jpg");
            _heroIconDic.Add("H009", @"/Content/icons/BTNGilgamesh.jpg");
            _heroIconDic.Add("E002", @"/Content/icons/BTNGilles.jpg");
            _heroIconDic.Add("H00A", @"/Content/icons/BTNIskander.jpg");
            _heroIconDic.Add("H021", @"/Content/icons/BTNKarna.jpg");
            _heroIconDic.Add("H03M", @"/Content/icons/BTNLancelot.jpg");
            _heroIconDic.Add("H01X", @"/Content/icons/BTNNero.jpg");
            _heroIconDic.Add("H024", @"/Content/icons/BTNNursery.jpg");
            _heroIconDic.Add("H003", @"/Content/icons/BTNRider.jpg");
            _heroIconDic.Add("H01T", @"/Content/icons/BTNRobin.jpg");
            _heroIconDic.Add("H00E", @"/Content/icons/BTNTamamo.jpg");
            _heroIconDic.Add("H008", @"/Content/icons/BTNTrueAssassin.jpg");
            _heroIconDic.Add("H01F", @"/Content/icons/BTNVlad.jpg");
            _heroIconDic.Add("H01Q", @"/Content/icons/BTNYeopo.jpg");
            _heroIconDic.Add("H04D", @"/Content/icons/BTNZeroLancer.jpg");
            _heroIconDic.Add("H01A", @"/Content/icons/BTNLi.jpg");
        }
        public static string GetHeroIconURL(string heroUnitTypeId)
        {
            string url = "";
            _heroIconDic.TryGetValue(heroUnitTypeId, out url);
            return url;
        }

        public static string GetLocalReplayPath(string replayPath)
        {
            return "Content\\replays\\" + Path.GetFileName(replayPath);
        }
    }
}
