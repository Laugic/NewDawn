using Terraria;
using Terraria.ID;
using System;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;
using Terraria.ModLoader;
using NewDawn.Items.Materials;

namespace NewDawn.Items.Weapons.Mystic
{
    public class OrionsGate : MysticItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orion's Gate");
            // Tooltip.SetDefault("'Open the path to the stars'\nIllusion inflicts 'Cosmic Disarray', which drains life and makes enemies take more damage.\nFires different projectiles based on Mysticism\nConsuming enemies increases the Gate's power.");
            Item.staff[Item.type] = true;
        }
        
        public override void SetMysticDefaults()
        {
            Item.damage = 40;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item105;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 50f;

            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Destruction)
            {
                mysticPlayer.OrionCharge++;
                mysticPlayer.UsingMysticWeapon = 60;
                if (mysticPlayer.OrionCharge > 24)
                    mysticPlayer.OrionCharge = 24;
                for (int i = 0; i < mysticPlayer.OrionCharge / 2 + 1; i++)
                {
                    float theta = (float)Main.rand.NextDouble() * 3.14f / 6 + 3.14f * 255f / 180f;
                    theta += -mysticPlayer.OrionCharge / 24 * 3.14f / 6 + 2 * mysticPlayer.OrionCharge / 24 * (float)Main.rand.NextDouble() * 3.14f / 6;
                    float mag = 600 + Main.rand.Next(200 + 4 * mysticPlayer.OrionCharge);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2((int)(Main.MouseWorld.X) + (int)(mag * Math.Cos(theta)) - 32 - mysticPlayer.OrionCharge + Main.rand.Next(64 + 2 * mysticPlayer.OrionCharge), (int)(Main.MouseWorld.Y) + (int)(mag * Math.Sin(theta)) - 32 - mysticPlayer.OrionCharge + Main.rand.Next(64 + 2 * mysticPlayer.OrionCharge)), new Vector2(-25 * (float)Math.Cos(theta), -25 * (float)Math.Sin(theta)), ModContent.ProjectileType<OrionDestruction>(), damage, 3, Main.myPlayer);
                }
            }
            if(player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Illusion)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(player.position.X, player.position.Y - 600), new Vector2(-4, 0), ModContent.ProjectileType<OrionIllusionSpawn>(), damage, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(player.position.X, player.position.Y - 600), new Vector2(4, 0), ModContent.ProjectileType<OrionIllusionSpawn>(), damage, 3, Main.myPlayer);
            }
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration)
            {
                bool projExists = false;
                foreach( Projectile projectile in Main.projectile)
                {
                    if(projectile.type == ModContent.ProjectileType<OrionConjuration>())
                    {
                        projectile.timeLeft = 90;
                        mysticPlayer.UsingMysticWeapon = 90;
                        projExists = true;
                    }
                }
                if(!projExists)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2((int)(Main.MouseWorld.X), (int)(Main.MouseWorld.Y)), new Vector2(0, 0), ModContent.ProjectileType<OrionConjuration>(), damage, 3, Main.myPlayer);
                }
            }
            return true;
        }
        
        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 48;
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 4;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.noUseGraphic = false;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 42;
            Item.useTime = 90;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 1;
            Item.shootSpeed = 22f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.noUseGraphic = false;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 35;
            Item.useTime = 40;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 0f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.noUseGraphic = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(GalacticFragment), 15);
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}