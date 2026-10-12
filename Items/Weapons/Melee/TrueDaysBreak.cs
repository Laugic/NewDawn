using Microsoft.Xna.Framework;
using System;
using NewDawn.Buffs;
using NewDawn.Projectiles.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Melee
{
    public class TrueDaysBreak : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Sunrise");
            // Tooltip.SetDefault("'Bring the sky'\nStriking enemies makes them emit True Sparks\nEnemies explode into True Sparks on death, spreading the Redemption.");
        }

        public override void SetDefaults()
        {
            Item.damage = 48;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<TrueGoldenSword>();
            Item.scale *= 1.25f;
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for(int i = 0; i < 3; i++)
            {
                float theta = (float)Main.rand.NextDouble() * 3.14f / 6 + 3.14f * 255f / 180f;
                float mag = 600 + Main.rand.Next(200);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), (int)(Main.MouseWorld.X) + (int)(mag * Math.Cos(theta)), (int)(Main.MouseWorld.Y) + (int)(mag * Math.Sin(theta)), -25 * (float)Math.Cos(theta), -25 * (float)Math.Sin(theta), ModContent.ProjectileType<TrueDawnStar>(), damage, 3, Main.myPlayer);
            }
            int numberProjectiles = Main.rand.Next(1, 4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                float scale = 1.1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<TrueGoldenSword>(), damage, knockback, player.whoAmI);
            }
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<TrueDawn>(), 12 * 60 + Main.rand.Next(8 * 60));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaysBreak>());
            recipe.AddRecipeGroup("TitaniumBars", 12);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.LargeDiamond, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}