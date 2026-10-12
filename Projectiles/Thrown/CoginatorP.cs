using NewDawn.Buffs;
using NewDawn.Items.Weapons.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class CoginatorP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 1;
            AIType = 507;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            {
                Projectile.Kill();
                if(Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<Coginator>(), 1);
                }
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            return false;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Steamy>(), 120);
        }
    }
}