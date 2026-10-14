using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class AndeshiardThrownProjectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;      
            Projectile.extraUpdates = 1;
            AIType = 48;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {                                                           
            {
                Projectile.Kill();

                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            return false;
        }
    }
}