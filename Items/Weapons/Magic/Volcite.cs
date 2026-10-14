using Microsoft.Xna.Framework;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Magic
{
    public class Volcite : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
}

        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.width = 28;
            Item.height = 30;
            Item.useAnimation = Item.useTime = 20;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.VolciteProjectile>();
            Item.shootSpeed = 18f;
        }

        public override void HoldItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Item.useAnimation = Item.useTime = 20 - modPlayer.ObsidiumHeart;
            base.HoldItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            int id = Projectile.NewProjectile(new EntitySource_Parent(player), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), type, damage, knockback, Main.myPlayer);
            Main.projectile[id].ai[1] = modPlayer.ObsidiumHeart;
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(ObsidiumBar), 16);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}