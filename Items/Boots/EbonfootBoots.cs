using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Boots
{
    public class EbonfootBoots : BootPar
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebonfoot Boots");
            Tooltip.SetDefault("Allows the wearer to dash");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.width = 28;
            item.height = 28;
            item.value = Item.buyPrice(silver: 50);
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateBootStats()
        {
            CanDash = true;
            DashSpeed = 10;
            DashCooldownMax = 60;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            UpdateBootStats();
            base.UpdateAccessory(player, hideVisual);
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddRecipeGroup(NewDawn.FAST_FOOTWEAR_GROUP);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}