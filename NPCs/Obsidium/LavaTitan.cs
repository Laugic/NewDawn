using NewDawn.Items.Loot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Obsidium
{
    public class LavaTitan : ModNPC
    {
        bool _attacking = false;
        bool _attack = false;
        Vector2 _targetPos;
        public float tVel = 0f;
        public float vMax = 14f;
        public float vAccel = .2f;
        public float vMag = 0f;
        int _attackDelay = 0;
        public override void SetDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            _attackDelay = 0;
            vMag = 0f;
            vMax = 2f;
            tVel = 0f;
            _targetPos = NPC.position;
            _attack = false;
            _attacking = false;
            NPC.width = 88;
            NPC.height = 88;
            NPC.damage = 50;
            NPC.defense = 30;
            NPC.lifeMax = 4500;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            Main.npcFrameCount[NPC.type] = 14;
            NPC.value = 20000f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
        }
        /*
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.CountNPCS(ModContent.NPCType<LavaTitan>()) == 0 && NewDawnWorld.obsidiumTiles > 150 && spawnInfo.spawnTileY > WorldGen.rockLayer && NewDawnWorld.downedRagnar && Main.hardMode)
                return SpawnCondition.Cavern.Chance * .075f;
            else return 0f;
        }*/

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 300, true);
        }

        public override void AI()
        {
            //Attack
            if(_attack)
            {
                if(Main.netMode != 1)
                {
                    _attack = false;
                    for (int i = 0; i < 8; i++)
                    {
                        int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<TitanBlast>());
                        Main.npc[n].ai[0] = NPC.whoAmI;
                        Main.npc[n].ai[1] = i;
                    }
                    SoundEngine.PlaySound(SoundID.Item122, NPC.position);
                }
            }
            if(!_attacking)
                _attackDelay++;
            if(_attackDelay > 60 * 4)
            {
                _attackDelay = 0;
                _attacking = true;
            }

            //Movement
            _targetPos = Main.player[NPC.target].Center;
            if(!_attacking)
            {
                float dist = Vector2.Distance(_targetPos, NPC.Center);
                tVel = dist / 15;
                if (vMag < vMax && vMag < tVel)
                {
                    vMag += vAccel;
                    vMag = tVel;
                }

                if (vMag > tVel)
                {
                    vMag = tVel;
                }

                if (vMag > vMax)
                {
                    vMag = vMax;
                }

                if (dist != 0)
                {
                    NPC.velocity = NPC.DirectionTo(_targetPos) * vMag;
                }
            }
            else
            {
                NPC.velocity.X *= .95f;
                NPC.velocity.Y *= .95f;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if(!_attacking)
            {
                if (NPC.frameCounter > 30)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0;
                }
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                if (NPC.frame.Y < frameHeight * 4)
                {
                    NPC.frame.Y = frameHeight * 4;
                }
                if (NPC.frameCounter > 4)
                {
                    if (NPC.frame.Y > frameHeight * 10 && NPC.frame.Y < frameHeight * 12)
                        _attack = true;
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0;
                }
                if (NPC.frame.Y > frameHeight * 13)
                {
                    NPC.frame.Y = 0;
                    _attacking = false;
                }
            }
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<MagmaticCluster>());
        }
    }
}
