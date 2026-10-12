using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class EtherialTank : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("CHOO CHOO! While in the etherial, the faster you move, the higher your damage.\nColliding with an enemy deals your movement speed * 500 in damage.\nGreatly increases Movement Speed.");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawnWorld.downedEtheria || modPlayer.Etherable > 0)
            {
                modPlayer.EtherialTank = true;
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