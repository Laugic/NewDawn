using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class JanusConjuration : ConjurationProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Janus Arrow");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            if(Main.rand.Next(4) == 0)
            {
                Vector2 newVel = -Projectile.velocity * 2 / 3f;
                float theta = Main.rand.Next(-30, 30) * (float)Math.PI / 180;
                float mag = Main.rand.Next(3, 7);
                newVel.X += (float)Math.Cos(theta) * mag;
                newVel.Y += (float)Math.Sin(theta) * mag;
                int dust = Dust.NewDust(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Sandy>(), newVel.X, newVel.Y);
                Main.dust[dust].scale *= .5f;
            }
        }

        public override void OnKill(int timeLeft)
        {

            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<JanusConjurationBottom>(), Projectile.damage, 0, Main.myPlayer);
        }
    }
}
