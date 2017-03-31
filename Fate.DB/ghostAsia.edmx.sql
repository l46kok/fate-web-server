










































-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/27/2017 03:14:20

-- Generated from EDMX file: D:\SourceCodes\Personal\fate-web-server\Fate.DB\ghostAsia.edmx
-- Target version: 3.0.0.0

-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;

    DROP TABLE IF EXISTS `admins`;

    DROP TABLE IF EXISTS `bans`;

    DROP TABLE IF EXISTS `dotagames`;

    DROP TABLE IF EXISTS `dotaplayers`;

    DROP TABLE IF EXISTS `downloads`;

    DROP TABLE IF EXISTS `gameplayers`;

    DROP TABLE IF EXISTS `games`;

    DROP TABLE IF EXISTS `kicks`;

    DROP TABLE IF EXISTS `phrases`;

    DROP TABLE IF EXISTS `plugindb`;

    DROP TABLE IF EXISTS `scores`;

    DROP TABLE IF EXISTS `users`;

    DROP TABLE IF EXISTS `w3mmdplayers`;

    DROP TABLE IF EXISTS `w3mmdvars`;

SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------


CREATE TABLE `admins`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`name` varchar (15) NOT NULL, 
	`server` varchar (100) NOT NULL);

ALTER TABLE `admins` ADD PRIMARY KEY (id);





CREATE TABLE `bans`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`server` varchar (100) NOT NULL, 
	`name` varchar (15) NOT NULL, 
	`ip` varchar (15) NOT NULL, 
	`date` datetime NOT NULL, 
	`gamename` varchar (31) NOT NULL, 
	`admin` varchar (15) NOT NULL, 
	`reason` varchar (255) NOT NULL);

ALTER TABLE `bans` ADD PRIMARY KEY (id);





CREATE TABLE `dotagames`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`gameid` int NOT NULL, 
	`winner` int NOT NULL, 
	`min` int NOT NULL, 
	`sec` int NOT NULL);

ALTER TABLE `dotagames` ADD PRIMARY KEY (id);





CREATE TABLE `dotaplayers`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`gameid` int NOT NULL, 
	`colour` int NOT NULL, 
	`kills` int NOT NULL, 
	`deaths` int NOT NULL, 
	`creepkills` int NOT NULL, 
	`creepdenies` int NOT NULL, 
	`assists` int NOT NULL, 
	`gold` int NOT NULL, 
	`neutralkills` int NOT NULL, 
	`item1` varchar (4) NOT NULL, 
	`item2` varchar (4) NOT NULL, 
	`item3` varchar (4) NOT NULL, 
	`item4` varchar (4) NOT NULL, 
	`item5` varchar (4) NOT NULL, 
	`item6` varchar (4) NOT NULL, 
	`hero` varchar (4) NOT NULL, 
	`newcolour` int NOT NULL, 
	`towerkills` int NOT NULL, 
	`raxkills` int NOT NULL, 
	`courierkills` int NOT NULL);

ALTER TABLE `dotaplayers` ADD PRIMARY KEY (id);





CREATE TABLE `downloads`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`map` varchar (100) NOT NULL, 
	`mapsize` int NOT NULL, 
	`datetime` datetime NOT NULL, 
	`name` varchar (15) NOT NULL, 
	`ip` varchar (15) NOT NULL, 
	`spoofed` int NOT NULL, 
	`spoofedrealm` varchar (100) NOT NULL, 
	`downloadtime` int NOT NULL);

ALTER TABLE `downloads` ADD PRIMARY KEY (id);





CREATE TABLE `kicks`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`server` varchar (100) NOT NULL, 
	`name` varchar (15) NOT NULL, 
	`ip` varchar (15) NOT NULL, 
	`date` datetime NOT NULL, 
	`admin` varchar (15) NOT NULL, 
	`reason` varchar (150) NOT NULL);

ALTER TABLE `kicks` ADD PRIMARY KEY (id);





CREATE TABLE `phrases`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`type` varchar (100) NOT NULL, 
	`phrase` varchar (150) NOT NULL);

ALTER TABLE `phrases` ADD PRIMARY KEY (id);





CREATE TABLE `plugindb`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`plugin` varchar (16), 
	`k` varchar (128), 
	`val` varchar (128));

ALTER TABLE `plugindb` ADD PRIMARY KEY (id);





CREATE TABLE `scores`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`category` varchar (25) NOT NULL, 
	`name` varchar (15) NOT NULL, 
	`server` varchar (100) NOT NULL, 
	`score` double NOT NULL);

ALTER TABLE `scores` ADD PRIMARY KEY (id);





CREATE TABLE `users`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`username` varchar (15) NOT NULL, 
	`properusername` varchar (15) NOT NULL, 
	`uid` int NOT NULL, 
	`rank` int NOT NULL, 
	`ipaddress` varchar (15) NOT NULL, 
	`lastseen` varchar (31) NOT NULL, 
	`promotedby` varchar (15) NOT NULL, 
	`unbannedby` varchar (15) NOT NULL);

ALTER TABLE `users` ADD PRIMARY KEY (id);





CREATE TABLE `w3mmdplayers`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`category` varchar (25) NOT NULL, 
	`gameid` int NOT NULL, 
	`pid` int NOT NULL, 
	`name` varchar (15) NOT NULL, 
	`flag` varchar (32) NOT NULL, 
	`leaver` int NOT NULL, 
	`practicing` int NOT NULL);

ALTER TABLE `w3mmdplayers` ADD PRIMARY KEY (id);





CREATE TABLE `w3mmdvars`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`gameid` int NOT NULL, 
	`pid` int NOT NULL, 
	`varname` varchar (25) NOT NULL, 
	`value_int` int, 
	`value_real` double, 
	`value_string` varchar (100));

ALTER TABLE `w3mmdvars` ADD PRIMARY KEY (id);





CREATE TABLE `gameplayers`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`gameid` int NOT NULL, 
	`name` varchar (15) NOT NULL, 
	`ip` varchar (15) NOT NULL, 
	`spoofed` int NOT NULL, 
	`reserved` int NOT NULL, 
	`loadingtime` int NOT NULL, 
	`left` int NOT NULL, 
	`leftreason` varchar (100) NOT NULL, 
	`team` int NOT NULL, 
	`colour` int NOT NULL, 
	`spoofedrealm` varchar (100) NOT NULL);

ALTER TABLE `gameplayers` ADD PRIMARY KEY (id);





CREATE TABLE `games`(
	`id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`botid` int NOT NULL, 
	`server` varchar (100) NOT NULL, 
	`map` varchar (100) NOT NULL, 
	`datetime` datetime NOT NULL, 
	`gamename` varchar (31) NOT NULL, 
	`ownername` varchar (15) NOT NULL, 
	`duration` int NOT NULL, 
	`gamestate` int NOT NULL, 
	`creatorname` varchar (15) NOT NULL, 
	`creatorserver` varchar (100) NOT NULL);

ALTER TABLE `games` ADD PRIMARY KEY (id);







-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
