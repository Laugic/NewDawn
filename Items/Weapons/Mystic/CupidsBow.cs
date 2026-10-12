using NewDawn.Items.Loot;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewDawn.Items.Weapons.Mystic
{
	public class CupidsBow : MysticItem
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cupid's Bow");
            /* Tooltip.SetDefault("'Make them fall for you' \nArrows inflict 'Lovestruck', which makes enemies drop Hearts on death" +
                "\nFires different projectiles based on Mysticism\nThe amount of angels that can be conjured is based on Mystic Duration"); */
        }
        
        public override void SetMysticDefaults()
		{
			Item.damage = 40;
            Item.width = 44;
			Item.height = 74;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = 10000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
		}

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 50f;

            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 3)
            {
                for (int p = 0; p < 1000; p++)
                {
                    if (Main.projectile[p].type == ModContent.ProjectileType<CupidConjurationAngel>())
                    {
                        if (player.ownedProjectileCounts[ModContent.ProjectileType<CupidConjurationAngel>()] >= modPlayer.MysticDuration * 4)
                        {
                            Main.projectile[p].Kill();
                            break;
                        }
                    }
                }
            }
            return true;
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.useStyle = 1;
            Item.damage = 48;
            Item.useTime = 18;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 4;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<CupidDestruction>();
            Item.noUseGraphic = true;
            LuxCost = 4;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.useStyle = 5;
            Item.damage = 42;
            Item.useTime = 16;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 1;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<CupidIllusion>();
            Item.noUseGraphic = false;
            VisCost = 5;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.useStyle = 5;
            Item.damage = 35;
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 0f;
            Item.shoot = ModContent.ProjectileType<CupidConjurationAngel>();
            Item.noUseGraphic = false;
            MundusCost = 24;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Pearlwood, 24);
            recipe.AddRecipeGroup("SilverBars", 8);
            recipe.AddIngredient(ItemID.SoulofLight, 6);
            recipe.AddIngredient(Mod, nameof(SoulOfSought), 4);
            recipe.AddIngredient(ItemID.CrystalShard, 4);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
        }
    }
}