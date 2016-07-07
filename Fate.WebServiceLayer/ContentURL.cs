using System;
using System.Collections.Generic;
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
            _heroIconDic.Add("H000",@"/Content/icons/BTNSaber.jpg");
            _heroIconDic.Add("H002",@"/Content/icons/BTNChulainn.jpg");
            _heroIconDic.Add("H004", @"/Content/icons/BTNCaster.jpg");
        }
        public static string GetHeroIconURL(string heroUnitTypeId)
        {
            string url = "";
            _heroIconDic.TryGetValue(heroUnitTypeId, out url);
            return url;
        }
    }
}
