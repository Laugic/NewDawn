using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
//using SubworldLibrary;
//using NewDawn.Subworlds.MoldyrTemple.Rooms;
using Terraria.ModLoader.IO;
using Terraria;
using NewDawn.Temple;
using SubworldLibrary;
using Terraria.Audio;
using NewDawn.Biomes;
using NewDawn.Temple.RoomGeneration;

namespace NewDawn.Players
{
    public class SpiritPlayer : ModPlayer
    {
        private static float SPIRIT_BASE_DECAY = 1f / (60f * 60f * 10f / 100) * 10; //The max time should take 10 Mins. * 10 is for testing

        public override void ResetEffects()
        {
            SpiritDecayRate = SPIRIT_BASE_DECAY;
            MaxSpirit = 100;
        }

        public override void Initialize()
        {
            base.Initialize();
            Spirit = MaxSpirit;
            ShowSpirit = false;
        }

        private void SpiritRegen()
        {
            //if (Player.InModBiome<TempleBiome>())
            if (SubworldSystem.IsActive<TempleWorld>() || SubworldSystem.IsActive<ExploreRoom>() || SubworldSystem.IsActive<ParkourRoom>())
            {
                ShowSpirit = true;
                if (Spirit > 0)
                    Spirit -= SpiritDecayRate;
                else
                    Spirit = 0;
            }
            else
            {
                if (Spirit < MaxSpirit)
                {
                    Spirit += SpiritRecoverRate;
                    ShowSpirit = true;
                }
                else
                {
                    Spirit = MaxSpirit;
                    ShowSpirit = false;
                }
            }
        }
        /*
        public override void UpdateBadLifeRegen()
        {
            base.UpdateBadLifeRegen();
            if(BelowPainThreshold)
            {
                Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.bleed = true;
            }
            if (BelowEmptyThreshold && Player.statLife < Player.statLifeMax2)
                Player.statLifeMax2 = Math.Max(Player.statLife, 1);
        }
        
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            base.ModifyHitByNPC(npc, ref damage, ref crit);
            if (BelowHurryThreshold)
                damage = (int)(damage * 1.5);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            base.ModifyHitByProjectile(proj, ref damage, ref crit);
            if (BelowHurryThreshold)
                damage = (int)(damage * 1.5);
        }*/

        public override void PostUpdate()
        {
            SpiritRegen();
            base.PostUpdate();
            /*
            if (BelowPainThreshold)
                Player.statDefense = Math.Max( Math.Min(Player.statDefense - 20, Player.statDefense / 2), 0);
            if (BelowGuardianThreshold)
                GuardianSpawnMult = 1f;
            if (BelowHurryThreshold)
                GuardianSpawnMult /= 2;
            EncounterBuffs();*/
        }

        public override void UpdateDead()
        {
            base.UpdateDead();
            Spirit = MaxSpirit;
            /*if (Subworld.IsActive<MTParentSubworld>())
                Subworld.Exit();*/
        }

        public float Spirit { get; set; } = 100;
        public float MaxSpirit { get; set; } = 100;
        public bool ShowSpirit { get; set; } = false;
        public float SpiritRecoverRate { get; set; } = 1f;
        public float SpiritDecayRate { get; set; } = SPIRIT_BASE_DECAY;

        #region thresholds
        public float GuardianThreshold { get; set; } = .75f;
        public float EncounterBuffThreshold { get; set; } = .67f;
        public float PainThreshold { get; set; } = .5f;
        public float BossThreshold { get; set; } = .33f;
        public float HurryThreshold { get; set; } = .15f;
        public float EmptyThreshold { get; set; } = .001f;
        public bool BelowGuardianThreshold { get; set; } = false;
        public bool BelowEncounterBuffThreshold { get; set; } = false;
        public bool BelowPainThreshold { get; set; } = false;
        public bool BelowBossThreshold { get; set; } = false;
        public bool BelowHurryThreshold { get; set; } = false;
        public bool BelowEmptyThreshold { get; set; } = false;
        #endregion
    }
}
