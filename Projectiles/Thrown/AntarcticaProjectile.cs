using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class AntarcticaProjectile : ModProjectile
    {
        bool stuck = false;
        int npcIndex = -1;
        Vector2 relPos;
        bool justSpawned = false;
        int duration = 1;
        bool seaking = false;
        float rotPos = 0;
        bool targetFound = false;
        int npcTarget = -1;
        float npcDistance = 8000;
        Vector2 targetPos;
        int delay = 0;
        int range = 600;
        int origDmg = 150;
        int counter = 0;

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 7 * 60;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if(npcIndex != -1)
            {
                if(!Main.npc[npcIndex].active)
                {
                    npcIndex = -1;
                    stuck = false;
                    seaking = true;
                }
            }

            if(!justSpawned)
            {
                justSpawned = true;
                if ((NewDawnPlayer.Get(Main.player[Projectile.owner]).Etherable > 2 || NewDawnWorld.downedEtheria) && NewDawnWorld.downedTrueEtheria)
                    duration = 2;
                Projectile.timeLeft = 7 * 60 * duration;
                origDmg = Projectile.damage;
            }
            if (stuck)
                Stuck();
            else if (seaking)
                Seaking();
            else
                Fall();
            Hail();
        }

        private void Hail()
        {
            counter++;
            if(stuck)
            {
                if (counter > 20 || ((NewDawnWorld.downedEtheria || NewDawnPlayer.Get(Main.player[Projectile.owner]).Etherable > 0) && NewDawnWorld.downedTrueEtheria && counter > 10))
                {
                    counter = 0;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Hail>(), origDmg, 0, Projectile.owner);
                }
            }
            else if(counter > 10 || ((NewDawnWorld.downedEtheria || NewDawnPlayer.Get(Main.player[Projectile.owner]).Etherable > 0) && NewDawnWorld.downedTrueEtheria && counter > 5))
            {
                counter = 0;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Hail>(), origDmg, 0, Projectile.owner);
            }
        }

        private void Stuck()
        {
            if(npcIndex != -1)
            {
                Projectile.position = Main.npc[npcIndex].Center + relPos;
                Projectile.rotation = rotPos;
                Projectile.knockBack = 0;
                Projectile.damage = 0;
                Projectile.tileCollide = false;
            }
        }

        private void Seaking()
        {
            Projectile.knockBack = 8;
            Projectile.damage = origDmg;
            Projectile.tileCollide = true;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2f;
            Projectile.ai[0] += .01f;
            Projectile.velocity.Y += Projectile.ai[0];
        }
        
        private void Fall()
        {
            Projectile.ai[0] += .02f;
            Projectile.velocity.Y += Projectile.ai[0];
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!stuck)
            {
                Projectile.Kill();
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(!stuck)
                npcIndex = target.whoAmI;
            if (npcIndex != -1 && npcIndex == target.whoAmI && !stuck)
            {
                relPos = Projectile.position - Main.npc[npcIndex].Center;
                stuck = true;
                Projectile.timeLeft = 7 * 60 * duration;
                seaking = false;
                rotPos = Projectile.rotation;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int imageHeight = TextureAssets.Projectile[Projectile.type].Value.Height;
            int y6 = imageHeight * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, imageHeight)), Projectile.GetAlpha(Color.White), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)imageHeight / 2f) + new Vector2(16, -16), Projectile.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
    }
}