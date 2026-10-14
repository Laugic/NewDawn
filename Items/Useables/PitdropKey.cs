//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ID;

//namespace NewDawn.Items.Useables
//{
//    public class PitdropKey : NewDawnItem
//    {
//        public override void SetStaticDefaults()
//        {
//        }

//        public override void SetDefaults()
//        {
//            Item.width = 32;
//            Item.height = 32;
//            Item.value = 100;
//            Item.rare = ItemRarityID.Pink;
//            Item.useAnimation = 30;
//            Item.useTime = 30;
//            Item.useStyle = ItemUseStyleID.HoldUp;
//        }

//        //public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
//        //{
//        //    Subworld.Enter<PitdropWorld>();
//        //    return true;
//        //}

//        public override Vector2? HoldoutOffset()
//        {
//            return new Vector2(20, 0);
//        }
//        /*
//        public override void AddRecipes()
//        {
//            ModRecipe recipe = new ModRecipe(mod);
//            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 4);
//            recipe.AddIngredient(mod, nameof(EtherialEssence), 2);
//            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }*/
//    }
//}