using NewDawn.Dusts;
using NewDawn.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Melee
{
	public class Scydian : NewDawnItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'The power of magma'\nBecomes stronger as you consume Obsidium Hearts");
		}

		public override void SetDefaults()
		{
            Item.scale = 1.25f;
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 64;
			Item.height = 56;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = 1;
			Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
        }

        public override void HoldItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Item.useAnimation = Item.useTime = 40 - modPlayer.ObsidiumHeart * 2;
            Item.scale = 1.25f + .1f * modPlayer.ObsidiumHeart;
            base.HoldItem(player);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, nameof(ObsidiumBar), 16);
			recipe.AddTile(16);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Magma>());
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.ObsidiumHeart > 0)
			    target.AddBuff(BuffID.OnFire, 2 * 60 + Main.rand.Next(60));
		}
	}
}
