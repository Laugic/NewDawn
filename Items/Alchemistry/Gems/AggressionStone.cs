using NewDawn.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class AggressionStone : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 1000;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegenBonus += 25;
            player.GetDamage(DamageClass.Magic) += 0.20f;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.kbBuff = true;
            player.ammoCost80 = true;
            player.GetDamage(DamageClass.Ranged) += 0.10f;
            if (!modPlayer.battle)
                player.enemySpawns = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ReichCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<KekCrystal>(), 1);
            recipe.AddTile(ModContent.TileType<MineralEnchanterTile>());
            recipe.Register();
        }
    }
}