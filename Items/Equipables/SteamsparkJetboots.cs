using NewDawn.Dusts;
using NewDawn.Items.Loot;
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
    public class SteamsparkJetboots : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
            NewDawnVars.RocketBoots.Add(Item.type);
}

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 18);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 40;
            VDashCooldownMax = 40;
            DashSpeed = 18;
            MaxVDashes = 6;
            JumpSpeed = 14;
            JumpDur = 24;
            ImmuneTime = 18;

            RocketBootTimeMax = 4 * 60 + 30;
            RocketSpeedMax = 12;
            RocketAccel = .075f;
            RocketAccelMax = .6f;
            RocketSoundID = SoundID.Item13;

            RunBoots = true;
            DustType = ModContent.DustType<SteamTrailDust>();
            ProjectileType = ModContent.ProjectileType<SteamTrailProj>();
            TrailLength = 60;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 4;
            player.moveSpeed += .5f;
            player.maxRunSpeed += 5.5f;
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
                player.GetModPlayer<NewDawnPlayer>().SteamTrail = true;
        }
        /*
        public override void Run(Player player)
        {
            if (Main.tileSolid[Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].type] && Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].type != 0 && Math.Abs(player.velocity.X) > 4)
            {
                int newDust = Dust.NewDust(new Vector2(player.Center.X + player.velocity.X, player.position.Y + 4 + (float)player.height - 10f + player.velocity.Y), 8, 8, ModContent.DustType<Steam>(), 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[newDust].scale = 4;
            }
        }*/

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HyperwarpLightboots>(), 1);
            recipe.AddIngredient(ItemID.Jetpack, 1);
            recipe.AddIngredient(ModContent.ItemType<SteamTank>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SteamBar>(), 16);
            recipe.AddIngredient(ModContent.ItemType<Gear>(), 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}