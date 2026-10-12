using NewDawn.Buffs;
using NewDawn.Items.Loot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Projectiles.Summon;

namespace NewDawn.Items.Weapons.Summon
{
	public class Antenna : NewDawnItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Summons a TV to fight for you.");
		}

		public override void SetDefaults()
		{
			Item.damage = 55;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 16;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 1;
			Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(0, 25, 0, 0);
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ModContent.ProjectileType<TV>();
			Item.shootSpeed = 12f;
			Item.buffType = ModContent.BuffType<TVBuff>();
			Item.buffTime = 3600;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return player.altFunctionUse != 2;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(Mod, nameof(SteamBar), 12);
            recipe.AddIngredient(ModContent.ItemType<SoulOfThought>(), 20);
            recipe.AddIngredient(1344, 40);
            recipe.Register();
        }
    }
}
