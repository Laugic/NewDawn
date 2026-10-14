using NewDawn.Control;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using NewDawn.Items.Loot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Mystic
{
	public class StaffOfAnDio : MysticItem
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Loki's Staff");
            // Tooltip.SetDefault("'Many tricks up his sleeve'");
            Item.staff[Item.type] = true; 
        }

		public override void SetMysticDefaults()
		{
			Item.damage = 40;
            Item.width = 48;
			Item.height = 48;
			Item.useTime = 18;
			Item.useAnimation = 28;
			Item.useStyle = 5;
			Item.noMelee = true; 
			Item.knockBack = 2;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (Main.LocalPlayer.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    return "Shoots a slightly homing orb";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Shoots a homing beam that inflicts 'Time Dilation', which\ncauses enemies to take a burst of damage when Time is Stopped.";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Spawns energy orbs that create stalagmites and stalactites.";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 65f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return base.MysticShoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 42;
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 8;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<AnDioDestruction1>();
            Item.UseSound = SoundID.Item20;
            Item.scale = 1f;
            LuxCost = 8;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 26;
            Item.useAnimation = Item.useTime = 4;
            Item.knockBack = 5;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<AnDioIllusion>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item20;
            Item.scale = 1f;
            VisCost = 4;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 38;
            Item.useAnimation = Item.useTime = 15;
            Item.knockBack = 2;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<AnDioConjuration1>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item20;
            Item.scale = 1f;
            MundusCost = 15;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeManager.TitaniumBars, 12);
            recipe.AddIngredient(ModContent.ItemType<DioritusCore>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
            recipe.AddIngredient(3081, 25);
            recipe.AddIngredient(3086, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}