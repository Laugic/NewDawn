using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.RockTwins
{
    public class LaserBall : ModNPC
    {
        public int laserBallNum = 0;
        public static int life = 0;
        public bool zImmune = true;
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("LaserBall"); 
		}
        public override void SetDefaults()
        {
            zImmune = true;
            life = 0;
            laserBallNum = 0;
            NPC.width = 16;
			NPC.height = 16;
			NPC.aiStyle = -1;
			//aiType = 429;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 2000;
			NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCHit1;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.buffImmune[BuffID.Poisoned] = true;
			NPC.buffImmune[BuffID.OnFire] = true;
			NPC.buffImmune[BuffID.Venom] = true;
			NPC.buffImmune[BuffID.ShadowFlame] = true;
			NPC.buffImmune[BuffID.CursedInferno] = true;
			NPC.buffImmune[BuffID.Frostburn] = true;
			NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.dontTakeDamage = true;
            NPC.netAlways = true;
        }
		public override bool CheckActive()
		{
			if(NPC.CountNPCS(ModContent.NPCType<Andesia>()) < 1)
				return false;
			return true;
		}
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			//target.AddBuff(BuffID.Shadowflame, 300, true);
		}
        
		public override Color? GetAlpha(Color drawColor)
		{
			return Color.White;
		}
		public override void AI()
		{
            NPC.life = life;
            int flameCount = 1;
            if(laserBallNum == 0)
            {
                laserBallNum = (int)Main.npc[(int)NPC.ai[3]].ai[1];
                Main.npc[(int)NPC.ai[3]].ai[1]++;
            }
            flameCount = NPC.CountNPCS(ModContent.NPCType<LaserBall>());
            float divisions = 6.28f / flameCount;
            float flameTheta = Main.npc[(int)NPC.ai[3]].ai[0] + laserBallNum * divisions;
            double targetX = Andesia.posX + Andesia.dist * Math.Cos(flameTheta) - NPC.width / 2;
            double targetY = Andesia.posY + Andesia.dist * Math.Sin(flameTheta);
            NPC.position.X = (float)targetX;
            NPC.position.Y = (float)targetY;
           
            
            for (int k = 0; k < 2; k++)
            {                                                                                               
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, ModContent.DustType<Blue>(), 0f, 0f);
            }
		}
		
    }
}