-- MySQL dump 10.13  Distrib 5.7.12, for Win32 (AMD64)
--
-- Host: 54.210.38.182    Database: frs
-- ------------------------------------------------------
-- Server version	5.7.13-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ban`
--

DROP TABLE IF EXISTS `ban`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ban` (
  `BanID` int(11) NOT NULL AUTO_INCREMENT,
  `PlayerName` varchar(15) NOT NULL,
  `Reason` text NOT NULL,
  `BannedDateTime` datetime NOT NULL,
  `Admin` varchar(45) NOT NULL,
  `BannedUntil` datetime DEFAULT NULL,
  `IsPermanentBan` tinyint(1) NOT NULL,
  `IsCurrentlyBanned` tinyint(1) NOT NULL,
  `IpAddresses` text NOT NULL,
  `ModifiedDateTime` datetime DEFAULT NULL,
  `ModifiedByAdmin` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`BanID`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ban`
--

LOCK TABLES `ban` WRITE;
/*!40000 ALTER TABLE `ban` DISABLE KEYS */;
INSERT INTO `ban` VALUES (44,'pinoy_batista','Guinea pig','2017-03-06 11:45:22','Jiggy','2017-03-06 12:54:22',0,0,'','2017-03-06 12:54:37','Timer Unban'),(45,'GodBoom','RQ','2017-03-12 00:15:37','Jiggy','2017-03-12 16:55:37',0,0,'','2017-03-12 16:55:56','Timer Unban'),(46,'kimbokyem','RQ','2017-03-13 01:22:54','launcelot','2017-03-13 03:22:54',0,0,'118.32.218.42','2017-03-13 03:22:58','Timer Unban'),(47,'mr.black_ar','Trolling','2017-03-13 01:23:57','launcelot','2017-03-13 03:23:57',0,0,'','2017-03-13 03:23:58','Timer Unban'),(48,'Juicy','intentionally trolling with pause and cursing on admin for no reason','2017-03-13 03:20:12','launcelot','2017-03-14 03:20:12',0,0,'211.54.186.95','2017-03-14 03:21:00','Timer Unban'),(49,'loeq91812','rq','2017-03-13 12:54:14','launcelot','2017-03-13 13:54:14',0,0,'','2017-03-13 13:55:02','Timer Unban'),(50,'Lv1Archer','Press UBW and leave as if he\'s cosplaying as DunBlameMe','2017-03-20 09:14:34','Elysium','2017-03-21 13:14:34',0,0,'','2017-03-21 13:14:36','Timer Unban'),(51,'motherless','RQ for 2 times','2017-03-25 07:50:08','Misa','2017-03-25 08:20:08',0,0,'175.115.41.48','2017-03-25 08:20:11','Timer Unban'),(52,'m3lted','RQ','2017-03-28 10:02:09','Misa','2017-03-28 10:47:09',0,0,'','2017-03-28 10:47:32','Timer Unban');
/*!40000 ALTER TABLE `ban` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-07 23:03:39
