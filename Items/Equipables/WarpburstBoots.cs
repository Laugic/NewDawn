using NewDawn.Dusts;
using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class WarpburstBoots : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
            // DisplayName.SetDefault("Warpburst Boots");
            // Tooltip.SetDefault("Allows the wearer to dash\nIncreased jump height and movement speed\nCan dash multiple times in the air\nBecome immune for a time while dashing");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 8);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 45;
            VDashCooldownMax = 45;
            DashSpeed = 14;
            MaxVDashes = 3;
            JumpSpeed = 12;
            JumpDur = 20;
            ImmuneTime = 15;

            DustType = ModContent.DustType<White>();
            TrailLength = 45;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.moveSpeed += .25f;
            player.jumpSpeedBoost += 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DemonsparkBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DioritusCore>());
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}