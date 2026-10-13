using NewDawn.Items.Materials;
using NewDawn.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Range
{
	public class ObsidiumGreatbow : NewDawnItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Molten Frenzy'\n20% chance to not consume ammo\nBecomes stronger as you consume Obsidium Hearts");
		}

		public override void SetDefaults()
        {
            Item.scale *= 1.2f;
            Item.damage = 28;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 64;
			Item.useAnimation = Item.useTime = 30;
			Item.useStyle = 5;
			Item.noMelee = true; 
			Item.knockBack = 3;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item10;
			Item.autoReuse = true;
			Item.shoot = 10; 
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Arrow;
        }

        public override void HoldItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Item.useAnimation = Item.useTime = 35 - modPlayer.ObsidiumHeart * 2;
            base.HoldItem(player);
        }


        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(ObsidiumBar), 16);
            recipe.AddTile(16);
			recipe.Register();
		}
        
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .20f;
		}
        
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (type == ProjectileID.WoodenArrowFriendly && modPlayer.ObsidiumHeart > 0) 
			{
				type = ModContent.ProjectileType<ObsidiumArrow>(); 
			}
            if (modPlayer.ObsidiumHeart >= 5)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
            }
            
            return true; 
		}
	}
}
