using NewDawn.Dusts;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Accessory;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class BysmaliteWarpers : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
            NewDawnVars.RocketBoots.Add(Item.type);
            // Tooltip.SetDefault("Dimension-tearing speed and dashing\nDominion over everything\nLeave a Frigid trail\nIncreased Damage, Movement Speed, and Defense in the Etherial");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
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
            WarpDist = 20;

            RocketBootTimeMax = (5 * 60) + 30;
            RocketSpeedMax = 13;
            RocketAccel = .25f;
            RocketAccelMax = .6f;

            RunBoots = true;
            DustType = ModContent.DustType<EtherialDust>();
            ProjectileType = ModContent.ProjectileType<BysmalTrailProj>();
            TrailLength = 60;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 5;
            player.moveSpeed += .6f;
            player.maxRunSpeed += 6.5f;
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
                player.GetModPlayer<NewDawnPlayer>().BysmalTrail = true;
            if (NewDawnWorld.downedEtheria || player.GetModPlayer<NewDawnPlayer>().Etherable > 0)
            {
                player.GetModPlayer<NewDawnPlayer>().DamageBoost(.1f);
                player.statDefense += 10;
                player.moveSpeed += 8f;
            }
        }
        public override SoundStyle RocketSoundID => SoundID.Item24;
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SteamsparkJetboots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 16);
            recipe.AddIngredient(ModContent.ItemType<EtherialEssence>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}