using NewDawn.Items.Weapons.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class TheSnowflakeProjectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 6;
            Projectile.extraUpdates = 1;
            AIType = 507;
        }
        

        public override void OnKill(int timeLeft)
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(Projectile.GetSource_Death(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<TheSnowflake>(), 1);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 120);       
        }
    }
}