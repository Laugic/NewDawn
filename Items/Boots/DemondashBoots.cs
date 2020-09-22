using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Boots
{
    public class DemondashBoots : BootPar
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demondash Boots");
            Tooltip.SetDefault("Allows the wearer to dash in any direction\nAllows the wearer to double jump");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.width = 28;
            item.height = 28;
            item.value = Item.buyPrice(silver: 75);
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateBootStats()
        {
            CanDash = true;
            MaxVertDash = 1;
            DashSpeed = 14;
            DashCooldownMax = 60;
            TrailDust = DustID.Fire;
            TrailMax = 60 / 2;
            DashBurstNum = 20;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            UpdateBootStats();
            base.UpdateAccessory(player, hideVisual);

            player.doubleJumpCloud = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BloodfootBoots>());
            recipe.AddRecipeGroup(NewDawn.DOUBLE_JUMP_GROUP);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<EbonfootBoots>());
            recipe.AddRecipeGroup(NewDawn.DOUBLE_JUMP_GROUP);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}