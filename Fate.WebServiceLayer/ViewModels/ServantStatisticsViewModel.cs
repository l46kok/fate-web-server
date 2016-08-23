namespace Fate.WebServiceLayer.ViewModels
{
    public class ServantStatisticsViewModel
    {
        public string HeroImageURL { get; set; }
        public string HeroName { get; set; }
        public string HeroTitle { get; set; }
        public double WinRatioVal { get; set; }
        public string WinRatio { get; set; }
        public int PlayCount { get; set; }
        public string AvgKDA { get; set; }
        public string KDAColor { get; set; }
        public string AvgGoldSpent { get; set; }
        public string AvgDamageDealt { get; set; }
        public string AvgDamageTaken { get; set; }
    }
}
