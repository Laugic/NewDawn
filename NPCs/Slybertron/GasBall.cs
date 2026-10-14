using NewDawn.Buffs;
using NewDawn.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Slybertron
{
	public class GasBall : ModProjectile
	{
        public int delay = 0;
        public int damage = 0;
        public bool bitherial = true;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gas Ball");
            //ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            Projectile.width = 48;
			Projectile.height = 48;
			//projectile.alpha = 255;
            Projectile.timeLeft = 240;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            damage = Projectile.damage;
        }

        public override void AI()
        {
            bitherial = true;
            delay += 1;
            if (delay == 30)
            {
                SoundEngine.PlaySound(SoundID.Item34, Projectile.position);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, -8, ModContent.ProjectileType<GasBallUp>(), damage, 3f, Main.myPlayer);
                delay = 0;
            }
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Steam>(), 0f, 0f);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            //NPCs.Slybertron.Slybertron.electroShockHits += 1;
            int debuff = ModContent.BuffType<Steamy>();
            if (debuff >= 0)
            {
                target.AddBuff(debuff, 90, true);
            }      //Add Onfire buff to the NPC for 1 second
        }
    }
}