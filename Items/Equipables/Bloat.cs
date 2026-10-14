using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class Bloat : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 *= 2;
            NewDawnPlayer.Get(player).Bloat = true;
            if (player.statLife > (int)(player.statLifeMax2 * .5))
            {
                player.statLife = (int)(player.statLifeMax2 * .5);
                player.lifeRegen = 0;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TissueSample, 12);
            recipe.AddIngredient(ItemID.ShadowScale, 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}