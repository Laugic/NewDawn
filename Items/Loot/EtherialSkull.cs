using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class EtherialSkull : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("After taking damage, your damage is boosted by the percentage of your health that was taken for 10 seconds.\nIf this buff is still active when damage is taken again, the boost is stacked.");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawnWorld.downedEtheria || modPlayer.Etherable > 0)
            {
                modPlayer.EtherialBones = true;
            }
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(2328, 4);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}