//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace NewDawn.Items.Alchemistry.OLD_Essences
//{
//    public class LightInfusion : ModItem
//    {

//        public override void SetDefaults()
//        {
//            Item.width = 32;
//            Item.height = 32;
//            Item.value = Item.sellPrice(silver: 50);
//            Item.rare = ItemRarityID.Orange;
//            Item.maxStack = 1;
//            Item.accessory = true;
//            Item.hasVanityEffects = true;
//        }

//        public override void UpdateEquip(Player player)
//        {
//            player.findTreasure = true;
//            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
//            player.nightVision = true;
//            player.detectCreature = true;
//            player.dangerSense = true;
//        }

//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ModContent.ItemType<LightEssence>(), 3);
//            recipe.AddCondition(InfusionCrystal.Infusion);
//            recipe.Register();
//        }
//    }
//}
