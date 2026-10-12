using NewDawn.Items.Materials;
using NewDawn.Projectiles.Accessory;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class ShadowarpFlameboots : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
            NewDawnVars.RocketBoots.Add(Item.type);
            // DisplayName.SetDefault("Shadowarp Flameboots");
            // Tooltip.SetDefault("Shadow speed, shadow flight, and shadow dash\nDominion over liquids and fire\nLeave a trail of Shadowflame");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 14);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 40;
            VDashCooldownMax = 40;
            DashSpeed = 16;
            MaxVDashes = 4;
            JumpSpeed = 12;
            JumpDur = 20;
            ImmuneTime = 15;

            RocketBootTimeMax = 3 * 60;
            RocketSpeedMax = 10;
            RocketAccel = .065f;
            RocketAccelMax = .5f;
            RocketSoundID = SoundID.Item13;

            RunBoots = true;
            DustType = DustID.Shadowflame;
            ProjectileType = ModContent.ProjectileType<GoodShadowflame>();
            TrailLength = 45;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 3;
            player.moveSpeed += .3f;
            player.maxRunSpeed += 4f;
            player.iceSkate = true;
            player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();;
            player.noFallDmg = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[24] = true;
            player.gills = true;
            player.ignoreWater = true;
            player.accFlipper = true;
            if(!hideVisual)
                player.GetModPlayer<NewDawnPlayer>().ShadowflameTrail = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostwarpBoots>());
            recipe.AddIngredient(ModContent.ItemType<AquaflameWaders>());
            recipe.AddIngredient(ModContent.ItemType<Shadowflame>(), 4);
            recipe.AddIngredient(ItemID.SoulofFlight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}