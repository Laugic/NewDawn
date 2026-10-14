using NewDawn.Buffs;
using NewDawn.Dusts;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Melee
{
	public class TrainScythe : NewDawnItem
	{

		public override void SetDefaults()
		{
            Item.scale = 3f;
			Item.damage = 832;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 144;
			Item.height = 144;
			Item.useTime = 55;
			Item.useAnimation = 55;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, nameof(SteamBar), 16);
            recipe.AddIngredient(Mod, nameof(SoulOfWrought), 8);
            recipe.AddIngredient(Mod, nameof(Gear), 8);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Steam>());
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<Steamy>(), 2 * 60);
		}
	}
}
