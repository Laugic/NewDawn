//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace NewDawn.Items.Alchemistry.OLD_Essences
//{
//    public class AirInfusion : ModItem
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
//            player.accRunSpeed += 3.5f;
//            player.maxRunSpeed += 3.5f;
//            player.moveSpeed += .2f;


//            player.dashType = 1;

//            player.slowFall = true;
//            player.jumpSpeedBoost = 3f;
//        }

//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ModContent.ItemType<AirEssence>(), 3);
//            recipe.AddCondition(InfusionCrystal.Infusion);
//            recipe.Register();
//        }
//    }
//}
