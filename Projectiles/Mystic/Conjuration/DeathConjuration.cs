using System;
using NewDawn.Dusts;
using NewDawn.NPCs.RockTwins;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class DeathConjuration : PrimaryConjurationProjectile
    {
        public Color colorType;
        float rotSpd = .4f;
        Vector2 offset = new Vector2();
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 12 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            rotSpd = (.3f + Main.rand.NextFloat() * .2f);
        }

        public override void AI()
        {
            Visuals();
            Movement();
            Effects();
        }

        private void Effects()
        {

        }

        private void Movement()
        {
            Player player = Main.player[Projectile.owner];
            var dist = (player.Center - Projectile.Center);
            if (dist.Length() > 150)
            {
                if (player.velocity.Length() < 1)
                    offset = new Vector2(-40 + Main.rand.Next(80), -40 + Main.rand.Next(80));
                Projectile.velocity = (player.Center + offset + player.velocity - Projectile.Center) / (20 + rotSpd * 4);
            }
            else if (Projectile.velocity.Length() > 2)
                Projectile.velocity *= .9f;
        }

        private void Visuals()
        {
            if(Projectile.velocity.X > 0)
                Projectile.rotation += rotSpd;
            else
                Projectile.rotation -= rotSpd;

            if (Projectile.timeLeft < 80)
            {
                Projectile.alpha += 2;
            }
        }

        public void Burst()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 20; i++)
                {
                    var theta = (float)(Math.PI * Main.rand.NextDouble() * 2);
                    Vector2 newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                    newVel *= 10;
                    int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, newVel.X, newVel.Y, ModContent.ProjectileType<DeathConjurationSkull>(), Projectile.damage, 3, Main.myPlayer);
                    Main.projectile[id].ai[1] = 3;
                }
            }
            Projectile.timeLeft += 3 * 60;
        }
    }
}
