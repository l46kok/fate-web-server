using System.Collections.Generic;
using System.IO;

namespace Fate.WebServiceLayer
{
    public static class ContentURL
    {
        private static readonly Dictionary<string, string> _heroIconDic = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _ghIconDic = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _attributeDic = new Dictionary<string, string>();

        static ContentURL()
        {
            _heroIconDic.Add("H028", @"/Content/icons/heroes/BTNAvenger.jpg");
            _heroIconDic.Add("H000",@"/Content/icons/heroes/BTNSaber.jpg");
            _heroIconDic.Add("H002",@"/Content/icons/heroes/BTNChulainn.jpg");
            _heroIconDic.Add("H004", @"/Content/icons/heroes/BTNCaster.jpg");
            _heroIconDic.Add("H001", @"/Content/icons/heroes/BTNArcherIcon.jpg");
            _heroIconDic.Add("H005", @"/Content/icons/heroes/BTNAssassinIcon.jpg");
            _heroIconDic.Add("H006", @"/Content/icons/heroes/BTNBerserker.jpg");
            _heroIconDic.Add("H007", @"/Content/icons/heroes/BTNDarkSaber.jpg");
            _heroIconDic.Add("H00Y", @"/Content/icons/heroes/BTNDrake.jpg");
            _heroIconDic.Add("H00I", @"/Content/icons/heroes/BTNGawain.jpg");
            _heroIconDic.Add("H009", @"/Content/icons/heroes/BTNGilgamesh.jpg");
            _heroIconDic.Add("E002", @"/Content/icons/heroes/BTNGilles.jpg");
            _heroIconDic.Add("H00A", @"/Content/icons/heroes/BTNIskander.jpg");
            _heroIconDic.Add("H021", @"/Content/icons/heroes/BTNKarna.jpg");
            _heroIconDic.Add("H03M", @"/Content/icons/heroes/BTNLancelot.jpg");
            _heroIconDic.Add("H01X", @"/Content/icons/heroes/BTNNero.jpg");
            _heroIconDic.Add("H024", @"/Content/icons/heroes/BTNNursery.jpg");
            _heroIconDic.Add("H003", @"/Content/icons/heroes/BTNRider.jpg");
            _heroIconDic.Add("H01T", @"/Content/icons/heroes/BTNRobin.jpg");
            _heroIconDic.Add("H00E", @"/Content/icons/heroes/BTNTamamo.jpg");
            _heroIconDic.Add("H008", @"/Content/icons/heroes/BTNTrueAssassin.jpg");
            _heroIconDic.Add("H01F", @"/Content/icons/heroes/BTNVlad.jpg");
            _heroIconDic.Add("H01Q", @"/Content/icons/heroes/BTNYeopo.jpg");
            _heroIconDic.Add("H04D", @"/Content/icons/heroes/BTNZeroLancer.jpg");
            _heroIconDic.Add("H01A", @"/Content/icons/heroes/BTNLi.jpg");
            _heroIconDic.Add("H02W", @"/Content/icons/heroes/BTNElizabeth.png");
            _ghIconDic.Add("A04B", @"/Content/icons/GHGold.jpg");
            _ghIconDic.Add("A04F", @"/Content/icons/GHLevelUp.jpg");
            _ghIconDic.Add("A0DD", @"/Content/icons/GHStats.jpg");
            _ghIconDic.Add("A04C", @"/Content/icons/GHAMP.jpg");
            _ghIconDic.Add("A04D", @"/Content/icons/GHFullHealPot.jpg");
            _ghIconDic.Add("A00U", @"/Content/icons/GHInvulBird.jpg");
            _attributeDic.Add("A0GO", @"/Content/icons/attributes/SphereBoundary.png");
            _attributeDic.Add("A0GP", @"/Content/icons/attributes/FierceTigerForciblyClimbsaMountain.png");
            _attributeDic.Add("A0GF", @"/Content/icons/attributes/CirculatoryShock.png");
            _attributeDic.Add("A0GG", @"/Content/icons/attributes/DoubleClass.png");
            _attributeDic.Add("A077", @"/Content/icons/attributes/ImprovedTerritory.png");
            _attributeDic.Add("A082", @"/Content/icons/attributes/DivineLanguage.png");
            _attributeDic.Add("A07R", @"/Content/icons/attributes/ImprovedHecaticGraea.png");
            _attributeDic.Add("A07H", @"/Content/icons/attributes/ImprovedAegis.png");
            _attributeDic.Add("A07B", @"/Content/icons/attributes/ImprovedGaeBolg.png");
            _attributeDic.Add("A07L", @"/Content/icons/attributes/FlyingSpearofBarbedDeath.png");
            _attributeDic.Add("A07V", @"/Content/icons/attributes/SpearofDeath.png");
            _attributeDic.Add("A0GL", @"/Content/icons/attributes/ImprovedRunes.png");
            _attributeDic.Add("A071", @"/Content/icons/attributes/ImprovedBattleContinuation.png");
            _attributeDic.Add("A08K", @"/Content/icons/attributes/ProtectionofFairies.png");
            _attributeDic.Add("A08M", @"/Content/icons/attributes/HonoroftheShiningLake.png");
            _attributeDic.Add("A08J", @"/Content/icons/attributes/EternalArmsMastership.png");
            _attributeDic.Add("A08L", @"/Content/icons/attributes/ImprovedArondight.png");
            _attributeDic.Add("A081", @"/Content/icons/attributes/PhantomAttack.png");
            _attributeDic.Add("A0H0", @"/Content/icons/attributes/DelusionalIllusion.png");
            _attributeDic.Add("A07Q", @"/Content/icons/attributes/Zabaniya.png");
            _attributeDic.Add("A07G", @"/Content/icons/attributes/ProtectionfromWind.png");
            _attributeDic.Add("A07A", @"/Content/icons/attributes/Riding.png");
            _attributeDic.Add("A07K", @"/Content/icons/attributes/Seal.png");
            _attributeDic.Add("A07U", @"/Content/icons/attributes/MonstrousStrength.png");
            _attributeDic.Add("A070", @"/Content/icons/attributes/ImprovedMysticEyes.png");
            _attributeDic.Add("A074", @"/Content/icons/attributes/ImprovedClairvoyance.png");
            _attributeDic.Add("A07E", @"/Content/icons/attributes/Hrunting.png");
            _attributeDic.Add("A07Y", @"/Content/icons/attributes/ImprovedTracing.png");
            _attributeDic.Add("A07I", @"/Content/icons/attributes/ManaBlast.png");
            _attributeDic.Add("A078", @"/Content/icons/attributes/ImprovedManaShroud.png");
            _attributeDic.Add("A083", @"/Content/icons/attributes/BlackLight-DarkExcalibur.png");
            _attributeDic.Add("A0BV", @"/Content/icons/attributes/ImprovedFerocity.png");
            _attributeDic.Add("A0AC", @"/Content/icons/attributes/BloomingofYellow-RedRose.png");
            _attributeDic.Add("A0AE", @"/Content/icons/attributes/DoubleSpear.png");
            _attributeDic.Add("A08P", @"/Content/icons/attributes/LoveSpotOfSeduction.png");
            _attributeDic.Add("A073", @"/Content/icons/attributes/ImprovedExcalibur.png");
            _attributeDic.Add("A0FZ", @"/Content/icons/attributes/StrikeAir.png");
            _attributeDic.Add("A07D", @"/Content/icons/attributes/ImprovedInstincts.png");
            _attributeDic.Add("A0K9", @"/Content/icons/attributes/DragonSkillet.png");
            _attributeDic.Add("A0KC", @"/Content/icons/attributes/RainbowPlainsandtheMazeofMirrors.png");
            _attributeDic.Add("A0KD", @"/Content/icons/attributes/TheKingofBlackandWhiteCheckerboard.png");
            _attributeDic.Add("A0D3", @"/Content/icons/attributes/ImprovedBlackMagic.png");
            _attributeDic.Add("A0CY", @"/Content/icons/attributes/ImprovedDemonicCreatureoftheOceanDepths.png");
            _attributeDic.Add("A0CV", @"/Content/icons/attributes/Contagion.png");
            _attributeDic.Add("A0J2", @"/Content/icons/attributes/KeytotheTheater.png");
            _attributeDic.Add("A0IW", @"/Content/icons/attributes/EmbryonicFlame.png");
            _attributeDic.Add("A0IX", @"/Content/icons/attributes/ImperialPrivilege.png");
            _attributeDic.Add("A0J0", @"/Content/icons/attributes/ThriceThoughIWelcometheSettingSun.png");
            _attributeDic.Add("A0JS", @"/Content/icons/attributes/ImprovedVasaviShakti.png");
            _attributeDic.Add("A0JQ", @"/Content/icons/attributes/ImprovedDivinity(Karna).png");
            _attributeDic.Add("A0JR", @"/Content/icons/attributes/ImprovedBrahmastra.png");
            _attributeDic.Add("A0I9", @"/Content/icons/attributes/SacramentumofDruid.png");
            _attributeDic.Add("A0I8", @"/Content/icons/attributes/SherwoodForest.png");
            _attributeDic.Add("A0I6", @"/Content/icons/attributes/OneDropofaViper.png");
            _attributeDic.Add("A0I7", @"/Content/icons/attributes/PoisonedArrow.png");
            _attributeDic.Add("A07N", @"/Content/icons/attributes/Knighthood.png");
            _attributeDic.Add("A07X", @"/Content/icons/attributes/ImprovedCharisma.png");
            _attributeDic.Add("A06F", @"/Content/icons/attributes/ImprovedIonionHetairoi.png");
            _attributeDic.Add("A053", @"/Content/icons/attributes/ImprovedCharisma(ZR).png");
            _attributeDic.Add("A046", @"/Content/icons/attributes/ImprovedMilitaryTactics.png");
            _attributeDic.Add("A0BP", @"/Content/icons/attributes/EyeforArt.png");
            _attributeDic.Add("A07Z", @"/Content/icons/attributes/QuickDraw.png");
            _attributeDic.Add("A07P", @"/Content/icons/attributes/Vitrification.png");
            _attributeDic.Add("A09O", @"/Content/icons/attributes/MonohoshiZao.png");
            _attributeDic.Add("A075", @"/Content/icons/attributes/EyeoftheMind.png");
            _attributeDic.Add("A06C", @"/Content/icons/attributes/Nightmare.png");
            _attributeDic.Add("A069", @"/Content/icons/attributes/CurseofBlood.png");
            _attributeDic.Add("A060", @"/Content/icons/attributes/CurseofBlood2.png");
            _attributeDic.Add("A0BQ", @"/Content/icons/attributes/AvengerofBlood.png");
            _attributeDic.Add("A066", @"/Content/icons/attributes/TawrichandZarich.png");
            _attributeDic.Add("A0BM", @"/Content/icons/attributes/SpiritOrb.png");
            _attributeDic.Add("A0C8", @"/Content/icons/attributes/Witchcraft.png");
            _attributeDic.Add("A0BZ", @"/Content/icons/attributes/CursedCharms.png");
            _attributeDic.Add("A0C1", @"/Content/icons/attributes/ImprovedTerritory(Tamamo).png");
            _attributeDic.Add("A0KA", @"/Content/icons/attributes/AckroydinCelluloid.png");
            _attributeDic.Add("A0JT", @"/Content/icons/attributes/PranaBurst(Karna).png");
            _attributeDic.Add("A00N", @"/Content/icons/attributes/KanshoBakuyaOveredge.png");
            _attributeDic.Add("A0I5", @"/Content/icons/attributes/TheFacelessKing.png");
            _attributeDic.Add("A05N", @"/Content/icons/attributes/WheelofGordias.png");
            _attributeDic.Add("A05F", @"/Content/icons/attributes/ImprovedSpatha.png");
            _attributeDic.Add("A000", @"/Content/icons/attributes/ImprovedDivinity.png");
            _attributeDic.Add("A07J", @"/Content/icons/attributes/ImprovedSwordRain.png");
            _attributeDic.Add("A07T", @"/Content/icons/attributes/TheStarofCreationthatSplitHeavenandEarth.png");
            _attributeDic.Add("A0ED", @"/Content/icons/attributes/SwordofVictory.png");
            _attributeDic.Add("A0EC", @"/Content/icons/attributes/NumeraloftheSaint.png");
            _attributeDic.Add("A072", @"/Content/icons/attributes/SacredMirror.png");
            _attributeDic.Add("A07F", @"/Content/icons/attributes/Ganryu-ShoreWillow.png");
            _attributeDic.Add("A06S", @"/Content/icons/attributes/ImprovedGoldenRule.png");
            _attributeDic.Add("A079", @"/Content/icons/attributes/PowerofSumer.png");
            _attributeDic.Add("A07W", @"/Content/icons/attributes/Reincarnation.png");
            _attributeDic.Add("A07M", @"/Content/icons/attributes/GodHand.png");
            _attributeDic.Add("A0J1", @"/Content/icons/attributes/ImprovedFountainoftheSaint.png");
            _attributeDic.Add("A0HF", @"/Content/icons/attributes/Houtengageki.png");
            _attributeDic.Add("A0HL", @"/Content/icons/attributes/Bravery.png");
            _attributeDic.Add("A0HK", @"/Content/icons/attributes/GateHalberdShot.png");
            _attributeDic.Add("A0FE", @"/Content/icons/attributes/CombustionShot.png");
            _attributeDic.Add("A0FG", @"/Content/icons/attributes/ImprovedGoldenHind.png");
            _attributeDic.Add("A0FH", @"/Content/icons/attributes/GoldenRule_Pillage.png");
            _attributeDic.Add("A0EB", @"/Content/icons/attributes/Knighthood(Gawain).png");
            _attributeDic.Add("A0EA", @"/Content/icons/attributes/ProtectionoftheFairies(Gawain).png");
            _attributeDic.Add("A07C", @"/Content/icons/attributes/MadEnhancement.png");
            _attributeDic.Add("A0E9", @"/Content/icons/attributes/Gwalhmai.png");
            _attributeDic.Add("A07O", @"/Content/icons/attributes/ShroudofMartin.png");
            _attributeDic.Add("A0FD", @"/Content/icons/attributes/Logbook.png");
            _attributeDic.Add("A0AT", @"/Content/icons/attributes/LordofExecution.png");
            _attributeDic.Add("A0B2", @"/Content/icons/attributes/RebelliousIntent.png");
            _attributeDic.Add("A0AK", @"/Content/icons/attributes/InnocentMonster.png");
            _attributeDic.Add("A0AS", @"/Content/icons/attributes/ProtectionoftheFaith.png");
            _attributeDic.Add("A0HE", @"/Content/icons/attributes/ImmortalRedHare.png");
            _attributeDic.Add("A0HD", @"/Content/icons/attributes/RedHare.png");
            _attributeDic.Add("A0KB", @"/Content/icons/attributes/VorpalBlade.png");
            _attributeDic.Add("A0JP", @"/Content/icons/attributes/UncrownedArmsMastership.png");
            _attributeDic.Add("A0AD", @"/Content/icons/attributes/Mind'sEye(True).png");
            _attributeDic.Add("A076", @"/Content/icons/attributes/ImprovedPresenceConcealment.png");
            _attributeDic.Add("A0LD", @"/Content/icons/attributes/BTNAspectOfDragon.png");
            _attributeDic.Add("A0LF", @"/Content/icons/attributes/BTNDoubleClassEliz.png");
            _attributeDic.Add("A0LE", @"/Content/icons/attributes/BTNInnocentMonsterEliz.png");
            _attributeDic.Add("A0LC", @"/Content/icons/attributes/BTNSadisticCharisma.png");
            _attributeDic.Add("A0LK", @"/Content/icons/attributes/BTNTortureTechniques.png");
        }

        public static string GetAttributeIconURL(string attributeAbilId)
        {
            string url;
            _attributeDic.TryGetValue(attributeAbilId, out url);
            return url;
        }

        public static string GetHeroIconURL(string heroUnitTypeId)
        {
            string url;
            _heroIconDic.TryGetValue(heroUnitTypeId, out url);
            return url;
        }

        public static string GetGodsHelpIconURL(string godsHelpAbilId)
        {
            string url;
            _ghIconDic.TryGetValue(godsHelpAbilId, out url);
            return url;
        }

        public static string GetLocalReplayPath(string replayPath)
        {
            return "Content\\replays\\" + Path.GetFileName(replayPath);
        }
    }
}
