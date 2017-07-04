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
-- Table structure for table `iteminfo`
--

DROP TABLE IF EXISTS `iteminfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `iteminfo` (
  `ItemID` int(11) NOT NULL,
  `ItemTypeID` varchar(4) NOT NULL,
  `ItemName` varchar(45) NOT NULL,
  `ItemCost` int(11) NOT NULL,
  PRIMARY KEY (`ItemID`),
  UNIQUE KEY `ItemTypeID_UNIQUE` (`ItemTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `iteminfo`
--

LOCK TABLES `iteminfo` WRITE;
/*!40000 ALTER TABLE `iteminfo` DISABLE KEYS */;
INSERT INTO `iteminfo` VALUES (1,'I00H','Blink Scroll',100),(2,'I00E','Blink Scroll 1.5x',150),(3,'I01A','Combination Scroll',300),(4,'I019','Combination Scroll 1.5x',450),(5,'I011','Dust of Navigation',1500),(6,'I012','Dust of Navigation 1.5x',2250),(7,'I002','Familiar',300),(8,'I005','Familiar 1.5x',450),(9,'I00A','Gem of Speed',300),(10,'I00D','Gem of Speed 1.5x',450),(11,'I00M','Mass Teleportation Scroll',1500),(12,'I00X','Mass Teleportation Scroll 1.5x',2250),(13,'I000','Rank C Magic Scroll',150),(14,'I00T','Rank C Magic Scroll 1.5x',225),(15,'I003','Sentry Ward',900),(16,'I00N','Sentry Ward 1.5x',1350),(17,'I00R','Spirit Link Scroll',1500),(18,'I00G','Spirit Link Scroll 1.5x',2250),(19,'I00B','Teleportation Scroll',500),(20,'I00U','Teleportation Scroll 1.5x',750),(21,'I00I','Red Potion',800),(22,'I00F','Red Potion',1200),(23,'vamp','Berserk Potion',800),(24,'I00W','Berserk Potion 1.5x',1200),(25,'SWAR','Sentry Ward (Familiar)',800);
/*!40000 ALTER TABLE `iteminfo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-07 23:03:13
