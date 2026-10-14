using NewDawn.Items.Loot;
using NewDawn.Projectiles.Accessory;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class HyperwarpLightboots : BootItem
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
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 40;
            VDashCooldownMax = 40;
            DashSpeed = 16;
            MaxVDashes = 5;
            JumpSpeed = 12;
            JumpDur = 24;
            ImmuneTime = 18;

            RocketBootTimeMax = 4 * 60;
            RocketSpeedMax = 10;
            RocketAccel = .065f;
            RocketAccelMax = .55f;
            RocketSoundID = SoundID.Item13;

            RunBoots = true;
            DustType = 58;
            ProjectileType = ModContent.ProjectileType<CrystalliteOrb>();
            TrailLength = 45;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 3.5f;
            player.moveSpeed += .35f;
            player.maxRunSpeed += 5f;
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
                player.GetModPlayer<NewDawnPlayer>().CrystalliteTrail = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadowarpFlameboots>());
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSought>(), 8);
            recipe.AddIngredient(ItemID.CrystalShard, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}