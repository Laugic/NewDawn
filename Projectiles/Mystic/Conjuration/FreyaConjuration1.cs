using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class FreyaConjuration1 : PrimaryConjurationProjectile
    {
        public bool stopped = false;
        public int damage = 0;
        public int sporeTimer = 0;

        public override void SetDefaults()
        {
            stopped = false;
            damage = Projectile.damage;
            Projectile.width = 44;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60 * 5;
            Main.projFrames[Projectile.type] = 2;
            Projectile.ignoreWater = true;
            NewDawnVars.ShroomProjectiles.Add(Projectile.type);
        }

		public override void PostDraw(Color lightColor)
        {
			Rectangle frame = new Rectangle(0, 0, 48, 38);
			frame.Y += 38 * Projectile.frame;	
			
			spriteBatch.Draw(Mod.GetTexture("Projectiles/Mystic/Conjuration/FreyaConjuration1_Glow"), Projectile.Center - Main.screenPosition, frame, Color.White * 0.25f, Projectile.rotation, new Vector2(22, 17), 1f, SpriteEffects.None, 0f);
		}

        public override bool PreAI()
        {
            Projectile.tileCollide = true;
            return true;
        }
		
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
			
			Lighting.AddLight(Projectile.position, 0.1f, 0.1f, 0.4f);
			
            Projectile.velocity.X *= 0.9f;
			Projectile.velocity.Y += 0.5f;
			Projectile.rotation = 0;

            if (stopped)
            {
                sporeTimer++;
                if (sporeTimer >= 15)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, -10f, ModContent.ProjectileType<FreyaConjuration2>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
					}
					sporeTimer = 0;
                }
            }
        }
		
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 56, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 50, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 4, Main.rand.Next((int)-3f, (int)3f), Main.rand.Next((int)-3f, (int)3f), 100, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false; 
			return true;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			stopped = true;
			return false;
		}
		
		public override void PostAI()
        {         
			if (stopped)
            {
				Projectile.frameCounter++;
				if (Projectile.frameCounter > 5)
				{
					Projectile.frame++;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame >= 2)
				{
					Projectile.frame = 0;
					return;
				}
			}
        }
    }
}