using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Dusts;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class FrostbiteBreath : ModProjectile
    {
        public bool bitherial = true;

        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            int dustID = Dust.NewDust(new Vector2(Projectile.position.X + Projectile.velocity.X, Projectile.position.Y + Projectile.velocity.Y), Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 3f * Projectile.scale);
            Main.dust[dustID].noGravity = true;
        }
    }
}