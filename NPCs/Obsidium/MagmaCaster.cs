using NewDawn.Dusts;
using NewDawn.Items.Loot;
using NewDawn.Items.Placeable;
using NewDawn.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Obsidium
{
    public class MagmaCaster : ModNPC
    {
        int _delay = 0;
        int _reload = 0;
        public override void SetDefaults()
        {
            _reload = 0;
            _delay = 0;
            NPC.width = 80;
            NPC.height = 80;
            NPC.damage = 36;
            NPC.defense = 12;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 8;
            NPC.lavaImmune = true;
            NPC.buffImmune[24] = true;
            //npc.noGravity = true;
            //npc.noTileCollide = true;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 450;
            NPC.damage = 48;
        }
        /*
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NewDawnWorld.obsidiumTiles > 150 && spawnInfo.spawnTileY > WorldGen.rockLayer && NewDawnWorld.downedRagnar)
                return SpawnCondition.Cavern.Chance * 0.5f;
            else return 0f;
        }*/

        public override void AI()
        {
            if (Main.player[NPC.target].Center.X > NPC.Center.X)
                NPC.spriteDirection = 1;
            else
                NPC.spriteDirection = 0;
            Vector2 adj;
            adj.X = -NPC.width / 4;
            adj.Y = -NPC.height / 2;
            if(Main.rand.Next(3) == 0)
                Dust.NewDust(NPC.Center + adj, NPC.width / 2, 12, ModContent.DustType<Magma>(), 0f, 0f);
            _delay--;
            if (_delay <= 0)
            {
                _reload++;
                if(_reload < 45)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, NPC.velocity.X - 4 + Main.rand.Next(9), -Main.rand.Next(6, 9), ModContent.ProjectileType<EruptionEvil>(), (int)(NPC.damage / 2), 3, Main.myPlayer);
                    }
                }
                else
                {
                    _reload = 0;
                    _delay = 240;
                }
            }

        }
        

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int debuff = BuffID.OnFire;
            if (debuff >= 0)
            {
                target.AddBuff(debuff, 90, true);
            }
        }

        public override void OnKill()
        {
            if (NewDawnWorld.downedRagnar && Main.rand.Next(2) == 0)
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<ObsidiumChunk>());
            else
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<ObsidiumOre>(), Main.rand.Next(2, 5));
        }
        
    }
}
