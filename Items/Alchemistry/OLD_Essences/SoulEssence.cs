//using NewDawn.Abstracts;
//using NewDawn.Players;
//using Terraria;

//namespace NewDawn.Items.Alchemistry.OLD_Essences
//{
//    public class SoulEssence : Essence
//    {

//        public override void GetEffectAtIndex(Player player, AbstractStorageItem item, int index)
//        {
//            switch (index)
//            {
//                case 0:
//                    player.statLifeMax2 += 40;
//                    break;
//                case 1:
//                    player.lifeRegen += 2;
//                    player.manaRegen += 100;
//                    player.manaRegenBonus += 100;
//                    player.manaRegenDelayBonus += 30;
//                    break;
//                default:
//                    player.GetModPlayer<AlchemistryPlayer>().BonusHealFromHearts += 20;
//                    break;
//            }
//        }

//        public override string GetTipAtIndex(AbstractStorageItem item, int index)
//        {
//            switch (index)
//            {
//                case 0:
//                    return "+40 maximum life";
//                case 1:
//                    return "Increases life and mana regeneration";
//                default:
//                    return "Hearts heal an additional 20 life upon pickup";
//            }
//        }
//    }
//}
