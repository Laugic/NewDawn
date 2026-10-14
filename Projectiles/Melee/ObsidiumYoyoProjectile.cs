using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
	public class ObsidiumYoyoProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 9f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 240f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 14f;
		}

		public override void SetDefaults()
		{
			Projectile.extraUpdates = 0;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 99;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.scale = 1f;
		}

        public override void AI()
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(Main.player[Projectile.owner]);
            if (modPlayer.ObsidiumHeart > 0 && Main.rand.Next(4 * 60 - (3 * 60 * (int)(modPlayer.ObsidiumHeart / 5f))) == 0 && Main.myPlayer == Projectile.owner)
            {
                float theta = Main.rand.NextFloat() * (float)Math.PI;
                float mag = Main.rand.NextFloat() * 4 + 8;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<ObsidiumYoyoFireball>(), Projectile.damage, 3f, Main.myPlayer);
            }
            base.AI();
        }

        public Vector2 GetPosition()
        {
            return Projectile.position;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(Main.player[Projectile.owner]);
            if (modPlayer.ObsidiumHeart > 0)
                target.AddBuff(BuffID.OnFire, 2 * 60 + Main.rand.Next(60));
        }
    }
}
