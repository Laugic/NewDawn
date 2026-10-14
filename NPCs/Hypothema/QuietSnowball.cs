using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    public class QuietSnowball : ModProjectile
    {
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
            Projectile.velocity.Y += .08f;
            Projectile.velocity.X *= .99f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (!Main.expertMode)
                return;
            target.AddBuff(BuffID.Frostburn, (2 * 60) + Main.rand.Next(60));
        }
    }
}
