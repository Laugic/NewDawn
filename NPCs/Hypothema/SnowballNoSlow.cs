using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    public class SnowballNoSlow : ModProjectile
    {
        public static float Gravity = .08f;
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 5 * 60;
        }

        public override void AI()
        {
            if (Projectile.scale != 1)
                return;
            Movement();

            Projectile.rotation += Projectile.velocity.X / 40f;
        }

        private void Movement()
        {
            Projectile.velocity.Y += Gravity;
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[1] = Main.rand.Next((int)Math.Abs(Projectile.velocity.X * 6), (int)Math.Abs(Projectile.velocity.X * 8));
                Projectile.ai[0] = 1;
            }
            if (Math.Abs(Main.player[Projectile.owner].Center.X - Projectile.Center.X) < Projectile.ai[1])
                Projectile.velocity.X *= .925f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            if (Projectile.ai[0] == 0)
                SoundEngine.PlaySound(SoundID.Item51, Projectile.Center);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (!Main.expertMode)
                return;
            target.AddBuff(BuffID.Frostburn, (2 * 60) + Main.rand.Next(60));
        }
    }
}
