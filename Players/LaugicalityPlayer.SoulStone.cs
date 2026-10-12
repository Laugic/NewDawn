//using NewDawn.Buffs;
//using NewDawn.Projectiles.SoulStone;
//using Microsoft.Xna.Framework;
//using System;
//
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace NewDawn
//{
//    public sealed partial class NewDawnPlayer
//    {
//        private const int HONEY_BASE_LIFE_REGEN = 2;
//        public const string 
//            FOCUS_NAME_CAPACITY = "Capacity",
//            FOCUS_NAME_VITALITY = "Vitality",
//            FOCUS_NAME_TENACITY = "Tenacity",
//            FOCUS_NAME_MOBILITY = "Mobility",
//            FOCUS_NAME_UTILITY = "Utility",
//            FOCUS_NAME_FEROCITY = "Ferocity";

//        internal void ResetSoulStoneEffects()
//        {
//            KingSlimeStomp = false;
//            HoneyRegenMultiplier = 1;
//            SharkronEffect = false;
//            EvilBossEffect = false;
//            SkeletronEffect = false;
//            WallOfFleshEffect = false;
//            TwinsEffect = false;
//            DestroyerEffect = false;
//            SkeletronPrimeEffect = false;
//            MoonLordEffect = false;
//            Lovestruck = false;
//            SteamTrainEffect = false;
//            GolemEffect = false;
//            DestroyerCooldown = false;
//            HypothemaEffect = false;
//            QueenBeeEffect = false;
//            FishronEffect = false;
//            EtheriaEffect = false;
//            CancelNoKnockback = false;

//            if (!Player.HasBuff(ModContent.BuffType<MoonLordSoulCooldownBuff>()))
//                MoonLordLifeMult = 1f;

//            AnDioCapacityEffect = false;
//            DestroyerCapacityEffect = false;
//            if (DungeonGuardianCounter > 0)
//                DungeonGuardianCounter--;

//            CapacityCurse1 = false;
//            CapacityCurse2 = false;
//            MobilityCurse2 = false;
//            VitalityCurse2 = false;
//            TenacityCurse2 = false;
//            CapacityCurse3 = false;
//            UtilityCurse3 = false;
//            VitalityCurse3 = false;
//            VitalityCurse4 = false;
//            if(MobilityCurse4Check > 0)
//            {
//                MobilityCurse4 = true;
//                MobilityCurse4Check--;
//            }
//            else
//                MobilityCurse4 = false;
//            CapacityCurse4 = false;
//        }

//        internal void UpdateSoulStoneLifeRegen()
//        {
//            if (Player.honey)
//                Player.lifeRegen += HONEY_BASE_LIFE_REGEN * HoneyRegenMultiplier - HONEY_BASE_LIFE_REGEN;

//            if(!Player.immune)
//            {
//                CapacityCurse4Applied = false;
//                MoonLordEffectApplied = false;
//            }

//            if (Focus != null)
//            {
//                if (Focus.IsCapacity())
//                {
//                    if (SkeletronPrimeEffect && Player.ownedProjectileCounts[ModContent.ProjectileType<FriendlyDungeonGuardianPrime>()] <= 0 && Player.statLife <= Player.statLifeMax2 / 2)
//                    {
//                        Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center, new Vector2(0, 0), ModContent.ProjectileType<FriendlyDungeonGuardianPrime>(), 99, 4, Player.whoAmI);
//                    }

//                    if (GolemEffect && Player.ownedProjectileCounts[ModContent.ProjectileType<FriendlyGolemProj>()] <= 0 && Player.statLife <= Player.statLifeMax2 / 2)
//                    {
//                        Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center, new Vector2(0, 0), ModContent.ProjectileType<FriendlyGolemProj>(), 0, 4, Player.whoAmI);
//                    }

//                    if (MoonLordEffect && Player.ownedProjectileCounts[ModContent.ProjectileType<FriendlyTrueEyeProj>()] <= 0 && Player.statLife <= Player.statLifeMax2 / 2)
//                    {
//                        Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center, new Vector2(0, 0), ModContent.ProjectileType<FriendlyTrueEyeProj>(), (int)(150 * GetGlobalDamage()), 4, Player.whoAmI);
//                    }
//                }
//            }
//        }

//        internal bool SoulStonePreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
//        {
//            if(CancelNoKnockback)
//                Player.noKnockback = false;

//            if (Focus == null)
//                return true;

//            if (Focus.IsCapacity())
//                CapacityPreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);

//            if (Focus.IsMobility())
//            {
//                if (SkeletronEffect && Main.rand.Next(25) == 0)
//                {
//                    Player.immune = true;
//                    Player.immuneTime = 2 * 60;
//                    return false;
//                }

//                if (MoonLordEffect)
//                {
//                    int mobChance = 60;
//                    mobChance -= (int)(Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y));

//                    if (mobChance < 3)
//                        mobChance = 3;

//                    if (Main.rand.Next(mobChance) == 0)
//                    {
//                        Player.immune = true;
//                        Player.immuneTime = 2 * 60;

//                        return false;
//                    }
//                }
//            }

//            if (DestroyerEffect && !Player.HasBuff(ModContent.BuffType<DestroyerSoulCooldownBuff>()) && damage >= 50 && damage > Player.statLife)
//            {
//                Player.AddBuff(ModContent.BuffType<DestroyerSoulCooldownBuff>(), 90 * Constants.TICKS_PER_SECONDS);
//                Player.immune = true;
//                Player.immuneTime = 2 * 60;

//                return false;
//            }

//            if (Focus.IsVitality())
//            {
//                if (SteamTrainEffect && !Player.HasBuff(ModContent.BuffType<SteamTrainSoulCooldownBuff>()))
//                {
//                    if (Player.statLife < Player.statLifeMax2)
//                    {
//                        Player.statLife = Player.statLifeMax2;
//                        Player.AddBuff(ModContent.BuffType<SteamTrainSoulCooldownBuff>(), 150 * Constants.TICKS_PER_SECONDS);
//                        Player.immune = true;
//                        Player.immuneTime = 2 * 60;

//                        return false;
//                    }
//                }

//                if (MoonLordEffect && Player.statLifeMax2 > 100 && damage >= Player.statLife)
//                {
//                    MoonLordLifeMult *= .5f;
//                    Player.statLifeMax2 = (int) (MoonLordLifeMult * Player.statLifeMax2);
//                    Player.statLife = Player.statLifeMax2;
//                    Player.AddBuff(ModContent.BuffType<MoonLordSoulCooldownBuff>(), 90 * Constants.TICKS_PER_SECONDS);
//                    Player.immune = true;
//                    Player.immuneTime = 2 * 60;

//                    return false;
//                }
//            }

//            if (MoonLordEffect && !Player.HasBuff(ModContent.BuffType<MoonLordSoulCooldownBuff>()) && Focus.IsTenacity())
//            {
//                Player.AddBuff(ModContent.BuffType<MoonLordSoulCooldownBuff>(), 30 * Constants.TICKS_PER_SECONDS);
//                Player.immune = true;
//                Player.immuneTime = 2 * 60;

//                return false;
//            }

//            return true;
//        }

//        internal void CapacityPreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
//        {
//            if (CapacityCurse1)
//                damage += 5;
//            if(WallOfFleshEffect && !Player.HasBuff(ModContent.BuffType<WallOfFleshEffectCooldownBuff>()) && damage > 1)
//            {
//                damage = 1;
//                Player.AddBuff(ModContent.BuffType<WallOfFleshEffectCooldownBuff>(), 120 * Constants.TICKS_PER_SECONDS);
//            }
//            if (EtheriaEffect)
//            {
//                Player.AddBuff(ModContent.BuffType<EtherialEffectCooldownBuff>(), 20 * 60, false);
//            }
//        }

//        internal void SoulStonePostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
//        {
//            if (Focus == null)
//                return;

//            if (MobilityCurse2)
//                Player.AddBuff(ModContent.BuffType<MobilityCurseBuff>(), 4 * Constants.TICKS_PER_SECONDS);

//            if (TenacityCurse2)
//                Player.AddBuff(ModContent.BuffType<TenacityCurseBuff>(), 4 * Constants.TICKS_PER_SECONDS);

//            /*if (QueenBeeEffect && Focus.IsTenacity())
//            {
//                if(player.HasBuff(BuffID.Poisoned))
//                {
//                    player.buffImmune[BuffID.Poisoned] = true;
//                    player.statLife += 10;
//                }

//                if (player.HasBuff(BuffID.Venom))
//                {
//                    player.buffImmune[BuffID.Venom] = true;
//                    player.statLife += 10;
//                }
//            }*/

//            if (DefenseCounter > 0)
//                DefenseCounter = 0;

//            if (Focus.IsCapacity())
//                CapacityPostHurt(pvp, quiet, damage, hitDirection, crit);
//        }

//        internal void CapacityPostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
//        {
//            if (CapacityCurse2)
//                Player.AddBuff(ModContent.BuffType<CapacityCurseBuff>(), 10 * Constants.TICKS_PER_SECONDS);
//            if(CapacityCurse4 && !CapacityCurse4Applied && Player.immune)
//            {
//                CapacityCurse4Applied = true;
//                Player.immuneTime /= 2;
//            }
//            if(MoonLordEffect && !MoonLordEffectApplied && Player.immune)
//            {
//                MoonLordEffectApplied = true;
//                Player.immuneTime *= 2;
//            }
//            if (QueenBeeEffect && Player.ownedProjectileCounts[ModContent.ProjectileType<CapacityThornsProj>()] <= 0)
//            {
//                for (int i = 0; i < 8; i++)
//                {
//                    Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center, new Vector2(0, 0), ModContent.ProjectileType<CapacityThornsProj>(), (int)(15 * GetGlobalDamage()), 5f, Player.whoAmI, (float)(Math.PI / 4) * i);
//                }
//            }
//            if (TwinsEffect && Player.ownedProjectileCounts[ModContent.ProjectileType<ShadowDoubleProj>()] <= 0)
//            {
//                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center, new Vector2(0, 0), ModContent.ProjectileType<ShadowDoubleProj>(), (int)(60 * GetGlobalDamage()), 0, Player.whoAmI);
//            }
//            if (DestroyerCapacityEffect && Player.statLife < Player.statLifeMax2 * .66)
//            {
//                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center, new Vector2(0, 0), ModContent.ProjectileType<FriendlyProbeProj>(), (int)(60 * GetGlobalDamage()), 4f, Player.whoAmI);
//            }
//            if (FishronEffect)
//            {
//                int rand = Main.rand.Next(5, 9);
//                for (int i = 0; i < rand; i++)
//                {
//                    float theta = -Main.rand.NextFloat() * (float)Math.PI;
//                    Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center, new Vector2((float)Math.Cos(theta) * 6, (float)Math.Sin(theta) * 6), ProjectileID.MiniSharkron, (int)(75 * GetGlobalDamage()), 4f, Player.whoAmI);
//                }
//            }
//            if (EtheriaEffect)
//            {
//                Player.AddBuff(ModContent.BuffType<EtherialEffectCooldownBuff>(), 20 * 60, false);
//            }
//        }

//        internal void SoulStoneBadLifeRegen()
//        {
//            if (HypothemaEffect && Player.lifeRegen < 0 && (Focus.IsVitality() || Focus.IsTenacity()))
//                Player.statDefense += 8;

//            if(UtilityCurse3 && Player.lifeRegen < 0)
//            {
//                Player.lifeRegen -= Player.lifeRegen / 2;
//            }

//            if (VitalityCurse2 && Player.lifeRegen < 0)
//            {
//                Player.lifeRegen -= 2;
//            }

//            if (VitalityCurse3 && Player.statLife >= Player.statLifeMax2 / 2)
//            {
//                Player.lifeRegen = 0;
//            }

//            if (VitalityCurse4)
//            {
//                if (Player.statLifeMax2 > Player.statLifeMax)
//                    Player.statLifeMax2 = Player.statLifeMax;
//            }

//            if (EvilBossEffect && Focus.IsVitality())
//            {
//                if (Player.lifeRegen < 0)
//                    Player.lifeRegen += 2;
//                if(Player.lifeRegen > -1)
//                    Player.lifeRegen = -1;
//            }

//            if (MoonLordEffect && Focus.IsUtility())
//            {
//                if (Player.lifeRegen < 0)
//                    Player.lifeRegen = 0;
//            }
//        }

//        internal void SoulStoneHitByNPC(NPC npc, ref int damage, ref bool crit)
//        {
//            if (Focus == null)
//                return;

//            if (Focus.IsCapacity())
//                CapacityHitByNPC(npc, ref damage, ref crit);

//            if (SteamTrainEffect && !Player.HasBuff(ModContent.BuffType<SteamTrainSoulCooldownBuff>()) && Focus.IsCapacity())
//            {
//                Player.AddBuff(ModContent.BuffType<SteamTrainSoulCooldownBuff>(), 90 * Constants.TICKS_PER_SECONDS);
//                Player.immune = true;
//                Player.immuneTime = 2 * 60;
//            }

//            if (EvilBossEffect && !Player.HasBuff(ModContent.BuffType<EvilBossCooldownBuff>()) && Focus.IsUtility())
//            {
//                Player.AddBuff(ModContent.BuffType<EvilBossCooldownBuff>(), 120 * Constants.TICKS_PER_SECONDS);
//                Player.immune = true;
//                Player.immuneTime = 2 * 60;
//            }

//            if(KingSlimeStomp && Player.velocity.Y > 4)
//            {
//                Player.ApplyDamageToNPC(npc, Player.statDefense + damage + 4, 4f, 0, false);
//            }
//        }

//        internal void CheckMobilityCurse4()
//        {
//            if(MobilityCurse4)
//            {
//                Player.wingTime = -1;
//                Player.wingTimeMax = -1;
//                Player.rocketTimeMax = -1;
//                Player.rocketTime = -1;
//            }
//        }

//        internal void CheckFerocityCurse4()
//        {
//            if (NoBuffedDamage)
//            {
//                if (Player.GetDamage(DamageClass.Generic) > 1)
//                    Player.GetDamage(DamageClass.Generic) = 1;
//                if (Player.GetDamage(DamageClass.Melee) > 1)
//                    Player.GetDamage(DamageClass.Melee) = 1;
//                if (Player.GetDamage(DamageClass.Ranged) > 1)
//                    Player.GetDamage(DamageClass.Ranged) = 1;
//                if (Player.GetDamage(DamageClass.Summon) > 1)
//                    Player.GetDamage(DamageClass.Summon) = 1;
//                if (Player.GetDamage(DamageClass.Magic) > 1)
//                    Player.GetDamage(DamageClass.Magic) = 1;
//                if (Player.GetDamage(DamageClass.Throwing) > 1)
//                    Player.GetDamage(DamageClass.Throwing) = 1;
//                if (MysticDamage > 1)
//                    MysticDamage = 1;
//                if (Player.GetDamage(DamageClass.Generic) > 1)
//                    Player.GetDamage(DamageClass.Generic) = 1;
//                if (Player.GetDamage(DamageClass.Melee) > 1)
//                    Player.GetDamage(DamageClass.Melee) = 1;
//                if (Player.GetDamage(DamageClass.Ranged) > 1)
//                    Player.GetDamage(DamageClass.Ranged) = 1;
//                if (Player.GetDamage(DamageClass.Summon) > 1)
//                    Player.GetDamage(DamageClass.Summon) = 1;
//                if (Player.GetDamage(DamageClass.Magic) > 1)
//                    Player.GetDamage(DamageClass.Magic) = 1;
//                if (Player.GetDamage(DamageClass.Throwing) > 1)
//                    Player.GetDamage(DamageClass.Throwing) = 1;
//            }
//        }

//        internal void CapacityHitByNPC(NPC npc, ref int damage, ref bool crit)
//        {
//            if(Player.statLife > Player.statLifeMax2 / 2 && CapacityCurse3)
//            {
//                damage = (int)(damage * 1.5);
//            }

//            if(SharkronEffect)
//            {
//                if(!npc.noGravity)
//                npc.velocity.Y = -15;
//            }

//            if(DungeonGuardianCounter <= 0 && SkeletronEffect)
//            {
//                if(npc.boss)
//                    DungeonGuardianCounter = 10 * 60;
//                else
//                    DungeonGuardianCounter = 1 * 60;

//                float theta = Main.rand.NextFloat() * 2 * (float)Math.PI;
//                float mag = 700;

//                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center + new Vector2((float)(Math.Cos(theta) * mag), (float)(Math.Sin(theta) * mag)), new Vector2(0, 0), ModContent.ProjectileType<FriendlyDungeonGuardian>(), 999, 5f, Player.whoAmI, npc.whoAmI);
//            }
//        }

//        public int HoneyRegenMultiplier { get; set; }
//        public int Counter { get; set; }
//        public int DungeonGuardianCounter { get; set; }
//        public bool CancelNoKnockback { get; set; }

//        public bool KingSlimeStomp { get; set; }
//        public bool SharkronEffect { get; set; }
//        public bool EvilBossEffect { get; set; }
//        public bool HypothemaEffect { get; set; }
//        public bool QueenBeeEffect { get; set; }
//        public bool SkeletronEffect { get; set; }
//        public bool WallOfFleshEffect { get; set; }
//        public bool TwinsEffect { get; set; }
//        public bool DestroyerEffect { get; set; }
//        public bool SkeletronPrimeEffect { get; set; }
//        public bool DestroyerCooldown { get; set; }
//        public bool Lovestruck { get; set; }
//        public int SlybertronCounter { get; set; }
//        public bool SteamTrainEffect { get; set; }
//        public float GolemBoost { get; set; }
//        public bool GolemEffect { get; set; }
//        public float MoonLordLifeMult { get; set; }
//        public float DefenseCounter { get; set; }
//        public bool FishronEffect { get; set; }
//        public bool EtheriaEffect { get; set; }
//        public bool MoonLordEffect { get; set; }
//        public bool MoonLordEffectApplied { get; set; }
//        public int AbilityCount { get; set; } = 0;

//        public bool AnDioCapacityEffect { get; set; }
//        public bool DestroyerCapacityEffect { get; set; }


//        public bool CapacityCurse1 { get; set; }
//        public bool CapacityCurse2 { get; set; }
//        public bool MobilityCurse2 { get; set; }
//        public bool MobilityCurse4 { get; set; }
//        public int MobilityCurse4Check { get; set; } = 0;
//        public bool VitalityCurse2 { get; set; }
//        public bool TenacityCurse2 { get; set; }
//        public bool CapacityCurse3 { get; set; }
//        public bool UtilityCurse3 { get; set; }
//        public bool VitalityCurse3 { get; set; }
//        public bool VitalityCurse4 { get; set; }
//        public bool CapacityCurse4 { get; set; }
//        public bool CapacityCurse4Applied { get; set; }
//    }
//}
