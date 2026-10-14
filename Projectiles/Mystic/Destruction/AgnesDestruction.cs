using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Items.Weapons.Mystic;

namespace NewDawn.Projectiles.Mystic.Destruction
{
    public class AgnesDestruction : DestructionProjectile
    {
        bool shoot = false;
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.scale = 2f;
            Projectile.aiStyle = 19;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            shoot = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.direction = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = Main.player[Projectile.owner].itemAnimation;
            Projectile.position.X = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - (float)(Projectile.width / 2);
            Projectile.position.Y = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - (float)(Projectile.height / 2);
            Projectile.position += Projectile.velocity * Projectile.ai[0];

            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 3f;
                Projectile.netUpdate = true;
            }
            if (player.itemAnimation < player.itemAnimationMax / 3)
            {
                Projectile.ai[0] -= 2.4f;
                if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
                {
                    Projectile.localAI[0] = 1f;
                }
                if (!shoot)
                {
                    if (Main.myPlayer == Projectile.owner)
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<AgnesDestruction2>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                    shoot = true;
                }
            }
            else
            {
                Projectile.ai[0] += 1.25f;
            }

            if (Main.player[Projectile.owner].itemAnimation == 0 || Main.player[Projectile.owner].GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode != global::NewDawn.Players.MysticPlayer.MysticMode.Destruction)
            {
                Projectile.Kill();
            }

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += 1.57f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int imageHeight = TextureAssets.Projectile[Projectile.type].Value.Height;
            int y6 = imageHeight * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, imageHeight)), Projectile.GetAlpha(Color.White), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)imageHeight / 2f) + new Vector2(40, -40), Projectile.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
    }
}
