using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Melee
{
	public class AuroraProjectile : ModProjectile
    {
        public bool powered = false;
        public int power = 1;
        public int damage = 0;

        public override void SetDefaults()
        {
            power = 3;
            powered = false;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
        }
        
        public override void AI()
        {
            if (!powered)
            {
                power = Main.rand.Next(3, 6);
                powered = true;
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            
            if (Main.rand.Next(2) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Frost>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            while (power > 0)
            {
                power -= 1;
                float theta = (Main.rand.NextFloat() * .4f + 1.3f) * (float)Math.PI;
                float mag = Main.rand.NextFloat() * 4 + 5;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, mag * (float)Math.Cos(theta), mag * (float)Math.Sin(theta), ModContent.ProjectileType<AuroraShard>(), Projectile.damage, 3f, Main.myPlayer);
            }
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 120);
            while (power > 0)
            {
                power -= 1;
                float theta = (Main.rand.NextFloat() * .4f + 1.3f) * (float)Math.PI;
                float mag = Main.rand.NextFloat() * 4 + 5;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, mag * (float)Math.Cos(theta), mag * (float)Math.Sin(theta), ModContent.ProjectileType<AuroraShard>(), Projectile.damage, 3f, Main.myPlayer);
            }
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}