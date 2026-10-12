
using NewDawn.Items;
using NewDawn.Items.Equipables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.SoulStones
{
    public class CapacityFocusStone : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Infuses your Soul with Capacity");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item9;
            Item.consumable = false;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.GetModPlayer<NewDawnPlayer>().Focus = FocusManager.Instance.Capacity;
            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LargeSapphire);
            recipe.AddIngredient(ModContent.ItemType<Arcanum>());
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}