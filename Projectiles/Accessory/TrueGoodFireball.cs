using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NewDawn.Projectiles.Accessory
{
    public class TrueGoodFireball : ModProjectile
    {
        public bool stopped = false;
        int delay = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Fireball");
        }

        public override void SetDefaults()
        {
            stopped = false;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 160;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.light = .5f;
            delay = 20;
        }

        public override void AI()
        {
            if (stopped)
            {
                Projectile.velocity.X *= .8f;
                Projectile.velocity.Y = 0;
                Projectile.scale *= .98f;
                if (Projectile.scale <= .05f)
                    Projectile.Kill();
            }
            else if(delay <= 0)
            {
                Projectile.velocity.Y += Projectile.ai[0];
                Projectile.ai[0] += .008f;
                Projectile.velocity.X *= .99f;
            }
            else
            {
                delay--;
            }

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            stopped = true;
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 4 * 60);
        }
    }
}
