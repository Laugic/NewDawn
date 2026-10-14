using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using NewDawn.Dusts;
using NewDawn.Projectiles.Special;

namespace NewDawn.Projectiles.Melee
{
    public class Borealis : ModProjectile
    {

        public override void SetDefaults()
        {
            //damage = projectile.damage;
            //mystDmg = (float)projectile.damage;
            //mystDur = 1f + projectile.knockBack;
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
        }


        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Main.rand.Next(2) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int power = Main.rand.Next(4, 7);
            if ((NewDawnWorld.downedEtheria || NewDawnPlayer.Get(Main.player[Projectile.owner]).Etherable > 0) && NewDawnWorld.downedTrueEtheria)
                    power *= 2;
            while (power > 0)
            {
                power -= 1;
                float theta = (float)Main.rand.Next(440) / 70f;
                float mag = (float)(Main.rand.Next(4, 7));
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, mag * (float)Math.Cos(theta), mag * (float)Math.Sin(theta), ModContent.ProjectileType<BysmalBlast>(), Projectile.damage, 3f, Main.myPlayer);
            }

            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int power = Main.rand.Next(4, 7);
            if ((NewDawnWorld.downedEtheria || NewDawnPlayer.Get(Main.player[Projectile.owner]).Etherable > 0) && NewDawnWorld.downedTrueEtheria)
                power *= 2;
            while (power > 0)
            {
                power -= 1;
                float theta = (float)Main.rand.Next(440) / 70f;
                float mag = (float)(Main.rand.Next(4, 7));
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, mag * (float)Math.Cos(theta), mag * (float)Math.Sin(theta), ModContent.ProjectileType<BysmalBlast>(), Projectile.damage, 3f, Main.myPlayer);
            }

            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}