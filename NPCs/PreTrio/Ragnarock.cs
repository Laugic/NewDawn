using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
    public class Ragnarock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ragnarock");
        }

        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.alpha = 0;
            Projectile.timeLeft = 240;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.ai[0] = 0;
            Projectile.scale = 1f + 2 * Main.rand.NextFloat();
        }

        public override void AI()
        {
            if (Main.rand.Next(4) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[1] = 1 - 2 * Main.rand.Next(2);
                Projectile.ai[0] = 1;
            }
            Projectile.rotation += 0.02f * Projectile.ai[1];
        }
    }
}