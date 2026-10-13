using Microsoft.Xna.Framework;
using NewDawn.Items.Materials;
using NewDawn.NPCs.Obsidium;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Ameldera
{
    class AgamondaHead : Agamonda
    {
        int delay = 0;
        public override string Texture { get { return "NewDawn/NPCs/Ameldera/AgamondaHead"; } }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.width = 48;
            NPC.height = 48;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.buffImmune[24] = true;
        }

        public override void Init()
        {
            base.Init();
            head = true;
        }


        public override void AI()
        {
            Player player = Main.player[NPC.target];
            delay++;
            if (delay > 60)
            {
                delay = 0;
                Vector2 newVel = NPC.velocity;
                newVel.Normalize();
                newVel *= 12;
                if (Main.netMode != 1)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, newVel, ModContent.ProjectileType<ElderShardRock>(), NPC.damage / 4, 5);
            }

            base.AI();
        }


        /*
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NewDawnWorld.obsidiumTiles > 150 && NewDawnWorld.downedRagnar && Main.hardMode)
                return SpawnCondition.Cavern.Chance * 0.25f;
            else return 0f;
        }*/

        int _attackCounter = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(_attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            _attackCounter = reader.ReadInt32();
        }

        public override void CustomBehavior()
        {

        }
    }

    class AgamondaBody : Agamonda
    {
        int delay = 0;
        public override string Texture { get { return "NewDawn/NPCs/Ameldera/AgamondaBody"; } }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.width = 48;
            NPC.height = 48;
            NPC.damage = 30;
            NPC.defense = 50;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.buffImmune[24] = true;
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            delay++;
            if (delay > 2 * 60)
            {
                delay = 0;
                if (NPC.position.Y < player.position.Y - 120)
                {
                    float theta = Main.rand.NextFloat() * (float)Math.PI;
                    float mag = (Main.rand.NextFloat() * 3) + 3;
                    if (Main.netMode != 1)
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * -mag), ModContent.ProjectileType<ElderCrystalShard>(), NPC.damage / 4, 3f);

                }

            }

            base.AI();
        }
    }

    class AgamondaTail : Agamonda
    {
        public override string Texture { get { return "NewDawn/NPCs/Ameldera/AgamondaTail"; } }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.width = 48;
            NPC.height = 48;
            NPC.damage = 40;
            NPC.defense = 40;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.buffImmune[24] = true;
        }

        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    public abstract class Agamonda : Worm
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Agamonda");
        }

        public override void Init()
        {
            minLength = 4;
            maxLength = 7;
            tailType = ModContent.NPCType<AgamondaTail>();
            bodyType = ModContent.NPCType<AgamondaBody>();
            headType = ModContent.NPCType<AgamondaHead>();
            speed = 20f;
            turnSpeed = 0.25f;
            NPC.buffImmune[24] = true;
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<ElderPearl>());
        }
    }

}