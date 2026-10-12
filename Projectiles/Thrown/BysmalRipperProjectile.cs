using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class BysmalRipperProjectile : ModProjectile
    {
        public bool justSpawned = false;
        Vector2 initVel;
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Projectile.velocity.Y += .25f;
            if(!justSpawned)
            {
                initVel = Projectile.velocity;
                justSpawned = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(Main.myPlayer == Projectile.owner)
            {
                float theta = (float)Main.rand.NextDouble() * 3.14f * 2;
                float mag = 120;
                if ((NewDawnWorld.downedEtheria || NewDawnPlayer.Get(Main.player[Projectile.owner]).Etherable > 0) && NewDawnWorld.downedTrueEtheria)
                {
                    theta = (float)Main.rand.NextDouble() * 3.14f * 2;
                    mag = 120;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), target.Center.X + (int)(mag * Math.Cos(theta)), target.Center.Y + (int)(mag * Math.Sin(theta)), -8 * (float)Math.Cos(theta), -8 * (float)Math.Sin(theta), ModContent.ProjectileType<BysmalRipperShadow>(), damage, 3, Main.myPlayer);
                }
                theta = (float)Main.rand.NextDouble() * 3.14f * 2;
                mag = 120;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), target.Center.X + (int)(mag * Math.Cos(theta)), target.Center.Y + (int)(mag * Math.Sin(theta)), -8 * (float)Math.Cos(theta), -8 * (float)Math.Sin(theta), ModContent.ProjectileType<BysmalRipperShadow>(), damage, 3, Main.myPlayer);
            }
        }
    }
}