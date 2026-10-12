using System.Collections.Generic;
using NewDawn.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.SoulStones
{
    public abstract class OldSoulStone : NewDawnItem
    {

        //Throwing
        string _ks1 = "[c/2B9DE9:+10% Throwing Damage]"; string _ks2 = "[c/2B9DE9:Greatly increases jump height]"; string _ks3 = "[c/2B9DE9:Attacks Inflict 'Slimed']"; string _ks4 = "[c/2B9DE9:+20% Throwing velocity]";
        //Mystic
        string _eoC1 = "[c/B03A2E:Friendly Eyes spawn to protect you when you take damage.]"; string _eoC2 = "[c/B03A2E:Greatly increases movement speed]"; string _eoC3 = "[c/B03A2E:Hunter Potion Effect]"; string _eoC4 = "[c/B03A2E:+5% Destruction and Conjuration Damage, +20% Illusion Duration]";
        //Magic
        string _eoWboC1 = "[c/884EA0:Causes 'Blood Rage' when struck]"; string _eoWboC2 = "[c/884EA0:+4 Defense, +20 Max Life]"; string _eoWboC3 = "[c/884EA0:+20 Max Mana, Increased Life Regeneration]"; string _eoWboC4 = "[c/884EA0:Increased Mana Regeneration]";
        //Summon
        string _qb1 = "[c/F39C12:Attacks inflict Poison]"; string _qb2 = "[c/F39C12:Increased Life Regeneration]"; string _qb3 = "[c/F39C12:+1 Max Minion]"; string _qb4 = "[c/F39C12:+10% Minion damage]";
        //Range
        string _sk1 = "[c/839192:+5% Damage]"; string _sk2 = "[c/839192:5 Defense]"; string _sk3 = "[c/839192:Increased Run Speed]"; string _sk4 = "[c/839192:+10% Ranged Critical strike chance]";
        //Melee
        string _woF1 = "[c/AC395A:+5% Damage]"; string _woF2 = "[c/AC395A:Increased Life Regeneration]"; string _woF3 = "[c/AC395A:+40 Max Mana]"; string _woF4 = "[c/AC395A:Attacks inflict 'On Fire!']";
       
        string _tw1 = "[c/2BD34D:-10% Mana Cost, +20 Mana, +5% Magic damage, +5% Mystic Damage]"; string _tw2 = "[c/2BD34D:Increased Wing flight time if worn under Wings]"; 

        string _dst1 = "[c/DF0A0A:+12% Ranged Critical Strike Chance]"; string _dst2 = "[c/DF0A0A:Increased Movement Speed]"; 

        string _sp1 = "[c/AAAAAA:+5% Melee damage and speed, attacks inflict 'Cursed Inferno']"; string _sp2 = "[c/AAAAAA:+6 Defense]"; 

        string _pt1 = "[c/27AE60:+1 Max Minion, Increased Mana regeneration]"; string _pt2 = "[c/27AE60:Release Spores when struck.]"; 

        string _gl1 = "[c/AF740E:+10% Critical strike chance]"; string _gl2 = "[c/AF740E:Increased Life Regeneration]"; 

        string _df1 = "[c/04F6B2:+8% Ranged Damage and Melee Speed, Attacks inflict 'Venom']"; string _df2 = "[c/04F6B2:Greatly increased Wing Acceleration]"; string _df3 = "[c/04F6B2:+10% Mystic Damage and Duration]";

        string _lc2 = "[c/1D9CA7:+8% Magic, Minion, and Mystic Damage]"; string _lc1 = "[c/1D9CA7:+8% Melee, Ranged, and Throwing Damage]";

        string _ml1 = "[c/3BE5D7:+10% Damage, +12 Defense]";

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Absorbs the souls of powerful fallen creatures\nAn otherworldly entitiy seems to have sealed some of its power...");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
            Item.accessory = true;
            //item.lifeRegen = 19;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer mPlayer = NewDawnPlayer.Get(player);
            int Class = mPlayer.Class;

            


            if (NPC.downedSlimeKing)
            {
                if (NewDawnVars.slimeThrow.Contains(Class))
                    player.GetDamage(DamageClass.Throwing) += 0.1f;

                if (NewDawnVars.slimeJump.Contains(Class) && mPlayer.SoulStoneMovement)
                    player.jumpSpeedBoost += 5.0f;

                if (NewDawnVars.slimeMinion.Contains(Class))
                    mPlayer.Slimey = true;

                if (NewDawnVars.slimeVelocity.Contains(Class))
                    player.ThrownVelocity += .2f;
            }
            if (NPC.downedBoss1)
            {
                if (NewDawnVars.boss1Thorns.Contains(Class))
                    mPlayer.Eyes = true;

                if (NewDawnVars.boss1Speed.Contains(Class))
                    player.moveSpeed += 1.0f;

                if (NewDawnVars.boss1Detect.Contains(Class) && mPlayer.SoulStoneVisuals)
                    player.detectCreature = true;

                if (NewDawnVars.boss1Damage.Contains(Class))
                {
                    mPlayer.DestructionDamage += 0.05f;
                    mPlayer.ConjurationDamage += 0.05f;
                    mPlayer.MysticDuration += 0.2f;
                }
            }
            if (NPC.downedBoss2)
            {
                if (NewDawnVars.boss2Rage.Contains(Class))
                    mPlayer.BloodRage = true;

                if (NewDawnVars.boss2Defence.Contains(Class))
                {
                    player.statDefense += 4;
                    player.statLifeMax2 += 20;
                }
                if (NewDawnVars.boss2Regen.Contains(Class))
                {
                    player.lifeRegen += 1;
                    player.statManaMax2 += 20;
                }
                if (NewDawnVars.boss2RBonus.Contains(Class))
                    player.manaRegenBonus += 15;

            }
            if (NPC.downedQueenBee)
            {
                if (NewDawnVars.beeTrue.Contains(Class))
                    mPlayer.QueenBee = true;

                if (NewDawnVars.beeRegen.Contains(Class))
                    player.lifeRegen += 2;

                if (NewDawnVars.beeMinions.Contains(Class))
                    player.maxMinions += 1;

                if (NewDawnVars.beeMDamage.Contains(Class))
                    player.GetDamage(DamageClass.Summon) += .1f;

            }
            if (NPC.downedBoss3)
            {
                if (NewDawnVars.boss3Damage.Contains(Class))
                {
                    player.GetDamage(DamageClass.Generic) += 0.05f;
                }
                if (NewDawnVars.boss3Defense.Contains(Class))
                    player.statDefense += 5;

                if (NewDawnVars.boss3Speed.Contains(Class))
                {
                    player.maxRunSpeed += .5f;
                    player.moveSpeed += .5f;
                }
                if (NewDawnVars.boss3Crit.Contains(Class))
                    player.GetCritChance(DamageClass.Ranged) += 10;

            }
            if (Main.hardMode)
            {
                if (NewDawnVars.hardDamage.Contains(Class))
                {
                    player.GetDamage(DamageClass.Generic) += 0.05f;
                }
                if (NewDawnVars.hardRegen.Contains(Class))
                    player.lifeRegen += 2;

                if (NewDawnVars.hardMana.Contains(Class))
                    player.statManaMax2 += 40;

                if (NewDawnVars.hardObsid.Contains(Class))
                    mPlayer.Obsidium = true;

            }
            if (NPC.downedMechBoss1)
            {
                if (NewDawnVars.mech1Crit.Contains(Class))
                    player.GetCritChance(DamageClass.Ranged) += 12;

                if (NewDawnVars.mech1Speed.Contains(Class))
                    player.moveSpeed += .4f;

            }
            if (NPC.downedMechBoss2)
            {
                if (NewDawnVars.mech2Magic.Contains(Class))
                {
                    player.GetDamage(DamageClass.Magic) += 0.05f;
                    player.statManaMax2 += 20;
                    player.manaCost -= .1f;
                    mPlayer.MysticDamage += .05f;
                }
                if (NewDawnVars.mech2Jump.Contains(Class) && mPlayer.SoulStoneMovement)
                    player.wingTimeMax += 120;

            }
            if (NPC.downedMechBoss3)
            {
                if (NewDawnVars.mech3Damage.Contains(Class))
                {
                    mPlayer.SkeletonPrime = true;
                    player.GetDamage(DamageClass.Melee) += 0.05f;
                    player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
                }
                if (NewDawnVars.mech3Defense.Contains(Class))
                    player.statDefense += 6;

            }
            if (NPC.downedPlantBoss)
            {
                if (NewDawnVars.plantBonus.Contains(Class))
                {
                    player.maxMinions++;
                    player.manaRegenBonus += 20;
                }
                if (NewDawnVars.plantThorns.Contains(Class))
                    mPlayer.Spores = true;

            }
            if (NPC.downedGolemBoss)
            {
                if (NewDawnVars.golemCrit.Contains(Class))
                {
                    player.GetCritChance(DamageClass.Throwing) += 10;
                    player.GetCritChance(DamageClass.Ranged) += 10;
                    player.GetCritChance(DamageClass.Magic) += 10;
                    player.GetCritChance(DamageClass.Generic) += 10;
                }
                if (NewDawnVars.golemRegen.Contains(Class))
                    player.lifeRegen += 2;

            }
            if (NPC.downedFishron)
            {
                if (NewDawnVars.fishDouche.Contains(Class))
                {
                    mPlayer.Doucheron = true;
                    player.GetDamage(DamageClass.Ranged) += 0.08f;
                    player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
                }
                if (NewDawnVars.fishSpeed.Contains(Class) && mPlayer.SoulStoneMovement)
                    player.jumpSpeedBoost += 4.0f;

                if (NewDawnVars.fishMDamage.Contains(Class))
                {
                    mPlayer.DestructionDamage += 0.05f;
                    mPlayer.ConjurationDamage += 0.05f;
                    mPlayer.MysticDuration += 0.1f;
                }
            }
            if (NPC.downedAncientCultist)
            {
                if (NewDawnVars.cultistDamage1.Contains(Class))
                {
                    player.GetDamage(DamageClass.Ranged) += 0.08f;
                    player.GetDamage(DamageClass.Melee) += 0.08f;
                    player.GetDamage(DamageClass.Throwing) += .08f;
                }
                if (NewDawnVars.cultistDamage2.Contains(Class))
                {
                    player.GetDamage(DamageClass.Magic) += 0.08f;
                    player.GetDamage(DamageClass.Summon) += 0.08f;
                    mPlayer.MysticDamage += .08f;
                }
            }
            if (NPC.downedMoonlord)
            {
                player.GetDamage(DamageClass.Generic) += 0.10f;
                player.statDefense += 12;
            }
        }



        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player ttPlayer = Main.player[Main.myPlayer];
            int Class = NewDawnPlayer.Get(ttPlayer).Class;

            //Tooltips
            /*if (KS > 0)
            {
                TooltipLine lineKSTT = new TooltipLine(mod, "", KSTT);
                tooltips.Add(lineKSTT);
            }*/

            //Class
            if      (Class == (int)NewDawnVars.ClassType.Undefined      ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Your Soul is not Bound. \nCraft one of the class Stones to bind it!"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Warrior        ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Warrior"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Tank           ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Tank"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Paladin        ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Paladin"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Warlock        ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Warlock"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Wizard         ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Wizard"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Mage           ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Mage"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Sharpshooter   ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Sharpshooter"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Rogue          ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Rogue"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Hunter         ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Hunter"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Necromancer    ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Necromancer"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Sorcerer       ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Sorcerer"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Shaman         ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Shaman"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Assasin        ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Assassin"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Ninja          ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Ninja"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Thief          ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Thief"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Destructionist ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Destructionist"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Illusionist    ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Illusionist"); tooltips.Add(lineClass); }
            else if (Class == (int)NewDawnVars.ClassType.Conjurer       ) { TooltipLine lineClass = new TooltipLine(Mod, "", "Conjurer"); tooltips.Add(lineClass); }
            //Tooltips
            if (NPC.downedSlimeKing)
            {
                if (NewDawnVars.slimeThrow.Contains(Class))
                {
                    TooltipLine lineKs1 = new TooltipLine(Mod, "", _ks1);
                    tooltips.Add(lineKs1);
                }

                if (NewDawnVars.slimeJump.Contains(Class))
                {
                    TooltipLine lineKs2 = new TooltipLine(Mod, "", _ks2);
                    tooltips.Add(lineKs2);
                }

                if (NewDawnVars.slimeMinion.Contains(Class))
                {
                    TooltipLine lineKs3 = new TooltipLine(Mod, "", _ks3);
                    tooltips.Add(lineKs3);
                }

                if (NewDawnVars.slimeVelocity.Contains(Class))
                {
                    TooltipLine lineKs4 = new TooltipLine(Mod, "", _ks4);
                    tooltips.Add(lineKs4);
                }
            }
            if (NPC.downedBoss1)
            {
                if (NewDawnVars.boss1Thorns.Contains(Class))
                {
                    TooltipLine lineEoC1 = new TooltipLine(Mod, "", _eoC1);
                    tooltips.Add(lineEoC1);
                }
                if (NewDawnVars.boss1Speed.Contains(Class))
                {
                    TooltipLine lineEoC2 = new TooltipLine(Mod, "", _eoC2);
                    tooltips.Add(lineEoC2);
                }
                if (NewDawnVars.boss1Detect.Contains(Class))
                {
                    TooltipLine lineEoC3 = new TooltipLine(Mod, "", _eoC3);
                    tooltips.Add(lineEoC3);
                }
                if (NewDawnVars.boss1Damage.Contains(Class))
                {
                    TooltipLine lineEoC4 = new TooltipLine(Mod, "", _eoC4);
                    tooltips.Add(lineEoC4);
                }
            }
            if (NPC.downedBoss2)
            {
                if (NewDawnVars.boss2Rage.Contains(Class))
                {
                    TooltipLine lineEoWboC1 = new TooltipLine(Mod, "", _eoWboC1);
                    tooltips.Add(lineEoWboC1);
                }
                if (NewDawnVars.boss2Defence.Contains(Class))
                {
                    TooltipLine lineEoWboC2 = new TooltipLine(Mod, "", _eoWboC2);
                    tooltips.Add(lineEoWboC2);
                }
                if (NewDawnVars.boss2Regen.Contains(Class))
                {
                    TooltipLine lineEoWboC3 = new TooltipLine(Mod, "", _eoWboC3);
                    tooltips.Add(lineEoWboC3);
                }
                if (NewDawnVars.boss2RBonus.Contains(Class))
                {
                    TooltipLine lineEoWboC4 = new TooltipLine(Mod, "", _eoWboC4);
                    tooltips.Add(lineEoWboC4);
                }
            }
            if (NPC.downedQueenBee)
            {
                if (NewDawnVars.beeTrue.Contains(Class))
                {
                    TooltipLine lineQb1 = new TooltipLine(Mod, "", _qb1);
                    tooltips.Add(lineQb1);
                }
                if (NewDawnVars.beeRegen.Contains(Class))
                {
                    TooltipLine lineQb2 = new TooltipLine(Mod, "", _qb2);
                    tooltips.Add(lineQb2);
                }
                if (NewDawnVars.beeMinions.Contains(Class))
                {
                    TooltipLine lineQb3 = new TooltipLine(Mod, "", _qb3);
                    tooltips.Add(lineQb3);
                }
                if (NewDawnVars.beeMDamage.Contains(Class))
                {
                    TooltipLine lineQb4 = new TooltipLine(Mod, "", _qb4);
                    tooltips.Add(lineQb4);
                }
            }
            if (NPC.downedBoss3)
            {
                if (NewDawnVars.boss3Damage.Contains(Class))
                {
                    TooltipLine lineSk1 = new TooltipLine(Mod, "", _sk1);
                    tooltips.Add(lineSk1);
                }
                if (NewDawnVars.boss3Defense.Contains(Class))
                {
                    TooltipLine lineSk2 = new TooltipLine(Mod, "", _sk2);
                    tooltips.Add(lineSk2);
                }
                if (NewDawnVars.boss3Speed.Contains(Class))
                {
                    TooltipLine lineSk3 = new TooltipLine(Mod, "", _sk3);
                    tooltips.Add(lineSk3);
                }
                if (NewDawnVars.boss3Crit.Contains(Class))
                {
                    TooltipLine lineSk4 = new TooltipLine(Mod, "", _sk4);
                    tooltips.Add(lineSk4);
                }

            }
            if (Main.hardMode)
            {
                if (NewDawnVars.hardDamage.Contains(Class))
                {
                    TooltipLine lineWoF1 = new TooltipLine(Mod, "", _woF1);
                    tooltips.Add(lineWoF1);
                }
                if (NewDawnVars.hardRegen.Contains(Class))
                {
                    TooltipLine lineWoF2 = new TooltipLine(Mod, "", _woF2);
                    tooltips.Add(lineWoF2);
                }
                if (NewDawnVars.hardMana.Contains(Class))
                {
                    TooltipLine lineWoF3 = new TooltipLine(Mod, "", _woF3);
                    tooltips.Add(lineWoF3);
                }
                if (NewDawnVars.hardObsid.Contains(Class))
                {
                    TooltipLine lineWoF4 = new TooltipLine(Mod, "", _woF4);
                    tooltips.Add(lineWoF4);
                }
            }
            if (NPC.downedMechBoss1)
            {
                if (NewDawnVars.mech1Crit.Contains(Class))
                
                    {
                        TooltipLine lineDst1 = new TooltipLine(Mod, "", _dst1);
                        tooltips.Add(lineDst1);
                    }
                if (NewDawnVars.mech1Speed.Contains(Class))
                    {
                        TooltipLine lineDst2 = new TooltipLine(Mod, "", _dst2);
                        tooltips.Add(lineDst2);
                    }
            }
            if (NPC.downedMechBoss2)
            {
                if (NewDawnVars.mech2Magic.Contains(Class))
                    {
                        TooltipLine lineTw1 = new TooltipLine(Mod, "", _tw1);
                        tooltips.Add(lineTw1);
                    }
                if (NewDawnVars.mech2Jump.Contains(Class))
                    {
                        TooltipLine lineTw2 = new TooltipLine(Mod, "", _tw2);
                        tooltips.Add(lineTw2);
                    }
            }
            if (NPC.downedMechBoss3)
            {
                if (NewDawnVars.mech3Damage.Contains(Class))
                    {
                        TooltipLine lineSp1 = new TooltipLine(Mod, "", _sp1);
                        tooltips.Add(lineSp1);
                    }
                if (NewDawnVars.mech3Defense.Contains(Class))
                    {
                        TooltipLine lineSp2 = new TooltipLine(Mod, "", _sp2);
                        tooltips.Add(lineSp2);
                    }
            }
            if (NPC.downedPlantBoss)
            {
                if (NewDawnVars.plantBonus.Contains(Class))
                {
                    TooltipLine linePt1 = new TooltipLine(Mod, "", _pt1);
                    tooltips.Add(linePt1);
                }
                if (NewDawnVars.plantThorns.Contains(Class))
                {
                    TooltipLine linePt2 = new TooltipLine(Mod, "", _pt2);
                    tooltips.Add(linePt2);
                }
            }
            if (NPC.downedGolemBoss)
            {
                if (NewDawnVars.golemCrit.Contains(Class))
                {
                    TooltipLine lineGl1 = new TooltipLine(Mod, "", _gl1);
                    tooltips.Add(lineGl1);
                }
                if (NewDawnVars.golemRegen.Contains(Class))
                {
                    TooltipLine lineGl2 = new TooltipLine(Mod, "", _gl2);
                    tooltips.Add(lineGl2);
                }
            }
            if (NPC.downedFishron)
            {
                if (NewDawnVars.fishDouche.Contains(Class))
                {
                    TooltipLine lineDf1 = new TooltipLine(Mod, "", _df1);
                    tooltips.Add(lineDf1);
                }
                if (NewDawnVars.fishSpeed.Contains(Class))
                {
                    TooltipLine lineDf2 = new TooltipLine(Mod, "", _df2);
                    tooltips.Add(lineDf2);
                }
                if (NewDawnVars.fishMDamage.Contains(Class))
                {
                    TooltipLine lineDf3 = new TooltipLine(Mod, "", _df3);
                    tooltips.Add(lineDf3);
                }
            }
            if (NPC.downedAncientCultist)
            {
                if (NewDawnVars.cultistDamage1.Contains(Class))
                {
                    TooltipLine lineLc1 = new TooltipLine(Mod, "", _lc1);
                    tooltips.Add(lineLc1);
                }
                if (NewDawnVars.cultistDamage2.Contains(Class))
                {
                    TooltipLine lineLc2 = new TooltipLine(Mod, "", _lc2);
                    tooltips.Add(lineLc2);
                }
            }
            if (NPC.downedMoonlord && Class > 0)
            {
                TooltipLine lineMl1 = new TooltipLine(Mod, "", _ml1);
                tooltips.Add(lineMl1);
            }
        }
        
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 10);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
    
}