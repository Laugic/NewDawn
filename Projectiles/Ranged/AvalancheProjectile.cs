using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using NewDawn.Buffs;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Ranged
{
    public class AvalancheProjectile : ModProjectile
    {
        public bool powered = false;
        public int power = 1;
        public int damage = 0;

        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            power = 3;
            powered = false;
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }


        public override void AI()
        {
            if (!powered)
            {
                power = Main.rand.Next(3, 6);
                powered = true;
            }

            Projectile.velocity.Y += .8f;
            if (Main.rand.Next(2) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            while (power > 0)
            {
                power -= 1;
                float theta = (float)Main.rand.Next(440) / 70f;
                float mag = (float)(Main.rand.Next(4, 7));
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, mag * (float)Math.Cos(theta), mag * (float)Math.Sin(theta), ModContent.ProjectileType<FrostballProjectile>(), Projectile.damage, 3f, Main.myPlayer);
            }

            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 2 * 60);
            target.AddBuff(ModContent.BuffType<Frostbite>(), 2 * 60);
            while (power > 0)
            {
                power -= 1;
                float theta = (float)Main.rand.Next(440) / 70f;
                float mag = (float)(Main.rand.Next(4, 7));
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, mag * (float)Math.Cos(theta), mag * (float)Math.Sin(theta), ModContent.ProjectileType<FrostballProjectile>(), Projectile.damage, 3f, Main.myPlayer);
            }

            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}