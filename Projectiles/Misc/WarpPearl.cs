using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Misc
{
    public class WarpPearl : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.aiStyle = 0;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.scale= .5f;
            AIType = 48;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .1f;
            if(Math.Abs(Projectile.velocity.X) > 6)
                Projectile.velocity.X *= .985f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            Main.player[Projectile.owner].Teleport(new Vector2(Projectile.Center.X, Projectile.Center.Y - 16));
            SoundEngine.PlaySound(SoundID.Item107, Projectile.position);
            return false;
        }
    }
}
