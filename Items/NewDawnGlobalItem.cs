using NewDawn.Players;
using NewDawn.Items.Equipables;
using NewDawn.Items.Weapons.Mystic;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items
{
    public class NewDawnGlobalItem : GlobalItem
    {

        public NewDawnGlobalItem()
        {
            Yeet = 0;
            Mystic = false;
            MeleeDmg = -1;
        }
        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            NewDawnGlobalItem myClone = (NewDawnGlobalItem)base.Clone(item, itemClone);

            myClone.Yeet = Yeet;
            return myClone;
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.EaterOfWorldsBossBag)
            {
                itemLoot.Add(new OneFromRulesRule(1,
                    ItemDropRule.Common(ModContent.ItemType<DarkfootBoots>()),
                    ItemDropRule.Common(ItemID.ShadowOrb),
                    ItemDropRule.Common(ItemID.Vilethorn),
                    ItemDropRule.Common(ItemID.BandofStarpower),
                    ItemDropRule.Common(ItemID.BallOHurt),
                    ItemDropRule.SequentialRulesNotScalingWithLuck(1,
                        ItemDropRule.Common(ItemID.Musket),
                        ItemDropRule.Common(ItemID.MusketBall, 1, 100, 100))));
            }
            else if (item.type == ItemID.BrainOfCthulhuBossBag)
            {
                itemLoot.Add(new OneFromRulesRule(1,
                    ItemDropRule.Common(ModContent.ItemType<BloodfootBoots>()),
                    ItemDropRule.Common(ItemID.CrimsonHeart),
                    ItemDropRule.Common(ItemID.CrimsonRod),
                    ItemDropRule.Common(ItemID.PanicNecklace),
                    ItemDropRule.Common(ItemID.TheRottedFork),
                    ItemDropRule.SequentialRulesNotScalingWithLuck(1,
                        ItemDropRule.Common(ItemID.TheUndertaker),
                        ItemDropRule.Common(ItemID.MusketBall, 1, 100, 100))));
            }
            else if (item.type == ItemID.FishronBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PoseidonsTide>(), 6));
            }
        }

        //public override void RightClick(Item item, Player player)
        //{
        //    base.RightClick(item, player);
        //    if (item.type == ItemID.EaterOfWorldsBossBag)
        //    {
        //        int rand = Main.rand.Next(6);
        //        switch (rand)
        //        {
        //            case 1:
        //                player.QuickSpawnItem(new EntitySource_Parent(player.Entity), ModContent.ItemType<DarkfootBoots>(), 1);
        //                break;
        //            case 2:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.ShadowOrb, 1);
        //                break;
        //            case 3:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.Vilethorn, 1);
        //                break;
        //            case 4:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.BandofStarpower, 1);
        //                break;
        //            case 5:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.BallOHurt, 1);
        //                break;
        //            default:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.Musket, 1);
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.MusketBall, 100);
        //                break;
        //        }
        //    }
        //    if (item.type == ItemID.BrainOfCthulhuBossBag)
        //    {
        //        int rand = Main.rand.Next(6);
        //        switch (rand)
        //        {
        //            case 1:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ModContent.ItemType<BloodfootBoots>(), 1);
        //                break;
        //            case 2:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.CrimsonHeart, 1);
        //                break;
        //            case 3:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.CrimsonRod, 1);
        //                break;
        //            case 4:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.PanicNecklace, 1);
        //                break;
        //            case 5:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.TheRottedFork, 1);
        //                break;
        //            default:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.TheUndertaker, 1);
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.MusketBall, 100);
        //                break;
        //        }
        //    }
        //    if (item.type == ItemID.FishronBossBag)
        //    {
        //        if (Main.rand.Next(6) == 0)
        //            player.QuickSpawnItem(new EntitySource_Parent(player), ModContent.ItemType<PoseidonsTide>(), 1);
        //    }
        //}

        //public override void OpenVanillaBag(string context, Player player, int arg)
        //{
        //    base.OpenVanillaBag(context, player, arg);
        //    if (arg == ItemID.EaterOfWorldsBossBag)
        //    {
        //        int rand = Main.rand.Next(6);
        //        switch (rand)
        //        {
        //            case 1:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ModContent.ItemType<DarkfootBoots>(), 1);
        //                break;
        //            case 2:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.ShadowOrb, 1);
        //                break;
        //            case 3:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.Vilethorn, 1);
        //                break;
        //            case 4:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.BandofStarpower, 1);
        //                break;
        //            case 5:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.BallOHurt, 1);
        //                break;
        //            default:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.Musket, 1);
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.MusketBall, 100);
        //                break;
        //        }
        //    }
        //    if (arg == ItemID.BrainOfCthulhuBossBag)
        //    {
        //        int rand = Main.rand.Next(6);
        //        switch (rand)
        //        {
        //            case 1:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ModContent.ItemType<BloodfootBoots>(), 1);
        //                break;
        //            case 2:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.CrimsonHeart, 1);
        //                break;
        //            case 3:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.CrimsonRod, 1);
        //                break;
        //            case 4:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.PanicNecklace, 1);
        //                break;
        //            case 5:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.TheRottedFork, 1);
        //                break;
        //            default:
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.TheUndertaker, 1);
        //                player.QuickSpawnItem(new EntitySource_Parent(player), ItemID.MusketBall, 100);
        //                break;
        //        }
        //    }
        //    if (arg == ItemID.FishronBossBag)
        //    {
        //        if (Main.rand.Next(6) == 0)
        //            player.QuickSpawnItem(new EntitySource_Parent(player), ModContent.ItemType<PoseidonsTide>(), 1);
        //    }
        //}

        //public override int ChoosePrefix(Item item, UnifiedRandom rand)
        //{
        //    if (item.accessory && item.stack == 1 && rand.NextBool(80))
        //    {
        //        byte pref;

        //        switch (rand.Next(4))
        //        {
        //            /*case 0:
        //                pref = ModContent.PrefixType<Swift>();
        //                break;
        //            case 1:
        //                pref = ModContent.PrefixType<Speedy>();
        //                break;
        //            case 2:
        //                pref = ModContent.PrefixType<Zippy>();
        //                break;*/
        //            default:
        //                pref = ModContent.PrefixType<Yeeting>();
        //                break;
        //        }

        //        return pref;
        //    }
        //    if (item.CountsAsClass(DamageClass.Ranged) && Main.rand.Next(30) == 0)
        //    {
        //        return ModContent.PrefixType<CarefulPrefix>();
        //    }
        //    if (item.CountsAsClass(DamageClass.Melee) && Main.rand.Next(30) == 0)
        //    {
        //        if (Main.rand.Next(2) == 0)
        //            return ModContent.PrefixType<ColossalPrefix>();
        //        return ModContent.PrefixType<HallowedPrefix>();
        //    }
        //    if (item.CountsAsClass(DamageClass.Magic) && Main.rand.Next(30) == 0)
        //    {
        //        return ModContent.PrefixType<KnowledgeablePrefix>();
        //    }

        //    return -1;
        //}

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            int yet = item.GetGlobalItem<NewDawnGlobalItem>().Yeet;

            player.moveSpeed += .1f * yet;
            player.maxRunSpeed += player.maxRunSpeed * (.015f * yet);
        }

        //public override void PreReforge(Item item)/* tModPorter Note: Use CanReforge instead for logic determining if a reforge can happen. */
        //{
        //    Yeet = 0;
        //    return true;
        //}



        public override bool OnPickup(Item item, Player player)
        {
            if (item.type == ItemID.Heart)
            {
                if (player.GetModPlayer<AlchemistryPlayer>().BonusHealFromHearts > 0)
                    player.Heal(player.GetModPlayer<AlchemistryPlayer>().BonusHealFromHearts);
            }
            return base.OnPickup(item, player);
        }
        public override void HoldItem(Item item, Player player)
        {
            if (MeleeDmg == -1)
            {
                if (item.noMelee)
                    MeleeDmg = 0;
                else
                    MeleeDmg = 1;
            }

            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);

            // TODO Wait for Webmilio's Commons 0.1.8
            /* (NPC.CountNPCS(ModContent.NPCType<ZaWarudo>()) >= 1 && modPlayer.zImmune)
            {
                if (!modPlayer.zProjImmune && MeleeDmg == 1)
                {
                    item.noMelee = true;
                }
                else
                {
                    if (MeleeDmg == 0)
                        item.noMelee = true;

                    if (MeleeDmg == 1)
                        item.noMelee = false;
                }
            }
            else
            {
                if (MeleeDmg == 0)
                    item.noMelee = true;

                if (MeleeDmg == 1)
                    item.noMelee = false;
            }*/
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            if (!item.social && item.prefix > 0 && item.GetGlobalItem<NewDawnGlobalItem>().Yeet > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "Yeeting", "+" + (item.GetGlobalItem<NewDawnGlobalItem>().Yeet * 1.5) + "% Max Run speed and Movement speed");
                line.IsModifier = true;
                tooltips.Add(line);
            }
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write((byte)Yeet);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            Yeet = (int)reader.ReadByte();
        }

        public bool Mystic { get; set; }

        public int MeleeDmg { get; set; } = -1;

        public int Yeet { get; set; }

    }
}
