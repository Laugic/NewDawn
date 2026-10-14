using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etheria
{
    public class EtherialTear : ModNPC
    {
        public static Random rnd = new Random();
        public static int ai = rnd.Next(1, 6);
        public static bool despawn = false;
        public bool bitherial = false;
        int _index = 0;
        Vector2 _targetPos;
        int _delay = 0;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 14f;
        public float vAccel = .2f;
        public float vMag = 0f;
        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            // DisplayName.SetDefault("Etherial Tear");
        }

        public override void SetDefaults()
        {
            vMag = 0f;
            vMax = 14f;
            tVel = 0f;
            _delay = 0;
            _index = 0;
            bitherial = true;
            despawn = false;
            NPC.width = 22;
            NPC.height = 22;
            NPC.damage = 70;
            NPC.defense = 50;
            NPC.lifeMax = 5000;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.scale = 2;
        }

        public override void AI()
        {
            bitherial = true;
            if (_index == 0)
            {
                if (NPC.CountNPCS(ModContent.NPCType<Etheria>()) > 0)
                    _index = Etheria.tearIndex;
            }
            if (NPC.CountNPCS(ModContent.NPCType<Etheria>()) > 0)
            {
                float mag = 96 * Etheria.scale;
                Vector2 rot;
                rot.X = (float)Math.Cos(Etheria.thetaSlow + 3.14f * _index / 4) * mag;
                rot.Y = (float)Math.Sin(Etheria.thetaSlow + 3.14f * _index / 4) * mag;
                _targetPos = Etheria.center + rot;
            }
            else
            {
                NPC.active = false;
                NPC.life = 0;
            }
            _delay++;
            if(_delay > 360)
            {
                _delay = 0;
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<EtherialYeet>(), (int)(NPC.damage / 4), 3, Main.myPlayer);
                }
            }

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
            NPC.rotation += 3.14f / 20f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(44, 300, true);
        }

        public override Color? GetAlpha(Color drawColor)
        {
            int b = 125;
            int b2 = 225;
            int b3 = 255;
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
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 0f;
            return null;
        }
    }
}
