using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class HermesConjuration1 : ConjurationProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 1;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<HermesDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Projectile.tileCollide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Main.netMode != 1)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -4 + Main.rand.Next(8), -4 + Main.rand.Next(8), ModContent.ProjectileType<HermesConjurationHoming>(), Projectile.damage, 3f, Main.myPlayer);
            }
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Projectile.owner == Main.myPlayer)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -4 + Main.rand.Next(8), -4 + Main.rand.Next(8), ModContent.ProjectileType<HermesConjurationHoming>(), Projectile.damage, 3f, Main.myPlayer);
            }
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}