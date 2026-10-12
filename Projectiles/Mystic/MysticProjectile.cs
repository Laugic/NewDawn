using NewDawn.Players;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic
{
    public abstract class MysticProjectile : ModProjectile
    {
        bool durationed = false;
        public float duration = 1;
        public bool overflowed = false;
        public int buffID = 0;
        public int baseDuration = 8 * 60;


        public override void SetDefaults()
        {
            overflowed = false;
            durationed = false;
            duration = 1;
        }

        public override bool PreAI()
        {
            if (!durationed)
            {
                duration = Main.player[Projectile.owner].GetModPlayer<MysticPlayer>().MysticDuration;
                durationed = true;
                Durationed(duration);
                overflowed = CheckOverflow();
            }
            if (overflowed)
            {
                OverflowEffects();
            }
            return true;
        }

        private bool CheckOverflow()
        {
            MysticPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<MysticPlayer>();
            return modPlayer.IsOnOverflow();
        }

        private void OverflowEffects()
        {
            MysticPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<MysticPlayer>();
            //if (modPlayer.ShroomOverflow > 0)
            //    projectile.tileCollide = false;
        }

        public virtual void Durationed(float dur)
        {

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (buffID != 0)
                target.AddBuff(buffID, (int)(baseDuration * duration) + Main.rand.Next(1 * 60));

            MysticPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<MysticPlayer>();

            //if (modPlayer.Incineration > 0)
            //    target.AddBuff(ModContent.BuffType<Incineration>(), (int)(baseDuration * duration) + Main.rand.Next(1 * 60));
            //if (modPlayer.SporeShard > 0)
            //    target.AddBuff(ModContent.BuffType<Spored>(), (int)(baseDuration * duration) + Main.rand.Next(1 * 60));
            //if (modPlayer.MysticShroomBurst && Main.myPlayer == Projectile.owner && Projectile.type != ModContent.ProjectileType<OverflowSpore>() && overflowed)
            //    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, -10f, ModContent.ProjectileType<OverflowSpore>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
        }
    }
}
