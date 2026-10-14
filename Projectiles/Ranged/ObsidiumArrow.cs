using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace NewDawn.Projectiles.Ranged
{
	public class ObsidiumArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Obsidium Arrow");     
		}

		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 36;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 600;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return false;
		}

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int damage = Projectile.damage;
            float theta = (float)(Main.rand.NextDouble() * 2 * Math.PI);
            float mag = 6f;
            if(Main.rand.Next(2) == 0 && Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<ObsidiumArrowHead>(), damage, 3f, Main.myPlayer);
            theta = (float)(Main.rand.NextDouble() * 2 * Math.PI);
            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<ObsidiumArrowHead>(), damage, 3f, Main.myPlayer);
            theta = (float)(Main.rand.NextDouble() * 2 * Math.PI);
            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<ObsidiumArrowHead>(), damage, 3f, Main.myPlayer);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 120, true);
        }
    }
}
