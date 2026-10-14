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
	public class DaysBreak : NewDawnItem
	{

		public override void SetDefaults()
		{
			Item.damage = 26;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 58;
			Item.height = 58;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = 1;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<GoldenSword>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float theta = (float)Main.rand.NextDouble() * 3.14f / 6 + 3.14f * 255f/180f;
            float mag = 600;
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2((int)(Main.MouseWorld.X) + (int)(mag * Math.Cos(theta)), (int)(player.position.Y) + (int)(mag * Math.Sin(theta))), new Vector2(-15 * (float)Math.Cos(theta), -15 * (float)Math.Sin(theta)), ModContent.ProjectileType<DawnStar>(), damage, 3, Main.myPlayer);
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<DawnBuff>(), 5 * 60 + Main.rand.Next(3 * 60));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Starfury);
            recipe.AddIngredient(ItemID.EnchantedSword);
            recipe.AddIngredient(ModContent.ItemType<Flarance>());
            recipe.AddIngredient(ModContent.ItemType<Mariana>());
            recipe.AddTile(TileID.SkyMill);
			recipe.Register();


            Recipe recipe2 = Recipe.Create(ItemID.SkyMill);
            recipe2.AddIngredient(ItemID.SunplateBlock, 20);
            recipe2.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe2.Register();
        }
	}
}