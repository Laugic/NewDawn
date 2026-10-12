using NewDawn.Buffs;
using NewDawn.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Plague
{
    public class JunglePlagueSporeSpread : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Jungle Plague Spore");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
        }

        public override void AI()
        {
            Projectile.velocity *= .98f;
            Projectile.alpha += 3;
            if (Projectile.alpha > 250)
                Projectile.Kill();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int rand = Main.rand.Next(5);
            if (target.GetGlobalNPC<LaugicalGlobalNPCs>().JunglePlagueDuration < 180 + 60 * rand)
            {
                target.AddBuff(ModContent.BuffType<JunglePlagueBuff>(), (int)((180 + 60 * rand)), false);
                target.AddBuff(BuffID.Poisoned, (int)(3 * 60 + 60 * rand), false);
            }
        }
    }
}