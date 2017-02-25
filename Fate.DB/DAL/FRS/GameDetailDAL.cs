using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Fate.Common.Data;
using MySql.Data.MySqlClient;

namespace Fate.DB.DAL
{
    public class GameDetailDAL
    {
        public List<GamePlayerDetailData> GetGameDetails(int gameId)
        {
            using (frsDatabase db = frsDatabase.Create())
            {
                const string sql = @"SELECT A.GameID, 
	                                   A.TeamOneWinCount, 
                                       A.TeamTwoWinCount, 
                                       D.PlayerName, 
                                       B.Kills, 
                                       B.Deaths, 
                                       B.Assists, 
                                       B.HeroLevel AS Level, 
                                       B.Team,
                                       B.DamageDealt,
                                       B.DamageTaken,
                                       B.GoldSpent,
                                       C.HeroUnitTypeID, 
                                       group_concat(F.GodsHelpAbilID separator ',') AS GodsHelpAbilIDConcat
                                FROM Game A
                                JOIN GamePlayerDetail B
	                                ON A.GameID = B.FK_GameID
                                JOIN HeroType C
	                                ON B.FK_HeroTypeID = C.HeroTypeID
                                JOIN Player D
	                                ON B.FK_PlayerID = D.PlayerID
                                LEFT OUTER JOIN GodsHelpUse E
	                                ON B.GamePlayerDetailID = E.FK_GamePlayerDetailID
                                LEFT OUTER JOIN GodsHelpInfo F
	                                ON E.FK_GodsHelpInfoID = F.GodsHelpInfoID
                                WHERE A.GameID = @GameID
                                GROUP BY A.GameID, A.TeamOneWinCount, A.TeamTwoWinCount, D.PlayerName, B.Kills, B.Deaths, B.Assists, B.HeroLevel, C.HeroUnitTypeID;";

                List<GamePlayerDetailData> gamePlayerDetailData = db.Database.SqlQuery<GamePlayerDetailData>(sql, new MySqlParameter("GameID",gameId)).ToList();
                return gamePlayerDetailData;
            }
        }

        public PlayerGameBuildData GetPlayerGameBuildDetail(string playerName, int gameId)
        {
            using (frsDatabase db = frsDatabase.Create())
            {
                //Find gamePlayerDetailId first
                int gamePlayerDetailId = (
                    from game in db.game
                    join gameDetail in db.gameplayerdetail on game.GameID equals gameDetail.FK_GameID
                    join player in db.player on gameDetail.FK_PlayerID equals player.PlayerID
                    where game.GameID == gameId && player.PlayerName == playerName
                    select gameDetail.GamePlayerDetailID).FirstOrDefault();

                if (gamePlayerDetailId == 0)
                    return null;

                //Find HeroUnitTypeId
                string heroTypeUnitId = (
                    from herotype in db.herotype
                    join gameDetail in db.gameplayerdetail on herotype.HeroTypeID equals gameDetail.FK_HeroTypeID
                    where gameDetail.GamePlayerDetailID == gamePlayerDetailId
                    select herotype.HeroUnitTypeID).First();

                PlayerGameBuildData buildData = new PlayerGameBuildData();
                buildData.HeroUnitTypeId = heroTypeUnitId;

                //Find stat learn information
                var heroStatLearnQuery = (
                    from herostatinfo in db.herostatinfo
                    join herostatlearn in db.herostatlearn on herostatinfo.HeroStatInfoID equals
                        herostatlearn.FK_HeroStatInfoID
                    where herostatlearn.FK_GamePlayerDetailID == gamePlayerDetailId
                    select new
                    {
                        herostatinfo.HeroStatAbilID,
                        herostatlearn.LearnCount
                    });

                buildData.StatBuildDic = new Dictionary<string, int>();
                foreach (var heroStat in heroStatLearnQuery)
                {
                    buildData.StatBuildDic.Add(heroStat.HeroStatAbilID,heroStat.LearnCount);
                }

                //Find attribute information
                var heroAttributeQuery = (
                    from attributeinfo in db.attributeinfo
                    join attributelearn in db.attributelearn on attributeinfo.AttributeInfoID equals attributelearn.FK_AttributeInfoID
                    where attributelearn.FK_GamePlayerDetailID == gamePlayerDetailId
                    select new
                    {
                        attributeinfo.AttributeAbilID,
                        attributeinfo.AttributeName
                    });

                buildData.LearnedAttributeList = new List<Tuple<string, string>>();
                foreach (var attribute in heroAttributeQuery)
                {
                    Tuple<string,string> attrTuple = new Tuple<string, string>(attribute.AttributeAbilID,attribute.AttributeName);
                    buildData.LearnedAttributeList.Add(attrTuple);
                }

                //Find Ward/Familiar Information
                var itemKeys = new string[] { "I003", "I00N", "SWAR", "I002", "I005" };
                
                var wardFamiliarQuery = (
                    from iteminfo in db.iteminfo
                    join itembuy in db.gameitempurchase on iteminfo.ItemID equals itembuy.FK_ItemID
                    where itembuy.FK_GamePlayerDetailID == gamePlayerDetailId && itemKeys.Contains(iteminfo.ItemTypeID)
                    select new
                    {
                        iteminfo.ItemTypeID,
                        itembuy.ItemPurchaseCount
                    });

                buildData.WardFamiliarDic = new Dictionary<string, int>();
                foreach (var item in wardFamiliarQuery)
                {
                    buildData.WardFamiliarDic.Add(item.ItemTypeID, item.ItemPurchaseCount);
                }

                //Find CommandSeal Info
                var commandSealKeys = new string[] { "A05Q", "A094", "A043", "A044"};

                var commandSealQuery = (
                    from commandseal in db.commandsealuse
                    where commandseal.FK_GamePlayerDetailID == gamePlayerDetailId && commandSealKeys.Contains(commandseal.CommandSealAbilID)
                    select new
                    {
                        commandseal.CommandSealAbilID,
                        commandseal.UseCount
                    });

                buildData.CommandSealDic = new Dictionary<string, int>();
                foreach (var cs in commandSealQuery)
                {
                    buildData.CommandSealDic.Add(cs.CommandSealAbilID,cs.UseCount);
                }

                return buildData;
            }
        }
    }
}
