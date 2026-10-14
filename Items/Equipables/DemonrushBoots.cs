using NewDawn.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class DemonrushBoots : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
}

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 45;
            VDashCooldownMax = 45;
            DashSpeed = 12;
            MaxVDashes = 1;
            JumpSpeed = 12;
            JumpDur = 20;

            DustType = ModContent.DustType<Black>();
            TrailLength = 45;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 3;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkfootBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Eruption>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BloodfootBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Eruption>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}