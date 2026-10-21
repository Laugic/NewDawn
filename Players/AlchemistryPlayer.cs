using Microsoft.Xna.Framework;
using NewDawn.Buffs;
using NewDawn.Items.Alchemistry.Essences;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace NewDawn.Players
{
    public class AlchemistryPlayer : ModPlayer
    {
        public bool Vita = false, Ignis = false;
        public int BonusHealFromHearts { get; set; } = 0;

        public int FatalityCharges { get; private set; }

        public bool ConvertsHealingPotions => Vita || FatalityCharges > 0;
        public static bool IsHealingPotion(Item item) => item.potion && item.healLife > 0;
        public override void ResetEffects()
        {
            Vita = false;
            BonusHealFromHearts = 0;
            base.ResetEffects();
        }

        public override void PostUpdate()
        {
            UpdateFatalityLife();
            UpdateIgnisVFX();
        }

        private void UpdateFatalityLife()
        {
            int buffType = ModContent.BuffType<Fatality>();

            if (FatalityCharges > 0)
                Player.AddBuff(buffType, 60 * FatalityCharges);
            else
                Player.ClearBuff(buffType);

            double multiplier = Math.Pow(0.5, FatalityCharges);
            Player.statLifeMax2 = Math.Max(1, (int)(Player.statLifeMax2 * multiplier));
            if (Player.statLife > Player.statLifeMax2)
                Player.statLife = Player.statLifeMax2;

        }

        private void UpdateIgnisVFX()
        {
            if (Main.GameUpdateCount % 6 == 0 && Ignis)
            {
                const int particleCount = 32;
                float rotation = (float)Main.GameUpdateCount * 0.015f;

                for (int i = 0; i < particleCount; i++)
                {
                    float angle = rotation + (MathHelper.TwoPi * i / particleCount);
                    Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                    Vector2 position = Player.Center + (direction * PureIgnisCrystal.RANGE);

                    Dust dust = Dust.NewDustDirect(position, 4, 4, DustID.Torch, SpeedX: 0f, SpeedY: -2f, Alpha: 100, Scale: Main.rand.NextFloat(1.5f, 2.2f));

                    dust.noGravity = true;
                    dust.noLight = true;
                }
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (Player.statLifeMax2 >= 100 && Vita)
            {
                FatalityCharges++;
                UpdateFatalityLife();
                Player.immune = true;
                Player.immuneTime = Math.Max(Player.immuneTime, 60);
                Player.statLife = Player.statLifeMax2;
                return false;
            }

            return true;
        }
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if (ConvertsHealingPotions && IsHealingPotion(item))
            {
                healValue = 0;
            }
        }

        public void RemoveFatalityCharge()
        {
            if (FatalityCharges > 0)
            {
                FatalityCharges--;
                UpdateFatalityLife();
            }
        }

        public override void UpdateDead()
        {
            FatalityCharges = 0;
            UpdateFatalityLife();
            Player.ClearBuff(ModContent.BuffType<Fatality>());
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Ignis && damageDone > 0 && Player.DistanceSQ(target.Center) <= PureIgnisCrystal.RANGE * PureIgnisCrystal.RANGE)
            {
                target.AddBuff(ModContent.BuffType<Ablaze>(), 5 * 60);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Ignis && damageDone > 0 && Player.DistanceSQ(target.Center) <= PureIgnisCrystal.RANGE * PureIgnisCrystal.RANGE)
            {
                target.AddBuff(ModContent.BuffType<Ablaze>(), 5 * 60);
            }
        }
    }
}
