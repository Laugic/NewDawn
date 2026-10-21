//using NewDawn.Abstracts;
//using Terraria;

//namespace NewDawn.Items.Alchemistry.OLD_Essences
//{
//    public class LightEssence : Essence
//    {

//        public override void GetEffectAtIndex(Player player, AbstractStorageItem item, int index)
//        {
//            switch (index)
//            {
//                case 0:
//                    player.findTreasure = true;
//                    break;
//                case 1:
//                    Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
//                    player.nightVision = true;
//                    break;
//                default:
//                    player.detectCreature = true;
//                    player.dangerSense = true;
//                    break;
//            }
//        }

//        public override string GetTipAtIndex(AbstractStorageItem item, int index)
//        {
//            switch (index)
//            {
//                case 0:
//                    return "Shows the location of treasure and ore";
//                case 1:
//                    return "Emits light and increases night vision";
//                default:
//                    return "Shows the location of creatures and danger sources";
//            }
//        }
//    }
//}
