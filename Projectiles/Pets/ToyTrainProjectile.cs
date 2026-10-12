using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;

namespace NewDawn.Projectiles.Pets
{
	public class ToyTrainProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Toy Train");
			Main.projFrames[Projectile.type] = 18;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BabyGrinch);
            Projectile.height = 92;
            Projectile.width = 168;
            AIType = ProjectileID.BabyGrinch;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.grinch = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);

			if (player.dead)
				modPlayer.ToyTrain = false;

			if (modPlayer.ToyTrain)
				Projectile.timeLeft = 2;

            if (Math.Abs(Projectile.velocity.Y) < 1f && Math.Abs(Projectile.velocity.X) > 1f)
            {
                float dist = 0;
                Rectangle rect = Projectile.getRect();
                if (Projectile.spriteDirection == 1)
                    dist = -24;
                else
                    dist = 24;
                Dust.NewDust(new Vector2(rect.X + Projectile.width / 2 + dist, rect.Y), 0, 0, ModContent.DustType<TrainSteam>());
            }
            /*if (Math.Abs(projectile.velocity.Y) > 1f)
            {
                Rectangle rect = projectile.getRect();
                Dust.NewDust(new Vector2(rect.X, rect.Y+projectile.height), projectile.width, 0, ModContent.DustType<Steam>());
            }*/
        }
        
    }
}