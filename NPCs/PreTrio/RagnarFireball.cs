using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace NewDawn.NPCs.PreTrio
{
    public class RagnarFireball : ModProjectile
    {
        public Texture2D Fireball;
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = false;
            Projectile.hostile = true;
            /*projectile.friendly = true;
            projectile.hostile = false;*/
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;

            if (!Main.dedServ)
            {
                Fireball = Mod.GetTexture(this.GetType().GetRootPath() + "/RagnarFireballLayers");
            }
        }

        public override void AI()
        {
            if(Projectile.ai[0] == 1) // Gravity
                Projectile.velocity.Y += .2f;
            if (Projectile.ai[0] == 2)
                SlowAI();
            if (Projectile.ai[0] == 3)
                SpiralAI();
            Projectile.rotation += .01f;
        }

        private void SpiralAI()
        {
            Projectile.velocity = new Vector2(Projectile.velocity.Length() * (float)Math.Cos(Projectile.velocity.ToRotation() + .015),
                Projectile.velocity.Length() * (float)Math.Sin(Projectile.velocity.ToRotation() + .015));
        }

        private void SlowAI()
        {
            Projectile.velocity *= .98f;
            Projectile.timeLeft = 3 * 60;
            if (Projectile.velocity.Length() < 1)
            {
                int npc = 0;
                if (Projectile.ai[1] < 0 || Projectile.ai[1] >= Main.npc.Length)
                    return;

                npc = (int)Projectile.ai[1];

                if(Main.npc[npc].type == ModContent.NPCType<Ragnar>() && Main.player[Main.npc[npc].target].active && Main.player[Main.npc[npc].target].statLife > 0)
                {
                    Projectile.velocity = Projectile.position - Main.player[Main.npc[npc].target].position;
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= -12;
                    Projectile.ai[1] = 0;
                    Projectile.ai[0] = 0;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var drawPos = new Vector2();
            var drawMult = new Vector2(-1f, -1f);
            drawMult *= Projectile.velocity;
            Vector2 rotOffset = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width / 2, TextureAssets.Projectile[Projectile.type].Value.Width / 2);
            var spriteFX = SpriteEffects.None;
            var imageRot = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            for (int i = 0; i < 10; i++)
            {
                Rectangle rect = new Rectangle(0, i / 2 * 40, 40, 40);
                int sign = 1;
                if (i / 2 == 2)
                {
                    spriteFX = SpriteEffects.FlipHorizontally;
                    sign = -1;
                }
                float rot = Projectile.rotation * (i / 2 + 1) / 2 * sign;
                spriteBatch.Draw(Fireball, Projectile.position - Main.screenPosition + rotOffset + drawPos * (i%2==0?2:1), rect, Color.White, (i==0? imageRot:rot), rotOffset, Projectile.scale, spriteFX, 0f);
                if(i % 2 == 0)
                    drawPos += drawMult;
                if (i == 0)
                    i++;
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 4 * 60, true);
        }
    }
}