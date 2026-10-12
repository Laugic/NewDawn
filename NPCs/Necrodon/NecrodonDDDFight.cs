using NewDawn.Dusts;
using NewDawn.Items.Loot;
using NewDawn.Items.Placeable;
using NewDawn.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Necrodon
{
    public class NecrodonDDDFight : ModNPC
    {
        private int counter = 0;
        private float startTime = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Necrodon");
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 28;
            NPC.damage = 999;
            NPC.lifeMax = 10000000;
            NPC.HitSound = SoundID.NPCHit11;
            NPC.DeathSound = SoundID.NPCDeath15;
            NPC.value = 0;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.dontTakeDamage = true;
            NPC.knockBackResist = -1;
            NPC.ai[0] = 0;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            counter = 0;
            startTime = (float)Main.upTimer;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DDD");
            bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<DuneSharkronTreasureBag>();
        }

        public override void AI()
        {
            counter++;
            if(counter % 60 == 0)
                Step();
        }

        private void Step()
        {
            NPC.ai[1]++;
            if((int)NPC.ai[1] % 4 == 0)
            {
                switch ((int)NPC.ai[1] % 16)
                {
                    case 0:

                        break;
                    case 1:

                        break;
                    case 2:

                        break;
                    default:
                        SpawnRotate((int)NPC.ai[1] % 8 == 0);
                        break;
                }

            }
            Main.NewText(NPC.ai[1]);
        }

        private void SpawnRotate(bool flipped)
        {
            if(flipped)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (i == 4)
                        i += 4;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(), ModContent.ProjectileType<PeriodicalRotateNotes>(), NPC.damage, 3f, Main.myPlayer, NPC.whoAmI, i);
                }
            }
            else
            {
                for (int i = 4; i < 16; i++)
                {
                    if (i == 8)
                        i += 4;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(), ModContent.ProjectileType<PeriodicalRotateNotes>(), NPC.damage, 3f, Main.myPlayer, NPC.whoAmI, i);
                }
            }
        }

        private void NoteSpawn()
        {
            NPC.ai[0] = 10;
            if (NPC.ai[2] % 2 == 0)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 4, 0, ModContent.ProjectileType<Eighth>(), (int)(NPC.damage / 4), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, -4, 0, ModContent.ProjectileType<Eighth>(), (int)(NPC.damage / 4), 3, Main.myPlayer);
            }
            else
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 4, ModContent.ProjectileType<Eighth>(), (int)(NPC.damage / 4), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, -4, ModContent.ProjectileType<Eighth>(), (int)(NPC.damage / 4), 3, Main.myPlayer);
            }
            NPC.ai[2]++;
        }

        private float GetCurTime()
        {
            return (float)Main.upTimer - startTime;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] > 0)
                NPC.ai[0]--;
            NPC.frame.Y = frameHeight * (NPC.ai[0] > 0 ? 1 : 0);
        }
    }
}
