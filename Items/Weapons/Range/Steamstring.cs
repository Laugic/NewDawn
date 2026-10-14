using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Range
{
	public class Steamstring : NewDawnItem
	{
        //public bool steam = true;
        //public int steamTier = 1;
        //public int steamCost = 60;

		public override void SetDefaults()
        {
            //steam = true;
            //steamTier = 1;
            //steamCost = 60;
            Item.scale *= 1.2f;
            Item.damage = 48;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 62;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item10;
			Item.autoReuse = true;
			Item.shoot = 10; 
			Item.shootSpeed = 16f;
			Item.useAmmo = 40;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(SteamBar), 16);
            recipe.AddIngredient(Mod, nameof(SoulOfSought), 8);
            recipe.AddIngredient(ModContent.ItemType<SoulOfThought>(), 8);
            recipe.AddIngredient(Mod, nameof(Gear), 8);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
        /*
        //Steam item
        //VV
        public override bool CanUseItem(Player player)
        {

            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (steam)
            {
                if (modPlayer.connected >= steamTier && NewDawnWorld.power >= steamCost)
                    return true;
                else
                    return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            NewDawnWorld.power -= steamCost;
            if (NewDawnWorld.power < 0)
                NewDawnWorld.power = 0;
            return true;
        }
        //^^
        //Steam Item end
        */
            
        public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .33f;
		}

        public override void HoldItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);

            //Main.NewText("Steam Tier: " + steamTier.ToString(), 0, 250, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));

            float scale = 1f - (Main.rand.NextFloat() * .2f);
            perturbedSpeed = perturbedSpeed * scale;
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<BrassArrowProjectile>(), damage, knockback, player.whoAmI);
            
            if(type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), ModContent.ProjectileType<BrassArrowProjectile>(), damage, knockback, player.whoAmI);
                return false;
            }
            return true;
        }
    }
}
