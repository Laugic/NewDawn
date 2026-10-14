using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
    public class Ablaze : ModBuff
    {
        public override string Texture =>
            $"Terraria/Images/Buff_{BuffID.OnFire}";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }

    public class AblazeNPC : GlobalNPC
    {
        private const int DamagePerSecond = 12;
        private const float DamageTakenMultiplier = 1.15f;
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<Ablaze>()))
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= DamagePerSecond * 2;
                if (damage < 2)
                    damage = 2;

                for (int i = 0; i < 3; i++)
                {
                    Dust flame = Dust.NewDustDirect(
                        npc.position,
                        npc.width,
                        npc.height,
                        DustID.Torch,
                        SpeedX: 0f,
                        SpeedY: -2f,
                        Alpha: 50,
                        Scale: Main.rand.NextFloat(1.5f, 2.2f)
                    );

                    flame.noGravity = true;
                    flame.velocity.X *= 0.3f;
                    flame.velocity.Y = -Main.rand.NextFloat(2f, 4f);

                    // Carry some of the enemy's movement into the flames.
                    flame.velocity += npc.velocity * 0.2f;
                }

                // Occasional smaller, faster embers.
                if (Main.rand.NextBool(3))
                {
                    Dust ember = Dust.NewDustDirect(
                        npc.position,
                        npc.width,
                        npc.height,
                        DustID.Torch,
                        Scale: 0.8f
                    );

                    ember.noGravity = true;
                    ember.velocity.X = Main.rand.NextFloat(-1.5f, 1.5f);
                    ember.velocity.Y = -Main.rand.NextFloat(4f, 6f);
                }
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<Ablaze>()))
                modifiers.FinalDamage *= DamageTakenMultiplier;
        }
    }
}