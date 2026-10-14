using Microsoft.Xna.Framework;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Magic;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Magic
{
    public class AncientStaff : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
}

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 2;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<CrystillaShardProjectile>();
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shootSpeed = 4f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float theta = ((float)Main.rand.NextDouble() * 3.14f / 6) + (3.14f * 255f / 180f);
            float mag = 600;
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2((int)(Main.MouseWorld.X + Main.rand.Next(-40, 40)) + (int)(mag * Math.Cos(theta)), (int)player.position.Y + (int)(mag * Math.Sin(theta))), new Vector2(-15 * (float)Math.Cos(theta), -15 * (float)Math.Sin(theta)), ModContent.ProjectileType<CrystillaShardProjectile>(), damage, 3, Main.myPlayer);
            theta = ((float)Main.rand.NextDouble() * 3.14f / 6) + (3.14f * 255f / 180f);
            mag = 700;
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2((int)(Main.MouseWorld.X + Main.rand.Next(-40, 40)) + (int)(mag * Math.Cos(theta)), (int)player.position.Y + (int)(mag * Math.Sin(theta))), new Vector2(-15 * (float)Math.Cos(theta), -15 * (float)Math.Sin(theta)), ModContent.ProjectileType<CrystillaShardProjectile>(), damage, 3, Main.myPlayer);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(AncientShard), 1);
            recipe.AddIngredient(ItemID.FossilOre, 6);
            recipe.AddIngredient(Mod, nameof(Crystilla), 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}