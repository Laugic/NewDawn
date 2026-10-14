//using Terraria;
//using Terraria.ID;

//namespace NewDawn.Items.Equipables
//{
//    public class SweetstarNecklace : NewDawnItem
//    {
//        public override void SetStaticDefaults()
//        {
//        }

//        public override void SetDefaults()
//        {
//            Item.width = 26;
//            Item.height = 36;
//            Item.value = 100;
//            Item.rare = ItemRarityID.Pink;
//            Item.accessory = true;
//        }

//        public override void UpdateAccessory(Player player, bool hideVisual)
//        {
//            player.longInvince = true;
//            player.starCloakItem = true;
//            player.starCloakItem_beeCloakOverrideItem = true;
//            player.panic = true;
//        }

//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(862);
//            recipe.AddIngredient(1578);
//            recipe.AddTile(114);
//            recipe.Register();
//        }
//    }
//}