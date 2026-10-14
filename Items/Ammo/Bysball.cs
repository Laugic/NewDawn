using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class Bysball : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.BysballProjectile>();
            Item.shootSpeed = 14f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(333);
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 3);
            recipe.AddIngredient(ModContent.ItemType<EtherialEssence>(), 6);
            recipe.Register();
        }
    }
}
