using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Weapons.Mystic
{
    public class CharonsScepter : MysticItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charon's Scepter");
            // Tooltip.SetDefault("Pay the toll");
            Item.staff[Item.type] = true;
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 32;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.useStyle = 5;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<AgnesDestruction>();
        }


        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "Shoots a shotgun of bones";
                case 2:
                    return "Shoots a bouncing spiked ball that inflict 'Undeath', which \nmakes enemies spawn powerful shadows on death";
                case 3:
                    return "Create an orbiting spikeball";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 48f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
            {
                int numberProjectiles = Main.rand.Next(4, 7);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));

                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockBack, player.whoAmI);
                }
                return false;
            }
            return true;
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 24;
            Item.useAnimation = Item.useTime = 30;
            Item.knockBack = 8;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<CharonDestruction>();
            LuxCost = 10;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 28;
            Item.useAnimation = Item.useTime = 21;
            Item.knockBack = 8;
            Item.shootSpeed = 8f;
            Item.useStyle = 5;
            Item.shoot = ModContent.ProjectileType<CharonIllusion>();
            VisCost = 33;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.damage = 28;
            Item.useAnimation = Item.useTime = 60;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.useStyle = 1;
            Item.shoot = ModContent.ProjectileType<CharonConjuration>();
            MundusCost = 40;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.AddIngredient(ItemID.GoldenKey, 1);
            recipe.AddIngredient(ItemID.GoldCoin, 1);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.Register();
        }
    }
}