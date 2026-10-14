using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Obsidium
{
    public class TitanBlast : ModNPC
    {
        float _theta = -1;
        float _dist = 0;
        float _distRate = 1;
        public override void SetDefaults()
        {
            _distRate = 1;
            _dist = 20;
            _theta = -1;
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 50;
            NPC.defense = 12;
            NPC.lifeMax = 4500;
            NPC.value = 0f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
            NPC.dontTakeDamage = true;
            NPC.scale = 1.5f;
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 300, true);
        }
        
        public override void AI()
        {
            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, ModContent.DustType<Magma>(), 0f, 0f);
            if (_theta == -1)
                _theta = NPC.ai[1] * 6.28f / 8;
            _theta += 3.14f / 80;
            _dist += _distRate;
            _distRate += .05f;
            float divisions = 6.28f / 8;
            Vector2 targetPos;
            targetPos.X = Main.npc[(int)NPC.ai[0]].Center.X + _dist * (float)Math.Cos(_theta) - NPC.width / 2;
            targetPos.Y = Main.npc[(int)NPC.ai[0]].Center.Y + _dist * (float)Math.Sin(_theta);
            NPC.position = targetPos;
            if(_dist > 1600)
            {
                NPC.active = false;
                NPC.life = 0;
            }
        }
        
    }
}
