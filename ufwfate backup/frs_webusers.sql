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
-- Table structure for table `webusers`
--

DROP TABLE IF EXISTS `webusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `webusers` (
  `WebUserID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(20) NOT NULL,
  `Password` char(128) NOT NULL,
  `IsAdmin` tinyint(1) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `ModifiedOn` datetime NOT NULL,
  `Salt` char(128) NOT NULL,
  `EmailAddress` varchar(100) NOT NULL,
  PRIMARY KEY (`WebUserID`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `webusers`
--

LOCK TABLES `webusers` WRITE;
/*!40000 ALTER TABLE `webusers` DISABLE KEYS */;
INSERT INTO `webusers` VALUES (1,'l46kok','�\Z��ɏ��9�����da8�@e���X%8���ܷ���׭ʹ����g����X�[�-���|�o',1,'2017-02-25 23:14:00','2017-02-25 23:14:00','�nj����ā2a�Wb\r��b�Lc���0\Z]:�:`���:*�4�R�$�\r$�6�6���e��','l46kok@hotmail.com'),(2,'Elysium','|��4�SY��\\�^�z��I��A��)=�[͊�!�/��v�ª	�P����2� K\r�$�',1,'2017-02-26 11:08:46','2017-02-26 11:08:46','^�o��yP�E���v���\Z2����c 1�`��Uz����y\n1��cP�1�3�����(�z��','Kuroyukihime1@yahoo.com'),(3,'paragontrix','ʢH`d��ɹ��8ߥ�R�z|���\'�{�[X\r>�Kp�B%m��N���9Fov��',1,'2017-02-26 11:09:23','2017-02-26 11:09:23','{[Dh�Ń{&ZC6\\�,=�$�̪=HR[��X3�B�;sp���ΰZ��h�Y#x�v�tP�+/]�^l','simon9876222@gmail.com'),(4,'Jiggy','e��z;�:�>�(h���z�PH��-?���Y�&0��\'��jyy�_D���h��Q�',1,'2017-02-26 11:24:46','2017-02-26 11:24:46','֪�?k[��Ð\'����BB\n\n!���ʙ�ٴ����P�RJ���f\"B��o�!�wY�I%oa=�Nje','albanajpaul@gmail.com'),(5,'Thestouges','�j���`]]z�6��\r�rb�Ф�X%�AEų�Z���5�-M�,G�\\]���鞁��Z�\r',1,'2017-02-26 12:11:11','2017-02-26 12:11:11','��W����ǯ� 2>n\\�n[�e�[���]*��\'�{��۶�)�)��i��2@-7�Ռ=uҦx','thestouges@gmail.com'),(7,'Launcelot','0�&<�D�kKb�A\"�:rZ����O\0� T�g�޷6�Y�[a��W,�����\'��8��f�Sq`',1,'2017-02-27 01:21:38','2017-02-27 01:21:38','�s;�2U˥��Y�w��5����jF\nK㇎��$C�*��S�(u�EaƻL�dم\"����h','customis@hotmail.com'),(8,'Syaoran_kun','����q�����Qx�M��+��;�C�q%�Z��?�G���H\'V�Q���zo�\r�s����y_u',1,'2017-02-27 01:22:04','2017-02-27 01:22:04','7�QI$�0��J���(���4�콮m���,���Vz0�	�O)�W��&!��-CC��ۣ','Syaoran_kun@hotmail.de'),(9,'luciouskami','?s�(�����=WUv�]�T��p3�m�RE0<��_�LФ��b�u)ۃnD�\r{�P�*���|�@',0,'2017-02-27 06:57:37','2017-02-27 06:57:37','�ݥ��N���Ȣ�\"��\\�F����,��N#,#����Pm�,��W�lB��L��bŇ��L_k� �0','realheiluo@gmail.com'),(10,'testaccount','!�I�~������\'���@0����᪞���;���,����9b��*-]�3��ܳ��\'��',0,'2017-02-27 14:30:50','2017-02-27 14:30:50','�J3/��R��u}!�3�K�\\��Y�[k�:��4,z�8��a����×����.�`�����x<q;','test@test.com'),(11,'Misa','�\n���~�m�Wz�p�y�z���j��L��j�����]�z�`5�J��-��Mj���O5-',1,'2017-03-22 19:48:42','2017-03-22 19:48:42','XVQ!$߯�@�ɳ�X�ixEsp�~ �]�*�V9�r�t�}�3J��2�4-<�+���=Tx���k�x','mxproxy@nate.com'),(12,'badao01','�s�B�ƨ�+�ЀH��Q�O�HQ�Ew�úS���oo��~�ص\"�]��̔A��%��V/',1,'2017-03-22 21:48:48','2017-03-22 21:48:48','4RV����ub~�PJ�K���	E�V��:g�Q�Ι_�������N��#�\'Q�C�M^?�h��','2330432450@qq.com');
/*!40000 ALTER TABLE `webusers` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-07 23:03:36
