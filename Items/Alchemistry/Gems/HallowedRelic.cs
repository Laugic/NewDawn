using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class HallowedRelic : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("You feel Blessed");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.lifeRegen = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.SoulStoneVisuals)
            {
                if (modPlayer.spelunker)
                    player.findTreasure = true;
                Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
                if (modPlayer.owl)
                    player.nightVision = true;
                if (modPlayer.hunter)
                    player.detectCreature = true;
                if (modPlayer.danger)
                    player.dangerSense = true;
            }
            player.maxMinions++;
            if (modPlayer.calm)
                player.calmed = true;
            player.resistCold = true;
            player.lifeMagnet = true;
            player.statLifeMax2 += ((player.statLifeMax + player.statLifeMax2) / 5 / 20 * 20) - (player.statLifeMax / 5 / 20 * 20);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HolyStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SightStone>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}