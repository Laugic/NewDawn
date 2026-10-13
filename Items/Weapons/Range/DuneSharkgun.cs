using System;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Range
{
    public class DuneSharkgun : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("33% chance to not consume ammo\nOccasionally shoots Crystal bullets");
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 86;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item41;
            Item.autoReuse = true;
            Item.channel = true;
            Item.shoot = 10;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Minishark, 1);
            recipe.AddIngredient(Mod, nameof(AncientShard), 1);
            recipe.AddIngredient(ItemID.FossilOre, 4);
            recipe.AddIngredient(Mod, nameof(Crystilla), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .33f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 14f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Vector2 perturbedSpeed = velocity;

            perturbedSpeed = perturbedSpeed * 1.33f;
            if (Main.rand.Next(3) == 0)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileID.CrystalBullet, (int)(damage * 1.25), knockback, player.whoAmI);

            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -4);
        }
    }
}
