using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Items.Loot;

namespace NewDawn.NPCs.Etheria
{
    public class TrueEtherialSpiralShot : ModNPC
    {
        float _theta = -1;
        float _dist = 0;
        float _distRate = 1;
        Vector2 _origin;
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            // DisplayName.SetDefault("True Etherial Pulse");
        }
        public override void SetDefaults()
        {
            _distRate = 1;
            _dist = 20;
            _theta = -1;
            NPC.width = 44;
            NPC.height = 44;
            NPC.damage = 70;
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
            target.AddBuff(BuffID.Frostburn, 300, true);
        }
        
        public override void AI()
        {
            if (_theta == -1)
            {
                _theta = NPC.ai[1] * 6.28f / 12;
                _origin = NPC.position;
            }
            _theta += 3.14f / 70;
            _dist += _distRate;
            _distRate += .075f;
            float divisions = 6.28f / 12;
            Vector2 targetPos;
            targetPos.X = _origin.X + _dist * (float)Math.Cos(_theta) - NPC.width / 2;
            targetPos.Y = _origin.Y + _dist * (float)Math.Sin(_theta);
            NPC.position = targetPos;
            if(_dist > 1600)
            {
                NPC.active = false;
                NPC.life = 0;
            }
            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f / 2;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            int b = 225;
            int b2 = 125;
            int b3 = 155;
            if (drawColor.R != (byte)b)
            {
                drawColor.R = (byte)b;
            }
            if (drawColor.G < (byte)b2)
            {
                drawColor.G = (byte)b2;
            }
            if (drawColor.B < (byte)b3)
            {
                drawColor.B = (byte)b3;
            }
            return drawColor;
        }
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<EtherialEssence>());
        }
    }
}
