using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.BossSummons
{
	public class GeneralBossSpawn : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 1;
			Projectile.scale = 1f;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 20;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
            NewDawnVars.zProjectiles.Add(Projectile.type);
        }
		
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.ai[1]++;
			
			if (Projectile.ai[1] >= 0)
			{
				SoundEngine.PlaySound(SoundID.Roar, new Vector2(player.position.X, (int)player.position.Y-50));
                if(Projectile.damage != NPCID.WallofFlesh)
				NPC.SpawnOnPlayer(player.whoAmI, (int)Projectile.damage);
                else
                {
                    if(player.position.X > Main.maxTilesX / 2)
                        NPC.NewNPC((int)player.position.X + 160, (int)player.position.Y, NPCID.WallofFlesh);
                    else
                        NPC.NewNPC((int)player.position.X - 160, (int)player.position.Y, NPCID.WallofFlesh);
                }
				Projectile.ai[1] = -30;
			}
		}
	}
}