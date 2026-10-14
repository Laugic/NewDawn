using NewDawn.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Melee
{
    class Flarance : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 42;
            Item.height = 48;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;    
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.FlaranceProjectile>();
            Item.shootSpeed = 14f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), Item.shoot, damage, knockback, player.whoAmI);
            Main.projectile[id].ai[0] = modPlayer.ObsidiumHeart;
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
