using Microsoft.Xna.Framework;
using NewDawn.Buffs;
using NewDawn.Items.Consumables.Potions;
using NewDawn.Items.Weapons.Mystic;
using NewDawn.Projectiles.Mystic.Burst;
using NewDawn.Projectiles.Mystic.Illusion;
using NewDawn.UI;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace NewDawn.Players
{
    public class MysticPlayer : ModPlayer
    {
        public float MysticDamage = 1f, MysticDuration = 1f, PotentiaRegenMult = 1f, IllusionDamage = 1f, DestructionDamage = 1f, ConjurationDamage = 1f,
            LuxRegen = 0.01f, VisRegen = 0.01f, MundusRegen = 0.01f, Lux = 100, Vis = 100, Mundus = 100, OverflowDamage = 1f,
            LuxCostMod = 1f, VisCostMod = 1f, MundusCostMod = 1f,
            LuxOverflow = 1f, VisOverflow = 1f, MundusOverflow = 1f, OverflowCapacity = 1.25f, PotentiaCostMod = 1f;

        public float LuxMax = 100, VisMax = 100, MundusMax = 100;

        public MysticMode CurrentMysticMode = MysticMode.Destruction;

        public enum MysticMode
        {
            Destruction,
            Illusion,
            Conjuration
        }

        public override void ResetEffects()
        {
            if (UsingMysticWeapon > 0)
                UsingMysticWeapon--;
            else
                OrionCharge = 0;

            if (MysticSwitchCool > 0)
                MysticSwitchCool -= MysticSwitchCoolRate;

            if (MysticErupting > 0)
                MysticErupting -= 1;

            if (MysticSpiralBurst > 0)
                MysticSpiralBurst -= 1;

            if (MysticSteamSpiralBurst > 0)
                MysticSteamSpiralBurst -= 1;

            MysticCrit = 4;
            MysticDamage = 1f;
            MysticDuration = 1f;

            if (MysticHold > 0)
                MysticHold -= 1;

            if (ShroomOverflow > 0)
                ShroomOverflow--;

            if (Incineration > 0)
                Incineration--;

            IllusionDamage = 1f;
            DestructionDamage = 1f;
            ConjurationDamage = 1f;

            MysticSwitchCoolRate = 1;
            MysticBurstDamage = 1f;
            MysticSandBurst = false;
            MysticSteamBurst = false;
            MysticShroomBurst = false;
            MysticMarblite = false;
            MysticEruption = false;
            MysticEruptionBurst = false;
            MysticObsidiumSwitch = false;

            if (Mysticality > 0)
                Mysticality -= 1;

            LuxDischargeRate = 1;
            LuxOverflow = 1.25f;
            theta += 3.14f / 60f;
            LuxAbsorbRate = 1;
            VisDischargeRate = 1;
            VisOverflow = 1.25f;
            VisAbsorbRate = 1;
            MundusDischargeRate = 1;
            MundusOverflow = 1.25f;
            MundusAbsorbRate = 1;
            GlobalAbsorbRate = .5f;
            GlobalOverflow = 1f;
            OverflowDamage = 1f;
            OverflowVelocity = 1f;
            AntiflowDamage = 1f;
            Magmatic = false;
            Midnight = false;
            AndioChestplate = false;

            LuxMax = 100;
            VisMax = 100;
            MundusMax = 100;

            LuxUseRate = 1;
            VisUseRate = 1;
            MundusUseRate = 1;
            GlobalPotentiaUseRate = 1;

            if (SporeShard > 0)
                SporeShard -= 1;

            LuxRegen = 0;
            VisRegen = 0;
            MundusRegen = 0;

            LuxUnuseRegen = .01f;
            VisUnuseRegen = .01f;
            MundusUnuseRegen = .01f;
        }

        public override void PostUpdate()
        {
            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("IsHoldingMystic: " + IsHoldingMystic()), Color.White);
            base.PostUpdate();
            PotentiaRegen();

            PostUpdateMysticBursts();
        }

        private void PostUpdateMysticBursts()
        {
            if (MysticErupting > 0)
            {
                if (Main.rand.Next(4) == 0)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, Player.velocity.X - 4 + Main.rand.Next(9), -Main.rand.Next(6, 9), ModContent.ProjectileType<EruptionProjectile>(), (int)(30 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
            }

            /*if (MysticSpiralBurst > 0)
            {
                MysticSpiralDelay++;

                if (MysticSpiralDelay > 2)
                {
                    MysticSpiralDelay = 0;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, 4 * (float)Math.Cos(theta * 2), 4 * (float)Math.Sin(theta * 2), ModContent.ProjectileType<AnDioChestguardBurst>(), (int)(32 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                }
            }
            */
            if (MysticSteamSpiralBurst > 0)
            {
                MysticSteamSpiralDelay++;

                if (MysticSteamSpiralDelay > 5)
                {
                    MysticSteamSpiralDelay = 0;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, 6 * (float)Math.Cos(theta), 6 * (float)Math.Sin(theta), ModContent.ProjectileType<SteamBurst>(), (int)(40 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, 6 * (float)Math.Cos(theta + 3.14f), 6 * (float)Math.Sin(theta + 3.14f), ModContent.ProjectileType<SteamBurst>(), (int)(40 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                }
            }
        }

        private void PotentiaRegen()
        {
            switch (CurrentMysticMode)
            {
                case MysticMode.Destruction:
                    Vis += VisRegen;
                    Mundus += MundusRegen;
                    break;
                case MysticMode.Illusion:
                    Lux += LuxRegen;
                    Mundus += MundusRegen;
                    break;
                case MysticMode.Conjuration:
                    Lux += LuxRegen;
                    Vis += VisRegen;
                    break;
            }
            Lux = Math.Min(Lux, GetMaxLux() * LuxOverflow * OverflowCapacity);
            Vis = Math.Min(Vis, GetMaxVis() * VisOverflow * OverflowCapacity);
            Mundus = Math.Min(Mundus, GetMaxMundus() * MundusOverflow * OverflowCapacity);
            if (Player.statLife < 1)
            {
                Lux = GetMaxLux();
                Vis = GetMaxVis();
                Mundus = GetMaxMundus();
            }
        }

        public void MysticSwitch()
        {
            if (!IsHoldingMystic())
                return;
            switch (CurrentMysticMode)
            {
                case MysticMode.Destruction:
                    CurrentMysticMode = MysticMode.Illusion;
                    break;
                case MysticMode.Illusion:
                    CurrentMysticMode = MysticMode.Conjuration;
                    break;
                case MysticMode.Conjuration:
                    CurrentMysticMode = MysticMode.Destruction;
                    break;
            }
            NewDawnUI.CyclePositions(CurrentMysticMode);
            SoundStyle switchSound = new SoundStyle("NewDawn/Sounds/Sounds/MysticSwitch")
            {
                Volume = 0.9f,
                PitchVariance = 0.0f,
                MaxInstances = 3,
            };
            SoundEngine.PlaySound(switchSound, Player.Center);
        }

        public void AddLux(float amount)
        {
            Lux = Math.Min(Lux + amount, GetMaxLux() * LuxOverflow * OverflowCapacity);
        }

        public void AddVis(float amount)
        {
            Vis = Math.Min(Vis + amount, GetMaxVis() * VisOverflow * OverflowCapacity);
        }

        public void AddMundus(float amount)
        {
            Mundus = Math.Min(Mundus + amount, GetMaxMundus() * MundusOverflow * OverflowCapacity);
        }

        public int GetMaxLux()
        {
            return (int)LuxMax; //TODO: Add permanent boosts
        }

        public int GetMaxVis()
        {
            return (int)VisMax; //TODO: Add permanent boosts
        }

        public int GetMaxMundus()
        {
            return (int)MundusMax; //TODO: Add permanent boosts
        }

        public bool IsHoldingMystic()
        {
            return Player.HeldItem.ModItem is Items.Weapons.MysticItem;
        }

        public bool IsOnOverflow()
        {
            if (!IsHoldingMystic())
                return false;

            switch (CurrentMysticMode)
            {
                case MysticMode.Destruction:
                    return Lux > GetMaxLux();
                case MysticMode.Illusion:
                    return Vis > GetMaxVis();
                case MysticMode.Conjuration:
                    return Mundus > GetMaxMundus();
                default:
                    return false;
            }
        }

        public override void PreUpdate()
        {
            if (((Main.tileSolid[Main.tile[(int)(Player.position.X / 16), (int)(Player.position.Y / 16) + 3].TileType] && Main.tile[(int)(Player.position.X / 16), (int)(Player.position.Y / 16) + 3].TileType != 0) ||
                (Main.tileSolid[Main.tile[(int)(Player.position.X / 16) + 1, (int)(Player.position.Y / 16) + 3].TileType] && Main.tile[(int)(Player.position.X / 16) + 1, (int)(Player.position.Y / 16) + 3].TileType != 0)) &&
                Player.velocity.Y >= 0)
                Grounded();

            if (Player.HeldItem.type == ModContent.ItemType<ThorsHammer>() && CurrentMysticMode == MysticMode.Illusion)
            {
                Player.maxFallSpeed += 80;
                if (Player.velocity.Y > 0)
                    Player.velocity.Y += .25f;
            }
        }


        private void Grounded()
        {
            if (Player.HeldItem.type == ModContent.ItemType<ThorsHammer>() && Player.itemAnimation > 0 && Player.velocity.Y > 4 && !ThorCharge && CurrentMysticMode == MysticMode.Illusion)
            {
                ThorCharge = true;
                SoundEngine.PlaySound(SoundID.Item122, Player.position);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X + (Player.direction * 40), Player.Center.Y - 70, 0, 0, ModContent.ProjectileType<ThorSmashIllusion>(), (int)Player.HeldItem.damage, 8, Main.myPlayer);
            }
            if (Player.HeldItem.type != ModContent.ItemType<ThorsHammer>() || Player.itemAnimation == 0 || CurrentMysticMode != MysticMode.Illusion)
                ThorCharge = false;
        }
        /*
        public void MysticSwitch()
        {
            switch (MysticMode)
            {
                case 1:
                    MysticMode = 3;
                    break;
                case 2:
                    MysticMode = 1;
                    break;
                case 3:
                    MysticMode = 2;
                    break;
                default:
                    break;
            }



            if (MysticObsidiumSwitch)
            {
                Player.AddBuff(ModContent.BuffType<ObsidiumArmorBuff>(), 6 * 60);
            }

            if (MysticSwitchCool <= 0 && !MysticBurstDisabled)
            {
                if (MysticShroomBurst)
                {
                    //Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, 2.5f, -6.25f, ModContent.ProjectileType<ShroomBurst>(), (int)(10 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    //Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, 5, -5, ModContent.ProjectileType<ShroomBurst>(), (int)(10 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    //Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, -5, -5, ModContent.ProjectileType<ShroomBurst>(), (int)(10 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    //Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, -2.5f, -6.25f, ModContent.ProjectileType<ShroomBurst>(), (int)(10 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    //Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, 2.5f / 2, -6.75f, ModContent.ProjectileType<ShroomBurst>(), (int)(10 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    //Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, 3.75f, -5.75f, ModContent.ProjectileType<ShroomBurst>(), (int)(10 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    //Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, -3.75f, -5.75f, ModContent.ProjectileType<ShroomBurst>(), (int)(10 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    //Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y, -2.5f / 2, -6.75f, ModContent.ProjectileType<ShroomBurst>(), (int)(10 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);

                    MysticSwitchCool += 1 * 60;
                }

                if (MysticEruption)
                {
                    MysticErupting += 90;
                    MysticSwitchCool += 3 * 60;
                }

                if (Magmatic)
                {
                    MysticErupting += 90;
                    MysticSwitchCool += 3 * 60;
                }

                if (MysticSandBurst)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y + 16, 2, 0, ModContent.ProjectileType<AncientRune>(), (int)(12 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y + 16, -2, 0, ModContent.ProjectileType<AncientRune>(), (int)(12 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);

                    MysticSwitchCool += 3 * 60;
                }

                if (MysticEruptionBurst)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y + 16, 4, 0, ModContent.ProjectileType<EruptionBurst>(), (int)(12 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Player.Center.X, Player.Center.Y + 16, -4, 0, ModContent.ProjectileType<EruptionBurst>(), (int)(12 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    MysticErupting += 45;

                    MysticSwitchCool += 4 * 60;
                }

                //if (AndioChestguard)
                //{
                //    MysticSpiralBurst += 150;
                //    MysticSwitchCool += 4 * 60;
                //}

                if (MysticSteamBurst)
                {
                    MysticSteamSpiralBurst += 145;
                    MysticSwitchCool += 4 * 60;
                }

                if (MysticMarblite)
                {
                    Player.AddBuff(ModContent.BuffType<ForGlory>(), 180 + (int)(120 * MysticDuration));
                    Player.AddBuff(ModContent.BuffType<ForHonor>(), 180 + (int)(120 * MysticDuration));
                }
                //if (MysticSwitchCool > 0)
                //    PostBurstEffects();
            }
            if (!Main.dedServ)
            {
                //MysticPlayer.MysticMode uiMode = MysticMode switch
                //{
                //    1 => MysticPlayer.MysticMode.Destruction,
                //    2 => MysticPlayer.MysticMode.Illusion,
                //    _ => MysticPlayer.MysticMode.Conjuration
                //};
                NewDawnUI.CyclePositions(MysticMode);
            }
        }
        */
        private void Blink()
        {
            var rect = GenerateAngledHitbox(Player.Center + ((Main.MouseWorld - Player.Center).SafeNormalize(Vector2.UnitY) * 10), Main.MouseWorld, 36f, 400f);
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && CheckWithinAngledRectangle(npc.Center, rect))
                    npc.AddBuff(BuffID.OnFire, 2);
            }
        }

        private Vector2[] GenerateAngledHitbox(Vector2 center, Vector2 target, float width, float length)
        {
            var dirTo = Direction(center, target);// (float)Math.Atan2((center.X - target.X), (center.Y - target.Y)) + (float)Math.PI / 2;
            var b1 = center + (new Vector2((float)Math.Cos(dirTo - (Math.PI / 2)), (float)Math.Sin(dirTo - (Math.PI / 2))) * width);
            var b2 = b1 + (new Vector2((float)Math.Cos(dirTo), (float)Math.Sin(dirTo)) * length);
            var t1 = center + (new Vector2((float)Math.Cos(dirTo + (Math.PI / 2)), (float)Math.Sin(dirTo + (Math.PI / 2))) * width);
            var t2 = t1 + (new Vector2((float)Math.Cos(dirTo), (float)Math.Sin(dirTo)) * length);

            var bTtheta = dirTo + ((float)Math.PI / 2);// Direction(b1, t1);// (float)Math.Atan2((b1.X - t1.X), (b1.Y - t1.Y));

            Vector2[] angles = new Vector2[]{
                b1,
                b2,
                t1,
                t2,
                new Vector2(length, width * 2)
            };
            return angles;
        }

        private bool CheckWithinAngledRectangle(Vector2 pos, Vector2[] rect)
        {
            if (rect.Length != 5)
                return false;

            int dust = Dust.NewDust(rect[0], 0, 0, DustID.Smoke);
            Main.dust[dust].velocity *= 0;
            dust = Dust.NewDust(rect[1], 0, 0, DustID.Smoke);
            Main.dust[dust].velocity *= 0;
            dust = Dust.NewDust(rect[2], 0, 0, DustID.Torch);
            Main.dust[dust].velocity *= 0;
            /*dust = Dust.NewDust(rect[0] + rect[2] - rect[1], 0, 0, DustID.Smoke);
            Main.dust[dust].velocity *= 0;*/
            dust = Dust.NewDust(rect[3], 0, 0, DustID.Torch);
            Main.dust[dust].velocity *= 0;

            var result = Direction(rect[0], pos) +
                Direction(rect[1], pos) +
                Direction(rect[2], pos) +
                Direction(rect[3], pos);
            result = Distance(rect[0], pos) +
                 Distance(rect[1], pos) +
                 Distance(rect[2], pos) +
                 Distance(rect[3], pos);
            result -= 2 * (float)Math.Sqrt((rect[4].X * rect[4].X) + (rect[4].Y * rect[4].Y));
            Main.NewText(result.ToString());
            var newResult = Math.Abs(result) < (float)Math.Sqrt(rect[4].Y * rect[4].X) / 2 && (Direction(rect[0], pos) >= Direction(rect[0], rect[1])) && (Direction(rect[2], pos) <= Direction(rect[2], rect[3]));
            Main.NewText(newResult.ToString());
            return newResult;
        }

        private float Distance(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Sqrt(((pos2.Y - pos1.Y) * (pos2.Y - pos1.Y)) + ((pos2.X - pos1.X) * (pos2.X - pos1.X)));
        }

        private float Direction(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Atan2(pos2.Y - pos1.Y, pos2.X - pos1.X);
        }

        //private void PostBurstEffects()
        //{
        //    if (PrismVeil)
        //    {
        //        if (MysticSwitchCool < 3 * 60 * MysticSwitchCoolRate)
        //            MysticSwitchCool = 3 * 60 * MysticSwitchCoolRate;
        //        Player.immune = true;
        //        Player.immuneTime = 60;
        //    }
        //}

        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            tag["LuxMaxPermaBoost"] = LuxMaxPermaBoost;
            tag["VisMaxPermaBoost"] = VisMaxPermaBoost;
            tag["MundusMaxPermaBoost"] = MundusMaxPermaBoost;
            tag["MysticBurstDisabled"] = MysticBurstDisabled;
        }
        public override void LoadData(TagCompound tag)
        {
            LuxMaxPermaBoost = tag.GetFloat("LuxMaxPermaBoost");
            VisMaxPermaBoost = tag.GetFloat("VisMaxPermaBoost");
            MundusMaxPermaBoost = tag.GetFloat("MundusMaxPermaBoost");
            MysticBurstDisabled = tag.GetBool("MysticBurstDisabled");
            Lux = LuxMax + LuxMaxPermaBoost;
            Vis = VisMax + VisMaxPermaBoost;
            Mundus = MundusMax + MundusMaxPermaBoost;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (IsHoldingMystic())
                NewDawnUI.Draw(Main.spriteBatch);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (NewDawn.toggleMystic.JustPressed && IsHoldingMystic())
            {
                MysticSwitch();
                if (NewDawnUI.SoundOn)
                    SoundEngine.PlaySound(NewDawn.CreateSoundStyle("Sounds/MysticSwitch"));
            }

            if (NewDawn.quickMystica.JustPressed && Mysticality == 0)
            {
                bool mysticaPotion = false;

                foreach (Item item in Player.inventory)
                {
                    if (item.type == ModContent.ItemType<SupremeMysticaPotion>())
                    {
                        mysticaPotion = true;
                        item.stack -= 1;
                        SoundEngine.PlaySound(SoundID.Item3, Player.position);
                        if (Lux < (LuxMax + LuxMaxPermaBoost) * (1 + ((LuxOverflow - 1) / 2)))
                            Lux = (LuxMax + LuxMaxPermaBoost) * (1 + ((LuxOverflow - 1) / 2));

                        if (Vis < (VisMax + VisMaxPermaBoost) * (1 + ((VisOverflow - 1) / 2)))
                            Vis = (VisMax + VisMaxPermaBoost) * (1 + ((VisOverflow - 1) / 2));

                        if (Mundus < (MundusMax + MundusMaxPermaBoost) * (1 + ((MundusOverflow - 1) / 2)))
                            Mundus = (MundusMax + MundusMaxPermaBoost) * (1 + ((MundusOverflow - 1) / 2));

                        Player.AddBuff(ModContent.BuffType<Mysticality3>(), 60 * 60, true);
                    }

                    if (mysticaPotion)
                        break;
                }

                if (!mysticaPotion)
                {
                    foreach (Item item in Player.inventory)
                    {
                        if (item.type == ModContent.ItemType<GreaterMysticaPotion>())
                        {
                            mysticaPotion = true;
                            item.stack -= 1;

                            SoundEngine.PlaySound(SoundID.Item3, Player.position);

                            if (Lux < LuxMax + LuxMaxPermaBoost)
                                Lux = LuxMax + LuxMaxPermaBoost;

                            if (Vis < VisMax + VisMaxPermaBoost)
                                Vis = VisMax + VisMaxPermaBoost;

                            if (Mundus < MundusMax + MundusMaxPermaBoost)
                                Mundus = MundusMax + MundusMaxPermaBoost;

                            Player.AddBuff(ModContent.BuffType<Mysticality2>(), 60 * 60, true);
                        }

                        if (mysticaPotion)
                            break;
                    }
                }

                if (!mysticaPotion)
                {
                    foreach (Item item in Player.inventory)
                    {
                        if (item.type == ModContent.ItemType<MysticaPotion>())
                        {
                            mysticaPotion = true;
                            item.stack -= 1;
                            SoundEngine.PlaySound(SoundID.Item3, Player.position);
                            Lux = LuxMax + LuxMaxPermaBoost;
                            Vis = VisMax + VisMaxPermaBoost;
                            Mundus = MundusMax + MundusMaxPermaBoost;
                            Player.AddBuff(ModContent.BuffType<Mysticality>(), 60 * 60, true);
                        }

                        if (mysticaPotion)
                            break;
                    }
                }
            }
        }
        public int SporeShard { get; set; } = 0;

        public float AntiflowDamage { get; set; } = 1f;

        #region General Mystic Vars

        public int MysticCrit { get; set; } = 4;

        public int MysticHold { get; set; } = 0;

        public int UsingMysticWeapon { get; set; } = 0;

        public float LuxUnuseRegen { get; set; } = .01f;

        public float VisUnuseRegen { get; set; } = .01f;

        public float MundusUnuseRegen { get; set; } = .01f;

        #endregion


        #region Mystica

        public float LuxDischargeRate { get; set; } = 1;
        public float LuxAbsorbRate { get; set; } = 1;
        public float LuxUseRate { get; set; } = 1f;
        public float LuxMaxPermaBoost { get; set; } = 0;
        public int CurrentLuxCost { get; set; }

        public float MundusDischargeRate { get; set; } = 1;
        public float MundusAbsorbRate { get; set; } = 1;
        public float MundusUseRate { get; set; } = 1f;
        public float MundusMaxPermaBoost { get; set; } = 0;
        public int CurrentMundusCost { get; set; } = 0;

        public float VisDischargeRate { get; set; } = 1;
        public float VisAbsorbRate { get; set; } = 1;
        public float VisUseRate { get; set; } = 1f;
        public float VisMaxPermaBoost { get; set; } = 0;
        public int CurrentVisCost { get; set; } = 0;

        public float GlobalAbsorbRate { get; set; } = .5f;
        public float GlobalPotentiaUseRate { get; set; } = 1f;
        public float GlobalOverflow { get; set; } = 1f;
        public float OverflowVelocity { get; set; } = 1f;

        #endregion


        #region Mystic Bursts

        public bool MysticBurstDisabled { get; set; } = false;

        public float MysticBurstDamage { get; set; } = 1f;

        public int MysticSwitchCool { get; set; } = 0;

        public int MysticSwitchCoolRate { get; set; } = 1;

        public bool MysticSteamBurst { get; set; } = false;

        public bool MysticShroomBurst { get; set; } = false;

        public bool MysticSandBurst { get; set; } = false;

        public bool MysticEruptionBurst { get; set; } = false;

        public bool MysticMarblite { get; set; } = false;

        public bool AndioChestplate { get; set; } = false;


        public bool Midnight { get; set; } = false;

        public int MysticSpiralBurst { get; set; } = 0;

        public int MysticSpiralDelay { get; set; } = 0;

        public int MysticSteamSpiralBurst { get; set; } = 0;

        public int MysticSteamSpiralDelay { get; set; } = 0;

        public bool MysticEruption { get; set; } = false;

        public int MysticErupting { get; set; } = 0;

        public bool Magmatic { get; set; } = false;

        public int OrionCharge { get; set; } = 0;

        public bool MysticObsidiumSwitch { get; set; } = false;

        #endregion

        #region Overflows

        public int ShroomOverflow { get; set; }

        public int Incineration { get; set; }

        public int Mysticality { get; set; }

        #endregion
        public bool ThorCharge { get; set; } = false;
        public float theta = 0;
    }
}
