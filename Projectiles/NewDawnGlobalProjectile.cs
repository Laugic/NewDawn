using NewDawn.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles
{
    public class NewDawnGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            InflictDebuffs(projectile, target, hit, damageDone);
        }

        private void InflictDebuffs(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            AccessoryPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<AccessoryPlayer>();
            if (Main.myPlayer == projectile.owner)
            {
                if (modPlayer.SetOnFire)
                    target.AddBuff(BuffID.OnFire, (int)(5 * 60 + Main.rand.Next(60)), false);
            }
        }
    }
}
