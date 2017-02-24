










































-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/24/2017 00:25:54

-- Generated from EDMX file: D:\SourceCodes\Personal\fate-web-server\Fate.DB\frsdb.edmx
-- Target version: 3.0.0.0

-- --------------------------------------------------


DROP DATABASE IF EXISTS `frs`;
CREATE DATABASE `frs`;
USE `frs`;


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;

    DROP TABLE IF EXISTS `webusers`;

SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------


CREATE TABLE `attributeinfo`(
	`AttributeInfoID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`AttributeAbilID` varchar (4) NOT NULL, 
	`AttributeName` varchar (60) NOT NULL);

ALTER TABLE `attributeinfo` ADD PRIMARY KEY (AttributeInfoID);





CREATE TABLE `attributelearn`(
	`AttributeLearnID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_AttributeInfoID` int NOT NULL, 
	`FK_GamePlayerDetailID` int NOT NULL);

ALTER TABLE `attributelearn` ADD PRIMARY KEY (AttributeLearnID);





CREATE TABLE `commandsealuse`(
	`CommandSealUseID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`CommandSealAbilID` varchar (4) NOT NULL, 
	`UseCount` int NOT NULL, 
	`FK_GamePlayerDetailID` int NOT NULL);

ALTER TABLE `commandsealuse` ADD PRIMARY KEY (CommandSealUseID);





CREATE TABLE `game`(
	`GameID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_ServerID` int NOT NULL, 
	`GameName` varchar (65535) NOT NULL, 
	`PlayedDate` datetime NOT NULL, 
	`Duration` time NOT NULL, 
	`Result` varchar (65532) NOT NULL, 
	`MapVersion` varchar (65535) NOT NULL, 
	`ReplayUrl` varchar (65535), 
	`MatchType` varchar (65532), 
	`Log` mediumtext, 
	`TeamOneWinCount` int NOT NULL, 
	`TeamTwoWinCount` int NOT NULL);

ALTER TABLE `game` ADD PRIMARY KEY (GameID);





CREATE TABLE `gameitempurchase`(
	`GameItemPurchaseID` int NOT NULL, 
	`FK_ItemID` int NOT NULL, 
	`FK_GamePlayerDetailID` int NOT NULL, 
	`ItemPurchaseCount` int NOT NULL);

ALTER TABLE `gameitempurchase` ADD PRIMARY KEY (GameItemPurchaseID);





CREATE TABLE `gameplayerdetail`(
	`GamePlayerDetailID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_GameID` int NOT NULL, 
	`FK_PlayerID` int NOT NULL, 
	`FK_ServerID` int NOT NULL, 
	`FK_HeroTypeID` int NOT NULL, 
	`Kills` int NOT NULL, 
	`Deaths` int NOT NULL, 
	`Assists` int NOT NULL, 
	`Team` varchar (65532) NOT NULL, 
	`Result` varchar (65532) NOT NULL, 
	`ScoreDiff` int NOT NULL, 
	`ELODiff` int NOT NULL, 
	`GoldSpent` int NOT NULL, 
	`DamageDealt` double NOT NULL, 
	`DamageTaken` double NOT NULL, 
	`HeroLevel` int NOT NULL);

ALTER TABLE `gameplayerdetail` ADD PRIMARY KEY (GamePlayerDetailID, FK_GameID, FK_PlayerID, FK_ServerID, FK_HeroTypeID);





CREATE TABLE `godshelpinfo`(
	`GodsHelpInfoID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`GodsHelpAbilID` varchar (4) NOT NULL, 
	`GodsHelpName` varchar (30) NOT NULL);

ALTER TABLE `godshelpinfo` ADD PRIMARY KEY (GodsHelpInfoID);





CREATE TABLE `godshelpuse`(
	`GodsHelpUseID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_GodsHelpInfoID` int NOT NULL, 
	`FK_GamePlayerDetailID` int NOT NULL);

ALTER TABLE `godshelpuse` ADD PRIMARY KEY (GodsHelpUseID);





CREATE TABLE `herostatinfo`(
	`HeroStatInfoID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`HeroStatAbilID` varchar (4) NOT NULL, 
	`HeroStatName` varchar (45) NOT NULL);

ALTER TABLE `herostatinfo` ADD PRIMARY KEY (HeroStatInfoID);





CREATE TABLE `herostatlearn`(
	`HeroStatLearnID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_HeroStatInfoID` int NOT NULL, 
	`FK_GamePlayerDetailID` int NOT NULL, 
	`LearnCount` int NOT NULL);

ALTER TABLE `herostatlearn` ADD PRIMARY KEY (HeroStatLearnID);





CREATE TABLE `herotype`(
	`HeroTypeID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`HeroUnitTypeID` varchar (4) NOT NULL);

ALTER TABLE `herotype` ADD PRIMARY KEY (HeroTypeID);





CREATE TABLE `herotypename`(
	`HeroTypeNameID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_HeroTypeID` int NOT NULL, 
	`Language` varchar (65532) NOT NULL, 
	`HeroName` varchar (20) NOT NULL, 
	`HeroTitle` varchar (45) NOT NULL);

ALTER TABLE `herotypename` ADD PRIMARY KEY (HeroTypeNameID, FK_HeroTypeID);





CREATE TABLE `iteminfo`(
	`ItemID` int NOT NULL, 
	`ItemTypeID` varchar (4) NOT NULL, 
	`ItemName` varchar (45) NOT NULL, 
	`ItemCost` int NOT NULL);

ALTER TABLE `iteminfo` ADD PRIMARY KEY (ItemID);





CREATE TABLE `player`(
	`PlayerID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_ServerID` int NOT NULL, 
	`PlayerName` varchar (15) NOT NULL, 
	`RegDate` datetime NOT NULL, 
	`IsBanned` bool NOT NULL, 
	`LastUpdatedOn` datetime NOT NULL, 
	`LastUpdatedBy` varchar (20) NOT NULL, 
	`BannedOn` datetime, 
	`UnbannedOn` datetime);

ALTER TABLE `player` ADD PRIMARY KEY (PlayerID, FK_ServerID);





CREATE TABLE `playerherostat`(
	`PlayerHeroStatID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_PlayerID` int NOT NULL, 
	`FK_ServerID` int NOT NULL, 
	`FK_HeroTypeID` int NOT NULL, 
	`HeroPlayCount` int NOT NULL, 
	`TotalHeroKills` int NOT NULL, 
	`TotalHeroDeaths` int NOT NULL, 
	`TotalHeroAssists` int NOT NULL);

ALTER TABLE `playerherostat` ADD PRIMARY KEY (PlayerHeroStatID, FK_PlayerID, FK_ServerID, FK_HeroTypeID);





CREATE TABLE `playerstat`(
	`PlayerStatID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_PlayerID` int NOT NULL, 
	`FK_ServerID` int NOT NULL, 
	`Win` int NOT NULL, 
	`Loss` int NOT NULL, 
	`PlayCount` int NOT NULL, 
	`ELO` int NOT NULL, 
	`Score` int NOT NULL);

ALTER TABLE `playerstat` ADD PRIMARY KEY (PlayerStatID, FK_PlayerID, FK_ServerID);





CREATE TABLE `ranking`(
	`RankID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FK_PlayerID` int NOT NULL, 
	`FK_ServerID` int NOT NULL, 
	`PlayerStatID` int NOT NULL, 
	`PlayerRank` int NOT NULL);

ALTER TABLE `ranking` ADD PRIMARY KEY (RankID, FK_PlayerID, FK_ServerID);





CREATE TABLE `server`(
	`ServerID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ServerName` varchar (10) NOT NULL, 
	`IsServiced` bool NOT NULL);

ALTER TABLE `server` ADD PRIMARY KEY (ServerID);





CREATE TABLE `webusers`(
	`WebUserID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`UserName` varchar (20) NOT NULL, 
	`Password` varchar (128) NOT NULL, 
	`IsAdmin` bool NOT NULL, 
	`CreatedOn` datetime NOT NULL, 
	`ModifiedOn` datetime NOT NULL, 
	`Salt` varchar (128) NOT NULL, 
	`EmailAddress` varchar (100) NOT NULL);

ALTER TABLE `webusers` ADD PRIMARY KEY (WebUserID);







-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------


-- Creating foreign key on `FK_ServerID` in table 'game'

ALTER TABLE `game`
ADD CONSTRAINT `fk_Game_Server`
    FOREIGN KEY (`FK_ServerID`)
    REFERENCES `server`
        (`ServerID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_Game_Server'

CREATE INDEX `IX_fk_Game_Server`
    ON `game`
    (`FK_ServerID`);



-- Creating foreign key on `FK_GameID` in table 'gameplayerdetail'

ALTER TABLE `gameplayerdetail`
ADD CONSTRAINT `fk_GamePlayerDetail_Game1`
    FOREIGN KEY (`FK_GameID`)
    REFERENCES `game`
        (`GameID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_GamePlayerDetail_Game1'

CREATE INDEX `IX_fk_GamePlayerDetail_Game1`
    ON `gameplayerdetail`
    (`FK_GameID`);



-- Creating foreign key on `FK_ItemID` in table 'gameitempurchase'

ALTER TABLE `gameitempurchase`
ADD CONSTRAINT `fk_GameItemPurchase_ItemID`
    FOREIGN KEY (`FK_ItemID`)
    REFERENCES `iteminfo`
        (`ItemID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_GameItemPurchase_ItemID'

CREATE INDEX `IX_fk_GameItemPurchase_ItemID`
    ON `gameitempurchase`
    (`FK_ItemID`);



-- Creating foreign key on `FK_HeroTypeID` in table 'gameplayerdetail'

ALTER TABLE `gameplayerdetail`
ADD CONSTRAINT `fk_GamePlayerDetail_HeroType1`
    FOREIGN KEY (`FK_HeroTypeID`)
    REFERENCES `herotype`
        (`HeroTypeID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_GamePlayerDetail_HeroType1'

CREATE INDEX `IX_fk_GamePlayerDetail_HeroType1`
    ON `gameplayerdetail`
    (`FK_HeroTypeID`);



-- Creating foreign key on `FK_ServerID` in table 'gameplayerdetail'

ALTER TABLE `gameplayerdetail`
ADD CONSTRAINT `fk_GamePlayerDetail_Server1`
    FOREIGN KEY (`FK_ServerID`)
    REFERENCES `server`
        (`ServerID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_GamePlayerDetail_Server1'

CREATE INDEX `IX_fk_GamePlayerDetail_Server1`
    ON `gameplayerdetail`
    (`FK_ServerID`);



-- Creating foreign key on `FK_HeroTypeID` in table 'herotypename'

ALTER TABLE `herotypename`
ADD CONSTRAINT `fk_HeroTypeName_HeroType1`
    FOREIGN KEY (`FK_HeroTypeID`)
    REFERENCES `herotype`
        (`HeroTypeID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_HeroTypeName_HeroType1'

CREATE INDEX `IX_fk_HeroTypeName_HeroType1`
    ON `herotypename`
    (`FK_HeroTypeID`);



-- Creating foreign key on `FK_HeroTypeID` in table 'playerherostat'

ALTER TABLE `playerherostat`
ADD CONSTRAINT `fk_PlayerHeroStat_HeroType1`
    FOREIGN KEY (`FK_HeroTypeID`)
    REFERENCES `herotype`
        (`HeroTypeID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_PlayerHeroStat_HeroType1'

CREATE INDEX `IX_fk_PlayerHeroStat_HeroType1`
    ON `playerherostat`
    (`FK_HeroTypeID`);



-- Creating foreign key on `FK_ServerID` in table 'player'

ALTER TABLE `player`
ADD CONSTRAINT `fk_Player_Server`
    FOREIGN KEY (`FK_ServerID`)
    REFERENCES `server`
        (`ServerID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_Player_Server'

CREATE INDEX `IX_fk_Player_Server`
    ON `player`
    (`FK_ServerID`);



-- Creating foreign key on `FK_ServerID` in table 'playerherostat'

ALTER TABLE `playerherostat`
ADD CONSTRAINT `fk_PlayerHeroStat_Server1`
    FOREIGN KEY (`FK_ServerID`)
    REFERENCES `server`
        (`ServerID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_PlayerHeroStat_Server1'

CREATE INDEX `IX_fk_PlayerHeroStat_Server1`
    ON `playerherostat`
    (`FK_ServerID`);



-- Creating foreign key on `FK_ServerID` in table 'playerstat'

ALTER TABLE `playerstat`
ADD CONSTRAINT `fk_PlayerStat_Server1`
    FOREIGN KEY (`FK_ServerID`)
    REFERENCES `server`
        (`ServerID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_PlayerStat_Server1'

CREATE INDEX `IX_fk_PlayerStat_Server1`
    ON `playerstat`
    (`FK_ServerID`);



-- Creating foreign key on `FK_ServerID` in table 'ranking'

ALTER TABLE `ranking`
ADD CONSTRAINT `fk_Ranking_Server1`
    FOREIGN KEY (`FK_ServerID`)
    REFERENCES `server`
        (`ServerID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'fk_Ranking_Server1'

CREATE INDEX `IX_fk_Ranking_Server1`
    ON `ranking`
    (`FK_ServerID`);



-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
