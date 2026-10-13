// using NewDawn.Dusts;
// using NewDawn.Items.Consumables;
// using NewDawn.Items.Consumables.Potions;
// using NewDawn.Items.Equipables;
// using NewDawn.Items.Materials;
// using NewDawn.Items.Misc;
// using NewDawn.Items.Placeable;
// using NewDawn.Projectiles.NPCProj;
// using System.Collections.Generic;
// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;

// namespace NewDawn.NPCs
// {
//     [AutoloadHead]
//     public class TheLastMoldyrian : ModNPC
//     {
//         int _chatIndex = 0;
//         int _mysticChatIndex = 0;
//         public override bool IsLoadingEnabled(Mod mod)
//         {
//             name = "TheLastMoldyrian";
//             return Mod.Properties/* tModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
//         }

//         public override void SetStaticDefaults()
//         {
//             // DisplayName.SetDefault("The Last Moldyrian");
//             Main.npcFrameCount[NPC.type] = 25;
//             NPCID.Sets.ExtraFramesCount[NPC.type] = 5;
//             NPCID.Sets.AttackFrameCount[NPC.type] = 4;
//             NPCID.Sets.DangerDetectRange[NPC.type] = 1000;
//             NPCID.Sets.AttackType[NPC.type] = 0;
//             NPCID.Sets.AttackTime[NPC.type] = 30;
//             NPCID.Sets.AttackAverageChance[NPC.type] = 30;
//         }

//         public override void SetDefaults()
//         {
//             _chatIndex = 0;
//             _mysticChatIndex = 0;
//             NPC.townNPC = true;
//             NPC.friendly = true;
//             NPC.width = 18;
//             NPC.height = 40;
//             NPC.aiStyle = 7;
//             NPC.damage = 50;
//             NPC.defense = 15;
//             NPC.lifeMax = 250;
//             NPC.HitSound = SoundID.NPCHit1;
//             NPC.DeathSound = SoundID.NPCDeath1;
//             NPC.knockBackResist = 0.5f;
//             AnimationType = NPCID.Guide;
//         }

//         public override void HitEffect(NPC.HitInfo hit)
//         {
//             int num = NPC.life > 0 ? 1 : 5;
//             for (int k = 0; k < num; k++)
//             {
//                 Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<White>());
//             }
//         }

//         public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
//         {
//             return (numTownNpCs > 3);
//         }
//         public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
//         {
//             return "";
//         }
        
//         public override string GetChat()
//         {
//             int steampunker = NPC.FindFirstNPC(NPCID.Steampunker);


//             //Mystic Dialogue
//             if (NewDawnPlayer.Get().MysticDamage > 1 && Main.rand.Next(2) == 0)
//             {
//                 _mysticChatIndex++;
//                 switch (_chatIndex % 3)
//                 {
//                     default:
//                         return "Are you pursuing the way of the Mystic, " + Main.LocalPlayer.name + "? I have not seen another soul destined down that path for years.";
//                 }
//             }

//             _chatIndex++;
//             //General Dialogue
//             switch (_chatIndex % 8)
//             {
//                 case 1:
//                     return "Have faith, young one. Good times will come again. Someday.";
//                 case 2:
//                     return "Do you have a moment to talk about whatever it is that you believe in?";
//                 case 3:
//                     return "Blessings upon you!";
//                 case 4:
//                     return "If you happen to spot a Vetruvian, best keep to yourself. Those ones can't help but play dirty. Probably had something to do with... Nevermind.";
//                 case 5:
//                     return "Have you been praying lately? \n\n...to whom? Why, to anyone of course.";
//                 case 6:
//                     return "There will always be things in the universe we can't explain, but that doesn't mean they don't have an explanation.";
//                 default:
//                     return "Good day and gods bless, " + Main.LocalPlayer.name + ".";
//             }
//             return "Good day and gods bless, " + Main.LocalPlayer.name + ".";
//         }

//         public override void SetChatButtons(ref string button, ref string button2) => button = Lang.inter[28].Value;

//         public override void OnChatButtonClicked(bool firstButton, ref string shopName)
//         {
//             if (firstButton)
//                 shop = true;
//         }

//         public override void ModifyActiveShop(string shopName, Item[] items)
//         {
//             shop.item[nextSlot].SetDefaults(ModContent.ItemType<AncientScroll>());
//             shop.item[nextSlot].value = Item.buyPrice(silver: 1);
//             nextSlot++;

//             shop.item[nextSlot].SetDefaults(ModContent.ItemType<MysticaPotion>());
//             shop.item[nextSlot].value = Item.buyPrice(silver: 5);
//             nextSlot++;

//             shop.item[nextSlot].SetDefaults(ItemID.MushroomGrassSeeds);
//             shop.item[nextSlot].value = Item.buyPrice(silver: 3);
//             nextSlot++;

//             shop.item[nextSlot].SetDefaults(ModContent.ItemType<FaeryInABottle>());
//             shop.item[nextSlot].value = Item.buyPrice(gold: 3);
//             nextSlot++;

//             if(NewDawnWorld.downedDuneSharkron)
//                 shop.item[nextSlot].SetDefaults(ModContent.ItemType<SolidPotentia>());
//             shop.item[nextSlot].value = Item.buyPrice(gold: 5);
//             nextSlot++;

//         }

//         public override void TownNPCAttackStrength(ref int damage, ref float knockback)
//         {
//             damage = 50;
//             knockback = 4f;
//         }

//         public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
//         {
//             cooldown = 12;
//             randExtraCooldown = 30;
//         }

//         public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
//         {
//             projType = ModContent.ProjectileType<ConductorProjectile>();
//             attackDelay = 1;
//         }

//         public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
//         {
//             multiplier = 12f;
//             randomOffset = 1f;
//         }
//     }
// }