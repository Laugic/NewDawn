using NewDawn.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Slybertron
{
	public class XOut : ModProjectile
    {
        public int delay = 25;
        public int damage = 0;
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("X-Out");
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
            Projectile.timeLeft = 120;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            delay = 0;
            damage = 40;
        }

		public override void AI()
        {
            bitherial = true;
            delay += 1;
            if (delay == 30 && Main.netMode != 1)
            {
                SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X + 3, Projectile.velocity.Y + 3, ModContent.ProjectileType<ElectroshockP2>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X + 3, Projectile.velocity.Y - 3, ModContent.ProjectileType<ElectroshockP2>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X - 3, Projectile.velocity.Y - 3, ModContent.ProjectileType<ElectroshockP2>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X - 3, Projectile.velocity.Y + 3, ModContent.ProjectileType<ElectroshockP2>(), damage, 3f, Main.myPlayer);
                delay = 0;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            //NPCs.Slybertron.Slybertron.steamStreamHits += 1;
            int debuff = ModContent.BuffType<Steamy>();
            if (debuff >= 0)
            {
                target.AddBuff(debuff, 90, true);
            }      //Add Onfire buff to the NPC for 1 second
        }
    }
}