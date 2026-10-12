using NewDawn.Dusts;
using NewDawn.Items.Consumables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class DemonsparkBoots : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
            // DisplayName.SetDefault("Hellspark Boots");
            // Tooltip.SetDefault("Allows the wearer to dash\nIncreased jump height and movement speed\nCan dash multiple times in the air");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 45;
            VDashCooldownMax = 45;
            DashSpeed = 12;
            MaxVDashes = 3;
            JumpSpeed = 12;
            JumpDur = 20;

            DustType = ModContent.DustType<Magma>();
            TrailLength = 45;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.moveSpeed += .2f;
            player.jumpSpeedBoost += 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DemonrushBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumHeart>(), 1);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}