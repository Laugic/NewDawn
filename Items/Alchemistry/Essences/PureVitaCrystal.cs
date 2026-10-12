using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Items.Materials;
using NewDawn.Players;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Essences
{
    public class PureVitaCrystal : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AlchemistryPlayer modPlayer = player.GetModPlayer<AlchemistryPlayer>();
            modPlayer.Vita = true;
            player.statLifeMax2 = (int)(player.statLifeMax2 * 1.2f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrigidCrystal>());
            recipe.AddIngredient(ModContent.ItemType<VitaEssence>(), 10);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ModContent.ItemType<LifeforceCrystal>());
            recipe.AddIngredient(ModContent.ItemType<RegenerationCrystal>());
            recipe.AddTile(ModContent.TileType<Crystallizer>());
            recipe.Register();
        }
    }
}