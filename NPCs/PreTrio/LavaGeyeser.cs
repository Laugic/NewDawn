using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
    class LavaGeyeser : ModProjectile
    {
        int delay = 0;
        public override void SetDefaults()
        {
            delay = 0;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.alpha = 0;
            Projectile.timeLeft = 480;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Vector2 velocity;
            velocity.Y = -20;
            velocity.X = -1 + (float)Main.rand.NextDouble() * 2;
            if (Main.rand.Next(4) == 0)// && Main.myPlayer == projectile.owner)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, velocity, ModContent.ProjectileType<Lava>(), Projectile.damage, 5f);
            delay++;
            if(delay == 60)
            {
                velocity.Y = -16;
                velocity.X = -3;
                //if (Main.myPlayer == projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, velocity, ModContent.ProjectileType<GravityFireball>(), Projectile.damage, 5f);
                velocity.X = 3;
                //if (Main.myPlayer == projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, velocity, ModContent.ProjectileType<GravityFireball>(), Projectile.damage, 5f);
            }
            if(delay > 119)
            {
                delay = 0;
                velocity.Y = -18;
                velocity.X = -2;
                //if (Main.myPlayer == projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, velocity, ModContent.ProjectileType<GravityFireball>(), Projectile.damage, 5f);
                velocity.X = 2;
                //if (Main.myPlayer == projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, velocity, ModContent.ProjectileType<GravityFireball>(), Projectile.damage, 5f);
                velocity.Y = -16;
                velocity.X = -5;
                //if (Main.myPlayer == projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, velocity, ModContent.ProjectileType<GravityFireball>(), Projectile.damage, 5f);
                velocity.X = 5;
                //if (Main.myPlayer == projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, velocity, ModContent.ProjectileType<GravityFireball>(), Projectile.damage, 5f);
            }
        }
    }
}
