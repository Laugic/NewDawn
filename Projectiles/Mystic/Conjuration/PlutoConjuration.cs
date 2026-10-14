using System;
using NewDawn.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class PlutoConjuration : ConjurationProjectile
    {
        public bool stopped = false;
        public int damage = 0;
        public bool growing = true;

        public override void SetDefaults()
        {
            growing = true;
            stopped = false;
            damage = Projectile.damage;
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 8 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (growing)
            {
                if (Projectile.scale < 1.5f)
                {
                    Projectile.scale += 0.02f;
                }
                else growing = false;
            }
            else
            {
                if (Projectile.scale > .75f)
                {
                    Projectile.scale -= 0.02f;
                }
                else growing = true;
            }
            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Projectile.velocity.X *= .92f;
            Projectile.velocity.Y *= .92f;
            if(Math.Abs(Projectile.velocity.X) <= .2 && Math.Abs(Projectile.velocity.Y) <= .2)
            {
                stopped = true;
            }
            if (stopped)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    if(Main.rand.Next(5) == 0)
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X - Projectile.width / 2 + Main.rand.Next(Projectile.width + 1), Projectile.Center.Y - Projectile.height / 2 + Main.rand.Next(Projectile.height + 1), 0, 8, ModContent.ProjectileType<PlutoConjuration3>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                }
            }
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Frost>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        }
    }
}