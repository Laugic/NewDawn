using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
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

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "Shoots a burst of gem shards";
                case 2:
                    return "Shoots large gemstones that inflict 'Refracting', which makes enemies break off gem shards upon being hit,\ndealing damage based on their defense";
                case 3:
                    return "Spawns Sigils with different effects based on their Focus";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
            {
                int numberProjectiles = Main.rand.Next(4, 7);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));

                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                    Main.projectile[id].ai[1] = Main.rand.Next(6);
                }
                return false;
            }
            if(modPlayer.MysticMode == 2 || modPlayer.MysticMode == 3)
            {
                int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
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

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 10;
            Item.useAnimation = Item.useTime = 30;
            Item.knockBack = 6;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<GaiaDestruction>();
            LuxCost = 10;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 15;
            Item.useTime = Item.useAnimation = 30;
            Item.knockBack = 4;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<GaiaIllusion>();
            VisCost = 15;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
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
            recipe.AddRecipeGroup("IronBar", 12);
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