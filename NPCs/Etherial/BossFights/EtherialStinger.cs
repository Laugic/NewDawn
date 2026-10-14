using System;
using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class EtherialStinger : ModProjectile
    {
        public int grounded = 0;
        public int delay = 4;
        public bool bitherial = true;
        bool powered = false;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Etherial Stinger");
        }

        public override void SetDefaults()
        {
            powered = false;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            delay = 4;
            grounded = 0;
            Projectile.width = 12;
            Projectile.height = 12;
            //projectile.alpha = 255;
            Projectile.timeLeft = 150;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            bitherial = true;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);

            bitherial = true;
            Projectile.rotation -= 6;
            if (!powered)
            {
                SoundEngine.PlaySound(SoundID.Item51, Projectile.position);
                Vector2 move = Vector2.Zero;
                float distance = 2400f;
                bool target = false;
                for (int k = 0; k < 8; k++)
                {
                    if (Main.player[k].active)
                    {
                        Vector2 newMove = Main.player[k].Center - Projectile.Center;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance)
                        {
                            move = newMove;
                            distance = distanceTo;
                            target = true;
                        }
                    }
                }
                if (target)
                {
                    AdjustMagnitude(ref move);
                    Projectile.velocity = (16 * Projectile.velocity + move) / 11f;
                    AdjustMagnitude(ref Projectile.velocity);
                }
                Projectile.velocity *= 24;
                powered = true;
            }
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 6f / magnitude;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (NewDawnWorld.downedEtheria)
            {
                target.AddBuff(ModContent.BuffType<Frostbite>(), 4 * 60, true);
            }
        }
    }
}