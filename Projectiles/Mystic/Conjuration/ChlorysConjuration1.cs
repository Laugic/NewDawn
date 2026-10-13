using NewDawn.Items.Consumables.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class ChlorysConjuration1 : PrimaryConjurationProjectile
    {
        int delay = 0;
        bool right = false;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 360;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            if (Main.rand.Next(2) == 0)
                right = false;
        }

        public override void AI()
        {
            Projectile.rotation += right?.1f:-.2f;
            Projectile.velocity.X *= .95f;
            Projectile.velocity.Y *= .95f;

            delay++;
            if (delay > 45)
            {
                delay = 0;
                if (Main.myPlayer == Projectile.owner)
                    Shoot();
            }
        }

        private void Shoot()
        {
            double theta = Main.rand.NextDouble() * Math.PI;
            float mag = 5;
            if (Main.rand.Next(16) != 0)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (int)(mag * Math.Cos(theta)), (int)(mag * Math.Sin(theta)), ModContent.ProjectileType<ChlorysConjuration2>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
            else
                Item.NewItem(Projectile.GetSource_Death(), Projectile.Center, ModContent.ItemType<DryadSpore>());
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity.Y = 0;
            return false;
        }
    }
}