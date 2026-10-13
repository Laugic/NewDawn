using System;
using Terraria;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.SoulStone
{
    public class FriendlyDungeonGuardian : ModProjectile
    {
        float theta = 0;
        bool collided = false;

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            if (!npc.active || collided || Projectile.damage == 0)
                Despawn();
            else
                HomeIn(npc);
        }

        public void Despawn()
        {
            Projectile.damage = 0;
            Projectile.velocity.X *= .95f;
            Projectile.velocity.Y += .05f;
            if (Projectile.position.Y - Main.player[Projectile.owner].position.Y > 500)
                Projectile.Kill();
        }

        public void HomeIn(NPC npc)
        {
            Projectile.rotation += (float)Math.PI / 15;

            Projectile.velocity = Projectile.DirectionTo(npc.Center) * 6;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.whoAmI == Projectile.ai[0])
                collided = true;
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}