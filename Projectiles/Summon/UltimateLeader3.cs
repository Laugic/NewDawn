using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Summon
{

    public class UltimateLeader3 : HoverShooter
    {
        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0;
        public float vMag = 0;
        public int index = 0;
        public float theta = 0f;
        public int reload = 0;
        public int reloadMax = 60;
        public override void SetStaticDefaults()
        {
            reload = 0;
            reloadMax = 60;
            Main.projFrames[Projectile.type] = 3;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            theta = 0f;
            index = 0;
            vMax = 14;
            vAccel = .1f;
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            inertia = 12f;
            shoot = ModContent.ProjectileType<UltimateLeader4>();
            shootCool = 30f;
            shootSpeed = 18f;
        }

        public override void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.dead)
            {
                modPlayer.UltraBoisSummon = false;
            }
            if (modPlayer.UltraBoisSummon)
            {
                Projectile.timeLeft = 2;
            }
        }

        public override void CreateDust()
        {
            Projectile.rotation = 1f/2f*3.14f;
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }

        public override void Behavior()
        {
            Player player = Main.player[Projectile.owner];
            index = player.ownedProjectileCounts[ModContent.ProjectileType<UltimateLeader2>()];

            theta += 3.14f / 60f;
            float mag = index / 2f + 2;
            //Movement
            Projectile.position.X = player.Center.X - Projectile.width * 2 - 4;
            Projectile.position.Y = player.position.Y - 32 - mag - mag * (float)Math.Sin(theta);

            //Attacks
            if (reload > 0)
                reload--;
            if (index < 6)
            {
                reloadMax = 70 - 2 * index;
                if(reload <= 0)
                {
                    reload = reloadMax;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + Projectile.width * 2 - 5 + Main.rand.Next(10), Projectile.Center.Y, 0, 0, ModContent.ProjectileType<UltimateLeader4>(), (int)(Projectile.damage * index / 2), 3, Main.myPlayer);

                }
            }
            else if (index < 12)
            {
                reloadMax = 50 - index;
                if (reload <= 0)
                {
                    reload = reloadMax;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + Projectile.width * 2 - 5 + Main.rand.Next(10), Projectile.Center.Y, 0, 0, ModContent.ProjectileType<UltimateLeader5>(), (int)(Projectile.damage * index / 3), 3, Main.myPlayer);

                }
            }
            else
            {
                reloadMax = 30;
                if (reload <= 0)
                {
                    reload = reloadMax;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + Projectile.width * 2 - 5 + Main.rand.Next(10), Projectile.Center.Y - 5 + Main.rand.Next(10), 0, 0, ModContent.ProjectileType<UltimateLeader6>(), (int)(Projectile.damage * index / 4), 3, Main.myPlayer);

                }
            }


            //Frame
            if (index < 6)
            {
                Projectile.frame = 0;
            }
            else if (index < 12)
            {
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frame = 2;
            }
            Projectile.netUpdate = true;
        }
    }
}