using NewDawn.Projectiles.Thrown;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Thrown
{
    public class ChakramOfTheViciousAssassin : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chakram of The Vicious Assassin");
            // Tooltip.SetDefault("'Surround and eliminate'");
        }
        public override void SetDefaults()
        {
            Item.damage = 320;           
            Item.DamageType = DamageClass.Throwing;            
            Item.noMelee = true;
            Item.width = 90;
            Item.height = 90;
            Item.useTime = 20;      
            Item.useAnimation = 20;   
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;       
            Item.shoot = ModContent.ProjectileType<ViciousAssassinMain>();  
            Item.shootSpeed = 20f;     
            Item.useTurn = true;
            Item.maxStack = 1;       
            Item.consumable = false;  
            Item.noUseGraphic = true;

        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(3467, 16);
            recipe.AddIngredient(ModContent.ItemType<NovaFragment>(), 8);
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/

    }
}