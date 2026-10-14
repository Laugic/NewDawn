using NewDawn.Buffs;
using NewDawn.Control;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Melee
{
    public class Mariana : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'To the depths'\nStriking enemies makes them emit bubbles!\nEnemies explode into bubbles on death, spreading the bubbles.");
        }

        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 48;
            Item.height = 54;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 10f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Bubbly>(), (4 * 60) + Main.rand.Next(2 * 60));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeManager.GoldBars, 5);
            recipe.AddRecipeGroup(RecipeManager.SilverBars, 10);
            recipe.AddIngredient(ItemID.Seashell, 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ItemID.SharkFin, 2);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}