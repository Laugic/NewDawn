using Microsoft.Xna.Framework;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Mystic
{
    public class GaiasWorld : MysticItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gaia's World");
            // Tooltip.SetDefault("The World is in your hands");
            Item.staff[Item.type] = true;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (Main.LocalPlayer.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    return "Shoots a burst of gem shards";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Shoots large gemstones that inflict 'Refracting', which makes enemies break off gem shards upon being hit,\ndealing damage based on their defense";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Spawns Sigils with different effects based on their Focus";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Destruction)
            {
                int numberProjectiles = Main.rand.Next(4, 7);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));

                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockBack, player.whoAmI);
                    Main.projectile[id].ai[1] = Main.rand.Next(6);
                }
                return false;
            }
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Illusion || player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration)
            {
                int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), type, damage, knockBack, player.whoAmI);
                Main.projectile[id].ai[1] = Main.rand.Next(6);
                return false;
            }
            return true;
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 15;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GaiaDestruction>();
            Item.shootSpeed = 6f;
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 10;
            Item.useAnimation = Item.useTime = 30;
            Item.knockBack = 6;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<GaiaDestruction>();
            LuxCost = 10;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 15;
            Item.useTime = Item.useAnimation = 30;
            Item.knockBack = 4;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<GaiaIllusion>();
            VisCost = 15;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 20;
            Item.useTime = Item.useAnimation = 30;
            Item.knockBack = 3;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<GaiaConjuration>();
            MundusCost = 20;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
            recipe.AddIngredient(ItemID.Amethyst, 3);
            recipe.AddIngredient(ItemID.Topaz, 3);
            recipe.AddIngredient(ItemID.Sapphire, 3);
            recipe.AddIngredient(ItemID.Emerald, 3);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 3);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}