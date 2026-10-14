using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using NewDawn.Projectiles.Mystic.Conjuration;

namespace NewDawn.Items.Weapons.Mystic
{
    public class ChlorysBlade : MysticItem
    {

        public override void SetMysticDefaults()
        {
            Item.damage = 10;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.noMelee = false;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 10;
            Item.useTime = 48;
            Item.useAnimation = (int)(Item.useTime / 2);
            Item.knockBack = 3;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<ChlorysDestruction>();
            Item.UseSound = SoundID.Item1;
            LuxCost = 6;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 8;
            Item.useTime = 30;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 5;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<ChlorysIllusion>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1f;
            VisCost = 12;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 10;
            Item.useTime = 60;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<ChlorysConjuration1>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1f;
            MundusCost = 25;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (Main.LocalPlayer.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    return "Shoots seeds";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Shoots blooms that inflict 'Fertile', which makes enemies take damage for each seed that hits them";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Shoots sunflowers that drop seeds and sunlight";
                default:
                    return "";
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 16);
            recipe.AddIngredient(ItemID.Acorn, 6);
            recipe.AddIngredient(ItemID.Sunflower, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}