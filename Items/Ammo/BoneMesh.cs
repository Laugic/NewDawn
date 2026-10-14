using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class BoneMesh : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.BoneMeshProjectile>();
            Item.shootSpeed = 14f;
            Item.ammo = AmmoID.Snowball;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(30);
            recipe.AddIngredient(ItemID.Bone, 1);
            recipe.AddTile(TileID.Extractinator);
            recipe.Register();
        }
    }
}
