using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Ranged;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class SpineChiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spine Chiller");
            // Tooltip.SetDefault("Shoots down in an area around your cursor\n80% chance not to consume ammo");
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useAnimation = 30;
            Item.useTime = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .8f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float theta = (float)Main.rand.NextDouble() * 3.14f * 2;
            float mag = 50;
            if(Main.netMode != NetmodeID.MultiplayerClient)
                Projectile.NewProjectile(source,new Vector2((int)(Main.MouseWorld.X) + (int)(mag * Math.Cos(theta)), player.position.Y - 600 - Math.Abs((int)(mag * Math.Sin(theta)))), new Vector2(-3 + 6 * (float)Main.rand.NextDouble(), 8 + 2 * Math.Abs((float)Math.Sin(theta))), type, damage, 3, Main.myPlayer);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -8);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpookyWood, 60);
            recipe.AddIngredient(ItemID.Ectoplasm, 8);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}