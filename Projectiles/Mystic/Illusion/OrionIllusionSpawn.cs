using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
    public class OrionIllusionSpawn : IllusionProjectile
    {
        int damage = 0;
        int delay = 0;
        int hSpeed = 0;
        int distance = 0;

        public override void SetDefaults()
        {
            distance = 0;
            hSpeed = 0;
            delay = 0;
            damage = Projectile.damage;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 240;
            Projectile.ignoreWater = true;
            Projectile.alpha = 200;
        }

        public override void AI()
        {
            delay++;
            if (delay >= 3)
            {
                delay = 0;
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 24, ModContent.ProjectileType<OrionIllusion>(), Projectile.damage, 3f, Main.myPlayer);
                }
            }
        }
    }
}