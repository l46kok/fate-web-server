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
-- Table structure for table `attributeinfo`
--

DROP TABLE IF EXISTS `attributeinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attributeinfo` (
  `AttributeInfoID` int(11) NOT NULL AUTO_INCREMENT,
  `AttributeAbilID` varchar(4) NOT NULL,
  `AttributeName` varchar(60) NOT NULL,
  PRIMARY KEY (`AttributeInfoID`)
) ENGINE=InnoDB AUTO_INCREMENT=170 DEFAULT CHARSET=utf8 COMMENT='	';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attributeinfo`
--

LOCK TABLES `attributeinfo` WRITE;
/*!40000 ALTER TABLE `attributeinfo` DISABLE KEYS */;
INSERT INTO `attributeinfo` VALUES (47,'A0GO','Sphere Boundary'),(48,'A0GP','Fierce Tiger Forcibly Climbs a Mountain'),(49,'A0GF','Circulatory Shock'),(51,'A0GG','Double Class'),(52,'A077','Improved Territory'),(53,'A082','Divine Language'),(54,'A07R','Improved Hecatic Graea'),(55,'A07H','Improved Aegis'),(56,'A07B','Improved Gae Bolg'),(57,'A07L','Flying Spear of Barbed Death'),(58,'A07V','Spear of Death'),(59,'A0GL','Improved Runes'),(60,'A071','Improved Battle Continuation'),(61,'A08K','Protection of Fairies'),(62,'A08M','Honor of the Shining Lake'),(64,'A08J','Eternal Arms Mastership'),(65,'A08L','Improved Arondight'),(66,'A081','Phantom Attack'),(67,'A0H0','Delusional Illusion'),(68,'A07Q','Zabaniya'),(69,'A07G','Protection from Wind'),(70,'A07A','Riding'),(71,'A07K','Seal'),(72,'A07U','Monstrous Strength'),(73,'A070','Improved Mystic Eyes'),(74,'A074','Improved Clairvoyance'),(75,'A07E','Hrunting'),(76,'A07Y','Improved Tracing'),(77,'A07I','Mana Blast'),(78,'A078','Improved Mana Shroud'),(79,'A083','Black Light - Dark Excalibur'),(80,'A0BV','Improved Ferocity'),(81,'A0AC','Blooming of Yellow-Red Rose'),(82,'A0AE','Double Spear'),(83,'A073','Improved Excalibur'),(84,'A0FZ','Strike Air'),(85,'A07D','Improved Instincts'),(86,'A0K9','Dragon Skillet'),(87,'A0KC','Rainbow Plains and the Maze of Mirrors'),(88,'A0KD','The King of Black and White Checkerboard'),(89,'A0D4',''),(90,'A0D3','Improved Black Magic'),(91,'A0CY','Improved Demonic Creature of the Ocean Depths'),(92,'A0CV','Contagion'),(93,'A0J2','Key to the Theater'),(94,'A0IW','Embryonic Flame'),(95,'A0IX','Imperial Privilege'),(96,'A0J0','Thrice, Though I Welcome the Setting Sun'),(97,'A0JS','Improved Vasavi Shakti'),(98,'A0JQ','Improved Divinity'),(99,'A0JR','Improved Brahmastra'),(100,'A0I9','Sacramentum of Druid'),(101,'A0I8','Sherwood Forest'),(102,'A0I6','One Drop of a Viper'),(103,'A0I7','Poisoned Arrow'),(104,'A07N','Knighthood'),(105,'A07X','Improved Charisma'),(106,'A06F','Improved Ionion Hetairoi'),(107,'A053','Improved Charisma'),(108,'A046','Improved Military Tactics'),(109,'A0BP','Eye for Art'),(110,'A07Z','Quick Draw'),(111,'A07P','Eye of Serenity'),(112,'A09O','Monohoshi Zao'),(113,'A075','Eye of the Mind'),(114,'A06C','Nightmare'),(115,'A060','Blood Metastasis'),(116,'A0BQ','Avenger of Blood'),(117,'A066','Tawrich and Zarich'),(118,'A0BM','Spirit Orb'),(119,'A0C8','Witchcraft'),(120,'A0BZ','Cursed Charms'),(122,'A0C1','Improved Territory'),(123,'A0KA','Ackroyd in Celluloid'),(124,'A0JT','Prana Burst'),(125,'A00N','Kansho Bakuya Overedge'),(126,'A0I5','The Faceless King'),(127,'A05N','Wheel of Gordias'),(128,'A05F','Improved Spatha'),(129,'A000','Improved Divinity'),(130,'A07J','Improved Sword Rain'),(131,'A07T','The Star of Creation that Split Heaven and Earth'),(132,'A0ED','Sword of Victory'),(133,'A0EC','Numeral of the Saint'),(134,'A072','Sacred Mirror'),(135,'A07F','Ganryu - Shore Willow'),(136,'A06S','Improved Golden Rule'),(137,'A079','Power of Sumer'),(138,'A07W','Reincarnation'),(139,'A07M','God Hand'),(140,'A0J1','Improved Fountain of the Saint'),(141,'A0HF','Houtengageki'),(142,'A0HL','Bravery'),(143,'A0HK','Gate Halberd Shot'),(144,'A0FE','Combustion Shot'),(145,'A0FG','Improved Golden Hind'),(146,'A0FH','Golden Rule : Pillage'),(147,'A0EB','Knighthood'),(148,'A0EA','Protection of the Fairies'),(149,'A07C','Mad Enhancement'),(150,'A0E9','Gwalhmai'),(151,'A07O','Shroud of Martin'),(152,'A0FD','Logbook'),(153,'A0AT','Lord of Execution'),(154,'A0B2','Rebellious Intent'),(155,'A0AK','Innocent Monster'),(156,'A0AS','Protection of the Faith'),(157,'A0HE','Immortal Red Hare'),(158,'A0HD','Red Hare'),(159,'A0KB','Vorpal Blade'),(160,'A0JP','Uncrowned Arms Mastership'),(161,'A0AD','Mind\'s Eye'),(162,'A076','Improved Presence Concealment'),(163,'A069','Curse of Blood'),(164,'A08P','Improved Love Spot of Seduction'),(165,'A0LD','Aspect of Dragon'),(166,'A0LF','Double Class'),(167,'A0LE','Innocent Monster'),(168,'A0LC','Sadistic Charisma'),(169,'A0LK','Torture Techniques');
/*!40000 ALTER TABLE `attributeinfo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-07 23:03:16
