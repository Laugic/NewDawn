using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Items.Loot;

namespace NewDawn.Items.Weapons.Thrown
{
    public class Daggoritus : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.damage = 38;           
            Item.DamageType = DamageClass.Throwing;            
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 20;      
            Item.useAnimation = 20;   
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;       
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.DaggoritusProjectile>();  
            Item.shootSpeed = 10f;     
            Item.useTurn = true;
            Item.maxStack = 1;       
            Item.consumable = false;  
            Item.noUseGraphic = true;

        }
        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DioritusCore>(), 1);
            recipe.AddIngredient(3081, 25);
            recipe.AddIngredient(3086, 25);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}