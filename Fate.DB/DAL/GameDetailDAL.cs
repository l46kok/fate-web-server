﻿using System;
using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;

namespace Fate.DB.DAL
{
    public class GameDetailDAL
    {
        public List<GamePlayerDetailData> GetGameDetails(int gameId)
        {
            using (frsDb db = frsDb.Create())
            {
                //EF Produces monsterous query on this
                //Perhaps in the future, we need to conver it into raw SQL
                var gameDetailQueryData = (
                    from gameplayerdetail in db.gameplayerdetail
                    join game in db.game on gameplayerdetail.FK_GameID equals game.GameID
                    join heroType in db.herotype on gameplayerdetail.FK_HeroTypeID equals heroType.HeroTypeID
                    join player in db.player on gameplayerdetail.FK_PlayerID equals player.PlayerID
                    join godshelpuse in db.godshelpuse on gameplayerdetail.GamePlayerDetailID equals godshelpuse.FK_GamePlayerDetailID into a
                    from ghUseL in a.DefaultIfEmpty() 
                    join godshelpinfo in db.godshelpinfo on ghUseL.FK_GodsHelpInfoID equals godshelpinfo.GodsHelpInfoID into b
                    from ghInfoL in b.DefaultIfEmpty() //Left outer join in EF... seriously?
                    where (gameplayerdetail.FK_GameID == gameId)
                    group new { game = gameplayerdetail, heroType, player, ghUseL, ghInfoL} 
                    by new
                    {
                        game.GameID,
                        game.TeamOneWinCount,
                        game.TeamTwoWinCount,
                        gameplayerdetail.GamePlayerDetailID,
                        gameplayerdetail.Kills,
                        gameplayerdetail.Assists,
                        gameplayerdetail.Deaths,
                        gameplayerdetail.Team,
                        gameplayerdetail.GoldSpent,
                        gameplayerdetail.DamageDealt,
                        gameplayerdetail.DamageTaken,
                        gameplayerdetail.HeroLevel,
                        player.PlayerName,
                        heroType.HeroUnitTypeID,
                    } into g
                    select new GamePlayerDetailData()
                    {
                        GameID = g.Key.GameID,
                        TeamOneWinCount = g.Key.TeamOneWinCount,
                        TeamTwoWinCount = g.Key.TeamTwoWinCount,
                        PlayerName = g.Key.PlayerName,
                        HeroUnitTypeID = g.Key.HeroUnitTypeID,
                        Kills = g.Key.Kills,
                        Assists = g.Key.Assists,
                        Deaths = g.Key.Deaths,
                        Team = g.Key.Team,
                        Level = g.Key.HeroLevel,
                        GoldSpent = g.Key.GoldSpent,
                        DamageDealt = g.Key.DamageDealt,
                        DamageTaken = g.Key.DamageTaken,
                        GodsHelpAbilIDList = g.Select(x=>x.ghInfoL.GodsHelpAbilID).ToList()
                    });
                return gameDetailQueryData.ToList();
            }
        }

        public PlayerGameBuildData GetPlayerGameBuildDetail(string playerName, int gameId)
        {
            using (frsDb db = frsDb.Create())
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
