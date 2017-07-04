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
-- Table structure for table `herotypename`
--

DROP TABLE IF EXISTS `herotypename`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `herotypename` (
  `HeroTypeNameID` int(11) NOT NULL AUTO_INCREMENT,
  `FK_HeroTypeID` int(11) NOT NULL,
  `Language` enum('KR','EN') NOT NULL,
  `HeroName` varchar(20) NOT NULL,
  `HeroTitle` varchar(45) NOT NULL,
  PRIMARY KEY (`HeroTypeNameID`,`FK_HeroTypeID`),
  KEY `fk_HeroTypeName_HeroType1_idx` (`FK_HeroTypeID`),
  CONSTRAINT `fk_HeroTypeName_HeroType1` FOREIGN KEY (`FK_HeroTypeID`) REFERENCES `herotype` (`HeroTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `herotypename`
--

LOCK TABLES `herotypename` WRITE;
/*!40000 ALTER TABLE `herotypename` DISABLE KEYS */;
INSERT INTO `herotypename` VALUES (1,1,'EN','Saber (5th)','King of Knights'),(2,4,'EN','Caster (Extra)','A Tale for Somebody\'s Sake'),(3,5,'EN','Avenger','All The World\'s Evils'),(4,16,'EN','Lancer (5th)','Prince of Light'),(5,18,'EN','Caster (5th)','Witch of the Ancient Ages'),(6,7,'EN','Archer (5th)','Counter Guardian'),(7,23,'EN','Rider (5th)','Monsterous Queen'),(8,24,'EN','Assassin (5th)','Mysterious Swordsman'),(9,11,'EN','Berserker (5th)','Greatest Warrior of Greece'),(10,26,'EN','Saber Alter','Tainted King of Knights'),(11,10,'EN','True Assassin','Genesis of the Middle East'),(12,13,'EN','Archer (4th)','King of Heroes'),(13,6,'EN','Rider (4th)','Conqueror of Macedonia'),(14,25,'EN','Caster (Extra)','Nine-Tailed Fox'),(15,21,'EN','Saber (Extra)','Knight of the Sun'),(16,29,'EN','Rider (Extra)','Voyager of the Storm'),(17,22,'EN','Assassin (Extra)','Master of Bajiquan'),(18,12,'EN','Lancer (Extra)','Innocent Monster'),(19,28,'EN','Berserker (Extra)','Traitorous General'),(20,8,'EN','Archer (Extra)','Faceless King'),(21,27,'EN','Saber (Extra)','Origin of the Flames'),(22,19,'EN','Lancer (Extra)','Merciful Saint'),(23,20,'EN','Berserkser (4th)','Knight of the Lake'),(24,9,'EN','Lancer (4th)','First Spear of the Fianna Knights'),(25,30,'EN','Caster (4th)','BlueBeard'),(26,31,'EN','Lancer (Extra)','Dragon\'s Daughter');
/*!40000 ALTER TABLE `herotypename` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-07 23:03:38
