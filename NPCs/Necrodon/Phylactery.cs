using NewDawn.Dusts;
using NewDawn.Items.Loot;
using NewDawn.Items.Placeable;
using NewDawn.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace NewDawn.NPCs.Necrodon
{
    [AutoloadBossHead]
    public class Phylactery : ModNPC
    {
        private int counter = 0;
        private static int TEMPO_CHANGE = 5858;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
            NewDawnVars.etherial.Add(NPC.type);
            // DisplayName.SetDefault("Phylactery");
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 156;
            NPC.height = 128;
            NPC.damage = 40;
            NPC.defense = 9999;
            NPC.takenDamageMultiplier = .01f;
            NPC.lifeMax = 400000;
            NPC.HitSound = SoundID.NPCHit11;
            NPC.DeathSound = SoundID.NPCDeath15;
            NPC.value = 0;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1;
            NPC.ai[2] = 0;
            Music = global::Terraria.ModLoader.MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Necrodungeon");
            counter = -8;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NewDawnWorld.downedEtheria && spawnInfo.Player.ZoneDungeon && NPC.CountNPCS(ModContent.NPCType<Phylactery>()) == 0)
                return SpawnCondition.Dungeon.Chance;
            else return 0f;
        }

        public override void AI()
        {
            NPC.ai[1]++;
            counter++;
            if ((NPC.ai[1] >= 66.6666666 && counter < TEMPO_CHANGE) || (NPC.ai[1] > 63.158 && counter >= TEMPO_CHANGE))
            {
                NoteSpawn();
                NPC.ai[1] = 1;
            }
        }

        private void NoteSpawn()
        {
            NPC.ai[0] = 10;
            switch(NPC.ai[2] % 4)
            {
                case 0:
                    SpawnRotate((int)NPC.ai[2] % 8 == 0);
                    break;
                case 1:
                    SpawnSixteenths();
                    break;
                case 2:
                    SpawnDoubleEighth();
                    break;
                default:
                    SpawnQuarters();
                    break;
            }
            NPC.ai[2]++;
        }

        private void SpawnSixteenths()
        {
            float mag = 6;
            var theta = 0;
            for (int i = 0; i < 16; i++)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2((float)Math.Cos(theta + i * Math.PI / 8) * mag, (float)Math.Sin(theta + i * Math.PI / 8) * mag), ModContent.ProjectileType<Sixteenth>(), NPC.damage, 3f, Main.myPlayer, NPC.whoAmI, i);
        }

        private void SpawnDoubleEighth()
        {
            Vector2 targetPos = Main.player[0].Center;
            Vector2 vel = targetPos - NPC.Center;
            vel.Normalize();
            vel *= 4;
            Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, vel, ModContent.ProjectileType<DoubleEighth>(), NPC.damage, 3f, Main.myPlayer, NPC.whoAmI);
        }

        private void SpawnQuarters()
        {
            float mag = 4;
            var theta = Main.rand.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < 4; i++)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2((float)Math.Cos(theta + i * Math.PI / 2) * mag, (float)Math.Sin(theta + i * Math.PI / 2) * mag), ModContent.ProjectileType<Quarter>(), NPC.damage, 3f, Main.myPlayer, NPC.whoAmI, i);
        }

        private void SpawnRotate(bool flipped)
        {
            if (flipped)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (i == 4)
                        i += 4;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(), ModContent.ProjectileType<PhylacteryRotate>(), NPC.damage, 3f, Main.myPlayer, NPC.whoAmI, i);
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

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] > 0)
                NPC.ai[0]--;
            NPC.frame.Y = frameHeight * (NPC.ai[0] > 0 ? 1 : 0);
        }
    }
}
