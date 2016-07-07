using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;
using Fate.DB.DAL;

namespace Fate.WebServiceLayer
{
    public class GameDetailSL
    {
        public GameDetailData GetGameDetail(int gameId)
        {
            GameDetailData dataModel = new GameDetailData();
            GameDetailDAL detailDal = new GameDetailDAL();
            List<GamePlayerDetailData> gameDetailData = detailDal.GetGameDetails(gameId);
            dataModel.Team1Data = gameDetailData.Where(x => x.Team == "1").ToList();
            dataModel.Team2Data = gameDetailData.Where(x => x.Team == "2").ToList();
            foreach (GamePlayerDetailData data in dataModel.Team1Data)
            {
                dataModel.Team1Kills += data.Kills;
                dataModel.Team1Deaths += data.Deaths;
                dataModel.Team1Assists += data.Assists;
                data.HeroImageURL = ContentURL.GetHeroIconURL(data.HeroUnitTypeID);
            }
            foreach (GamePlayerDetailData data in dataModel.Team2Data)
            {
                dataModel.Team2Kills += data.Kills;
                dataModel.Team2Deaths += data.Deaths;
                dataModel.Team2Assists += data.Assists;
                data.HeroImageURL = ContentURL.GetHeroIconURL(data.HeroUnitTypeID);
            }
            return dataModel;
        }

    }
}
