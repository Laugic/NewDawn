using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Useables
{
    public class SteampunkWatch : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Steampunk Watch");
            // Tooltip.SetDefault("You tell the time.");
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.value = 100;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }
        
        
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Main.dayTime = !Main.dayTime;
            Main.time = 0.0;
            return true;
        }


        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(Mod, nameof(SteamBar), 8);
                recipe.AddIngredient(Mod, nameof(Gear), 20);
                recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
                recipe.AddIngredient(ModContent.ItemType<DioritusCore>(), 1);
                recipe.AddIngredient(ModContent.ItemType<CogOfKnowledge>(), 1);
                recipe.AddIngredient(ModContent.ItemType<SteamTank>(), 1);
                recipe.AddIngredient(ModContent.ItemType<Pipeworks>(), 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
        }
    }
}