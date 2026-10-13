using System;
using NewDawn.Buffs;
using NewDawn.Items.Loot;
using NewDawn.Items.Placeable;
using NewDawn.NPCs.Slybertron;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.SteamTrain
{
    [AutoloadBossHead]
    public class SteamTrain : ModNPC
    {
        private int _despawn;
        public float MaxVelocity = 20;
        public float Acceleration = .2f;

        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            // DisplayName.SetDefault("Steam Train");
        }

        public override void SetDefaults()
        {
            MaxVelocity = 20;
            Acceleration = .2f;
            NPC.width = 252;
            NPC.height = 138;
            NPC.damage = 90;
            NPC.defense = 30;
            NPC.aiStyle = -1;
            NPC.lifeMax = 42000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            Music = global::Terraria.ModLoader.MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SteamTrain");
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 50000 + numPlayers * 6000;
            NPC.damage = 100;
        }

        public override bool CheckActive()
        {
            if (_despawn < 300)
                return false;
            return true;
        }

        private void Retarget()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;
        }

        private void DespawnCheck()
        {
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
                {
                    if (_despawn == 0)
                        _despawn++;
                }
                else
                    _despawn = 0;
            }
            if (_despawn >= 1)
            {
                _despawn++;
                NPC.noTileCollide = true;
                NPC.velocity.Y = 8f;
                if (_despawn >= 300)
                    NPC.active = false;
            }
        }

        public override void AI()
        {
            Retarget();
            DespawnCheck();
            if(_despawn == 0)
                Move();
        }

        private void Move()
        {
            if(NPC.Center.X + NPC.width / 2 < Main.player[NPC.target].Center.X)
            {
                if (NPC.velocity.X < 0)
                    NPC.velocity *= .9f;
                NPC.velocity.X = Math.Min(NPC.velocity.X + Acceleration, MaxVelocity);
                NPC.spriteDirection = -1;
            }
            if (NPC.Center.X - NPC.width / 2 > Main.player[NPC.target].Center.X)
            {
                if (NPC.velocity.X > 0)
                    NPC.velocity *= .9f;
                NPC.velocity.X = Math.Max(NPC.velocity.X - Acceleration, -MaxVelocity);
                NPC.spriteDirection = 1;
            }
            
            if (NPC.Center.Y + NPC.height / 2 < Main.player[NPC.target].Center.Y)
            {
                if (NPC.velocity.Y < 0)
                    NPC.velocity *= .9f;
                NPC.velocity.Y = Math.Min(NPC.velocity.Y + Acceleration / 5f, MaxVelocity / 5f);
            }
            if (NPC.Center.Y - NPC.height / 2 > Main.player[NPC.target].Center.Y)
            {
                if (NPC.velocity.Y > 0)
                    NPC.velocity *= .9f;
                NPC.velocity.Y = Math.Max(NPC.velocity.Y - Acceleration / 5f, -MaxVelocity / 5f);
            }
        }

        /*
        public override void OnHitPlayer(Player target, int dmgDealt, bool crit)
        {
            if (Main.expertMode)
                target.AddBuff(ModContent.BuffType<Steamy>(), 90, true);
        }*/

        public override void OnKill()
        {
            if (NewDawnWorld.downedEtheria)
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<EtherialTank>(), 1);

            if (!Main.expertMode)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SteamBar>(), Main.rand.Next(15, 30));
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SoulOfWrought>(), Main.rand.Next(20, 40));
            }

            if (Main.rand.Next(10) == 0)
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SteamTrainTrophy>(), 1);

            NewDawnWorld.downedSteamTrain = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 0f;
            return null;
        }
    }
}
