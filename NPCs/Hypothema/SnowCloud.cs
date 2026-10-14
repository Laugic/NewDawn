using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    public class SnowCloud : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snow Cloud");
        }

        public override void SetDefaults()
        {
            Projectile.width = 192;
            Projectile.height = 110;
            Projectile.timeLeft = 32 * 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 8;
        }

        public override void AI()
        {
            if (!(Main.npc[(int)Projectile.ai[1]].type == ModContent.NPCType<Hypothema>()) || !Main.npc[(int)Projectile.ai[1]].active)
                Projectile.Kill();
            FrameAnimation();
            //if (Main.npc[(int)projectile.ai[1]].type == ModContent.NPCType<Hypothema>() && Main.npc[(int)projectile.ai[1]].active)
            {
                //    if (Main.player[projectile.owner].Center.Y < projectile.Center.Y)
                var newPos = Projectile.position;
                newPos.X += Main.rand.Next(Projectile.width);
                newPos.Y += (Projectile.height / 2) + Main.rand.Next(Projectile.height / 3);
                var entitySource = new EntitySource_Parent(this.Entity);
                if (Main.rand.Next(4) == 0)
                    Projectile.NewProjectile(entitySource, newPos, new Vector2(0, 2), ModContent.ProjectileType<QuietSnowball>(), Projectile.damage, 7, 255, 1);
            }
        }

        private void FrameAnimation()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 7)
            {
                Projectile.frame = 0;
            }
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Frostburn, (3 * 60) + Main.rand.Next(60), true);
            target.AddBuff(BuffID.Chilled, (3 * 60) + Main.rand.Next(60), true);
        }
    }
}