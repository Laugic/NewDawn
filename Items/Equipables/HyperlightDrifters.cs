using NewDawn.Dusts;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Accessory;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class HyperlightDrifters : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
            NewDawnVars.RocketBoots.Add(Item.type);
            // Tooltip.SetDefault("Light speed and light dash\nDominion over everything\nLeave a Cosmic trail\n+10% Damage");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 24);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.defense = 10;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 40;
            VDashCooldownMax = 40;
            DashSpeed = 20;
            MaxVDashes = -1;
            JumpSpeed = 14;
            JumpDur = 24;
            ImmuneTime = 20;
            WarpDist = 25;

            RocketBootTimeMax = 6 * 60;
            RocketSpeedMax = 14;
            RocketAccel = .25f;
            RocketAccelMax = .7f;
            RocketSoundID = SoundID.Item13;

            RunBoots = true;
            DustType = ModContent.DustType<Rainbow>();
            ProjectileType = ModContent.ProjectileType<CosmicTrailProj>();
            TrailLength = 60;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 7;
            player.moveSpeed += .75f;
            player.maxRunSpeed += 7.5f;
            player.iceSkate = true;
            player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();
            player.noFallDmg = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[24] = true;
            player.gills = true;
            player.ignoreWater = true;
            player.accFlipper = true;
            if (!hideVisual)
                player.GetModPlayer<NewDawnPlayer>().CosmicTrail = true;
            player.GetModPlayer<NewDawnPlayer>().DamageBoost(.1f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmaliteWarpers>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddIngredient(ModContent.ItemType<GalacticFragment>(), 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}