using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace NewDawn.Projectiles.Summon
{
	public class UltimateLeader4 : ModProjectile
    {
        public bool spawned = false;
        public Vector2 targetPos;

		public override void SetDefaults()
        {
            spawned = false;
            Projectile.width = 16;               
			Projectile.height = 16;              
			Projectile.aiStyle = -1;             
			Projectile.friendly = true;         
			Projectile.hostile = false;         
			Projectile.DamageType = DamageClass.Ranged;           
			Projectile.penetrate = 2;           
			Projectile.timeLeft = 120;            
			Projectile.ignoreWater = true;         
			Projectile.tileCollide = false;          
		}

        public override void AI()
        {
            if (!spawned)
            {
                spawned = true;
                float distance = 10000f;
                foreach (NPC npc in Main.npc)
                {
                    if (Vector2.Distance(Projectile.Center, npc.Center) < distance && !npc.friendly && npc.damage > 0 && !npc.dontTakeDamage)
                    {
                        targetPos = npc.Center;
                        distance = Vector2.Distance(Projectile.Center, npc.Center);
                    }
                }
                if (distance == 10000f)
                    targetPos = Main.MouseWorld;
                Projectile.velocity = Projectile.DirectionTo(targetPos) * 18f;
            }

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
            
        }
        
    }
}
