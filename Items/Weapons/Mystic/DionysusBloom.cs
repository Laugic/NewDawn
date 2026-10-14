using NewDawn.Items.Loot;
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
	public class DionysusBloom : MysticItem
    {
        public int damage = 0;
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dionysus' Bloom");
            // Tooltip.SetDefault("'Grow his strength' \nIllusion inflicts 'Venom'\nFires different projectiles based on Mysticism");
            Item.staff[Item.type] = true;
        }
        
        public override void SetMysticDefaults()
		{
			Item.damage = 44;
            Item.width = 64;
			Item.height = 64;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 5;
			Item.noMelee = true; 
			Item.knockBack = 2;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
		}

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 64f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Illusion)
            {

                int numberProjectiles = Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10)); 
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<DionysusIllusion>(), damage, knockBack, player.whoAmI);
                }
                return false;
            }
            return true;
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 72;
            Item.useTime = 25;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 0;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<DionysusDestruction>();
            Item.UseSound = SoundID.Item1;
            Item.scale = 1.25f;
            LuxCost = 6;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 42;
            Item.useTime = 10;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 5;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1f;
            VisCost = 4;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 44;
            Item.useTime = 30;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<DionysusConjuration>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1f;
            MundusCost = 20;
        }

       


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(1006, 16); //Chlorophyte
            recipe.AddIngredient(Mod, nameof(SoulOfSought), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}