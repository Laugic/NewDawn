using System;
using System.Collections.Generic;
using NewDawn.Buffs;
using NewDawn.Dusts;
using NewDawn.NPCs.Hypothema;
using NewDawn.NPCs.Bosses;
using NewDawn.NPCs.PreTrio;
using NewDawn.NPCs.RockTwins;
using NewDawn.NPCs.Slybertron;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;

namespace NewDawn
{
    public sealed partial class NewDawnPlayer
    {
        public void CycleBysmalPowers(int newPower)
        {
            if(!BysmalPowers.Contains(newPower))
            {
                if (BysmalPowers.Count > 2)
                    BysmalPowers.RemoveAt(0);

                BysmalPowers.Add(newPower);
            }
        }
        
        private void ResetEtherial()
        {
            AnnihilationBoost = false;
            JusticeCooldown = false;
            EtherialGel = false;
            EtherVision = false;
            EtherialScarf = false;
            EtherialScarfCooldown = false;
            EtherialBrain = false;
            EtherialBrainCooldown = false;
            EtherialFrost = false;
            EtherialBees = false;
            EtherialMagma = false;
            EtherialBones = false;
            EtherBonesBoost = false;
            EtherialAnDio = false;
            EtherialTwins = false;
            EtherialDestroyer = false;
            EtherialPrime = false;
            EtherCog = false;
            EtherialPipes = false;
            EtherialTank = false;
            EtherialSpores = false;
            EtherialStone = false;
            EtherialTruffle = false;

            if(Etherable > 0)
                Etherable -= 1;
        }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            CheckBysmalPowers();

            modifiers.ModifyHurtInfo += (ref Player.HurtInfo info) =>
            {
                if (EtherialBrain && !EtherialBrainCooldown && (NewDawnWorld.downedEtheria || Etherable > 0))
                {
                    npc.AddBuff(ModContent.BuffType<FragmentedMind>(), 15 * 60, false);

                    if (info.Damage >= Player.statLife)
                    {
                        if (EtherialBrain && !EtherialBrainCooldown && (NewDawnWorld.downedEtheria || Etherable > 0))
                        {
                            Player.AddBuff(ModContent.BuffType<FragmentedMind>(), 60 * 60 * 3, true);
                            Player.immune = true;
                            Player.immuneTime = 2 * 60;
                            Player.statLife += 300;

                            SoundEngine.PlaySound(NewDawn.CreateSoundStyle("Sounds/EtherialChange"));

                            for (int i = 0; i < 20; i++)
                                Dust.NewDust(Player.position + Player.velocity, Player.width, Player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
                        }
                    }
                }
            };

            if(EtherialTank)
            {
                npc.life -= (int)(Math.Abs(Player.velocity.X) * 500);

                if (npc.life <= 0)
                    npc.life = 1;
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)/* tModPorter Override ImmuneTo, FreeDodge or ConsumableDodge instead to prevent taking damage */
        {
            CheckBysmalPowers();

            modifiers.ModifyHurtInfo += (ref Player.HurtInfo info) =>
            {
                if (EtherialBones)
                {
                    Player.AddBuff(ModContent.BuffType<EtherBones>(), 10 * 60, true);
                    EtherBonesDamageBoost += ((float)info.Damage / (float)Player.statLifeMax2);
                }

                if (EtherialTwins && !JusticeCooldown)
                {
                    Player.AddBuff(ModContent.BuffType<JusticeCooldown>(), 90 * 60, true);
                    Player.statLife += info.Damage;
                    Player.immune = true;
                    Player.immuneTime = 2 * 60;

                    info.Cancelled = true;
                    return;
                }

                if (info.Damage >= Player.statLife)
                {
                    if (EtherialScarf && !EtherialScarfCooldown && (NewDawnWorld.downedEtheria || Etherable > 0))
                    {
                        Player.AddBuff(ModContent.BuffType<EtherialScarfCooldown>(), 60 * 60 * 1, true);
                        SoundEngine.PlaySound(NewDawn.CreateSoundStyle("Sounds/EtherialChange"));

                        for (int i = 0; i < 20; i++)
                            Dust.NewDust(Player.position + Player.velocity, Player.width, Player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);

                        Player.immune = true;
                        Player.immuneTime = 2 * 60;

                        info.Cancelled = true;
                    }
                }
            };
        }

        // TODO Use BysmalPower class.
        private void CheckBysmalPowers()
        {
            if(fullBysmal > 0)
            {
                if (BysmalPowers.Contains(NPCID.KingSlime))
                    EtherialGel = true;

                if (BysmalPowers.Contains(NPCID.EyeofCthulhu))
                    EtherVision = true;

                if (BysmalPowers.Contains(NPCID.EaterofWorldsHead))
                    EtherialScarf = true;

                if (BysmalPowers.Contains(NPCID.BrainofCthulhu))
                    EtherialBrain = true;

                if (BysmalPowers.Contains(ModContent.NPCType<Hypothema>()))
                    EtherialFrost = true;

                if (BysmalPowers.Contains(NPCID.QueenBee))
                    EtherialBees = true;

                if (BysmalPowers.Contains(ModContent.NPCType<Ragnar>()))
                    EtherialMagma = true;

                if (BysmalPowers.Contains(NPCID.SkeletronHead))
                    EtherialBones = true;

                if (BysmalPowers.Contains(ModContent.NPCType<AnDio3>()))
                    EtherialAnDio = true;

                if (BysmalPowers.Contains(NPCID.Retinazer) || BysmalPowers.Contains(NPCID.Spazmatism))
                    EtherialTwins = true;

                if (BysmalPowers.Contains(NPCID.TheDestroyer))
                    EtherialDestroyer = true;

                if (BysmalPowers.Contains(NPCID.SkeletronPrime))
                    EtherialPrime = true;

                if (BysmalPowers.Contains(ModContent.NPCType<TheAnnihilator>()))
                    EtherCog = true;

                if (BysmalPowers.Contains(ModContent.NPCType<Slybertron>()))
                    EtherialPipes = true;

                if (BysmalPowers.Contains(ModContent.NPCType<NPCs.SteamTrain.SteamTrainOld>()))
                    EtherialTank = true;

                if (BysmalPowers.Contains(NPCID.Plantera))
                    EtherialSpores = true;

                if (BysmalPowers.Contains(NPCID.Golem))
                    EtherialStone = true;

                if (BysmalPowers.Contains(NPCID.DukeFishron))
                    EtherialTruffle = true;

                if (BysmalPowers.Contains(NPCID.MoonLordCore))
                    Etherable = 2;
            }
        }

        private void GetEtherialAccessories()
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(Player);

            if(EtherialGel)
            {
                Player.jumpSpeedBoost += 5.0f;
                Player.moveSpeed += .5f;
                Player.maxRunSpeed += 3f;
            }

            if (EtherialMagma && Player.lavaWet)
                Player.AddBuff(ModContent.BuffType<EtherialRagnar>(), 15 * 60);

            if (EtherialAnDio)
                modPlayer.zProjImmune = true;

            if (EtherCog)
                Player.maxMinions += 4;

            if(EtherialPipes)
            {
                Player.GetDamage(DamageClass.Throwing) += .30f;
                Player.ThrownVelocity += .5f;
            }

            if (EtherialTank)
            {
                Player.jumpSpeedBoost += 3.0f;
                Player.maxRunSpeed += 4f;
                Player.moveSpeed += 4f;

            }

            if (EtherialTruffle)
            {
                Player.gills = true;
                Player.ignoreWater = true;
                Player.accFlipper = true;

                if (Player.wet)
                {
                    Player.jumpSpeedBoost += 3.0f;
                    Player.moveSpeed += .5f;
                    Player.maxRunSpeed += 3f;
                }
            }
        }


        private void GetEtherialAccessoriesPost()
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(Player);

            if (EtherialBees && Player.honey)
            {
                modPlayer.HoneyRegenMultiplier *= 3;
                Player.statDefense += 15;

                const float dmgBoost = .15f;

                Player.GetDamage(DamageClass.Generic) += dmgBoost;
            }

            if (EtherialMagma)
                Player.statLifeMax2 = (int)(Player.statLifeMax2 * 1.25f);

            if (EtherialBones)
            {
                if(EtherBonesBoost)
                {
                    if (EtherBonesDamageBoost > 1)
                        EtherBonesDamageBoost = 1;

                    Player.GetDamage(DamageClass.Generic) += EtherBonesDamageBoost;
                }
                else
                    EtherBonesDamageBoost = 0;
            }

            if (EtherialAnDio)
                modPlayer.zProjImmune = true;

            if (EtherialDestroyer)
            {
                int originalDef = Player.statDefense;

                float globalDmg = Player.GetDamage(DamageClass.Generic).ApplyTo(1f) - 1f;

                if (globalDmg > 0)
                {
                    if (globalDmg > 2)
                        globalDmg = 2;

                    Player.statDefense += (int)(originalDef * globalDmg);
                }
            }

            if (EtherialPrime)
            {
                float lifePercentage = (float)(Player.statLifeMax2 - Player.statLife) / (float)Player.statLifeMax2;

                Player.GetDamage(DamageClass.Generic) += lifePercentage;
            }

            if (EtherCog)
            {
                if (AnnihilationBoost)
                {
                    if (AnnihilationDamageBoost > 1)
                        AnnihilationDamageBoost = 1;

                    Player.GetDamage(DamageClass.Generic) += AnnihilationDamageBoost;
                }
                else
                    AnnihilationDamageBoost = 0;
            }

            if (EtherialTank)
            {
                float moveSpeed = (float)Math.Abs(Player.velocity.X) / 25f;

                Player.GetDamage(DamageClass.Generic) += moveSpeed;
            }

            if (EtherialSpores)
            {
                if (Player.grapCount > 0)
                {
                    GrappleReturned = false;
                    Player.lifeRegen += 15;

                    const float dmgBoost = .5f;

                    Player.GetDamage(DamageClass.Generic) += dmgBoost;
                }
            }

            if (EtherialStone)
            {
                Player.lifeRegen += 18;
                Player.statDefense += 20;
                Player.statLifeMax2 += Player.statDefense;
            }

            if (EtherialTruffle)
            {
                if(Player.wet)
                {
                    Player.lifeRegen += 12;
                    Player.statDefense += 35;

                    const float dmgBoost = .4f;

                    Player.GetDamage(DamageClass.Generic) += dmgBoost;
                }
            }
        }
        
        // TODO Change this to BysmalPower class.
        public List<int> BysmalPowers { get; internal set; } = new List<int>();


        #region Etherial Accessories

        public bool EtherialGel { get; set; }

        public bool EtherVision { get; set; }

        public bool EtherialScarf { get; set; }

        public bool EtherialScarfCooldown { get; set; }

        public bool EtherialBrain { get; set; }

        public bool EtherialBrainCooldown { get; set; }

        public bool EtherialFrost { get; set; }

        public bool EtherialBees { get; set; }

        public bool EtherialMagma { get; set; }

        public bool EtherialBones { get; set; }

        public bool EtherialAnDio { get; set; }

        public bool EtherialTwins { get; set; }

        public bool EtherialDestroyer { get; set; }

        public bool EtherialPrime { get; set; }

        public bool EtherCog { get; set; }

        public bool EtherialPipes { get; set; }

        public bool EtherialTank { get; set; }

        public bool EtherialSpores { get; set; }

        public bool EtherialStone { get; set; }

        public bool EtherialTruffle { get; set; }

        public int Etherable { get; set; }

        public float EtherBonesDamageBoost { get; set; }

        public bool EtherBonesBoost { get; set; }

        public bool GrappleReturned { get; set; }

        public bool JusticeCooldown { get; set; }

        public bool AnnihilationBoost { get; set; }

        public float AnnihilationDamageBoost { get; set; }

        #endregion
    }
}
