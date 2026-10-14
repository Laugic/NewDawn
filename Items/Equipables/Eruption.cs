using Microsoft.Xna.Framework;
using NewDawn.Dusts;
using NewDawn.Projectiles.Mystic.Burst;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class Eruption : NewDawnItem
    {
        bool usedJump = false;
        int justJumped = 0;
        int counter = 0;
        int trail = 0;

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 36;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            justJumped = 0;
            trail = 0;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 3;
            JumpCheck(player, hideVisual);
            Trail(player);
            Reset(player);
        }

        private void Trail(Player player)
        {
            if (trail > 0)
            {
                trail++;
                Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, ModContent.DustType<Magma>());
                player.velocity.Y = -12;
            }
            if (trail > 25 || player.releaseJump)
                trail = 0;
        }

        private void JumpCheck(Player player, bool hideVisual)
        {
            if (justJumped == 1 && player.releaseJump)
                justJumped = 2;
            if (!player.mount.Active && player.grappling[0] == -1)
            {
                if (player.controlJump && !usedJump && player.velocity.Y != 0)
                {
                    if (justJumped == 0)
                    {
                        justJumped = 1;
                        return;
                    }
                    Jump(player, hideVisual);
                }
            }
        }

        private void Jump(Player player, bool hideVisual)
        {
            player.velocity.Y = -12;
            usedJump = true;
            trail++;
            player.fallStart = (int)player.position.Y / 16;
            for (int i = 0; i < 40; i++)
                Dust.NewDust(new Vector2(player.position.X, player.Center.Y + 24), player.width, 4, ModContent.DustType<Magma>());
            if (hideVisual)
                return;
            for (int i = 0; i < 4; i++)
            {
                double theta = Math.PI * Main.rand.NextDouble();
                var mag = 6 + (Main.rand.NextFloat() * 4);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(player.position.X, player.Center.Y + 24), new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<EruptionProjectile>(), (int)(10 * player.GetDamage(DamageClass.Generic).Multiplicative), 3, player.whoAmI);
            }
        }

        private void Reset(Player player)
        {
            if (player.velocity.Y != 0)
                counter = 0;
            counter++;
            if (counter > 2 || player.grappling[0] != -1)
            {
                justJumped = 0;
                usedJump = false;
            }
        }
    }
}