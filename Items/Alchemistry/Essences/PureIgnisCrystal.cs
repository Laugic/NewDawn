using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Players;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Essences
{
    public class PureIgnisCrystal : ModItem
    {
        public static float RANGE = 300f;
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AlchemistryPlayer modPlayer = player.GetModPlayer<AlchemistryPlayer>();
            modPlayer.Ignis = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidianSkinCrystal>());
            recipe.AddIngredient(ModContent.ItemType<InfernoCrystal>());
            recipe.AddIngredient(ModContent.ItemType<IgnisEssence>(), 10);
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.MagmaStone);
            recipe.AddTile(ModContent.TileType<Crystallizer>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidianSkinCrystal>());
            recipe.AddIngredient(ModContent.ItemType<InfernoCrystal>());
            recipe.AddIngredient(ModContent.ItemType<IgnisEssence>(), 10);
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.LavaCharm);
            recipe.AddTile(ModContent.TileType<Crystallizer>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidianSkinCrystal>());
            recipe.AddIngredient(ModContent.ItemType<InfernoCrystal>());
            recipe.AddIngredient(ModContent.ItemType<IgnisEssence>(), 10);
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.ObsidianRose);
            recipe.AddTile(ModContent.TileType<Crystallizer>());
            recipe.Register();
        }
    }
}