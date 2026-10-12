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
	public class HallowsEve : MysticItem
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallow's Eve");
             // Tooltip.SetDefault("'The spirits of the darkness are bent to your will'");
            Item.staff[Item.type] = true;
        }

        public override void SetMysticDefaults()
		{
			Item.damage = 55;
            Item.width = 116;
			Item.height = 122;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 5;
			Item.noMelee = true; 
			Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
            Item.scale = .75f;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "Shoots a burst of skulls that follow your mouse";
                case 2:
                    return "Shoots an orb with a fire trail that inflicts 'Spooked', which\nmakes enemies take more damage for each unique thing that hits them";
                case 3:
                    return "Creates Jack-o-lanterns that shoot at nearby enemies";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
            {
				Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 0f, 8f, ModContent.ProjectileType<HallowsEveDestruction1>(), damage, 3f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 0f, -8f, ModContent.ProjectileType<HallowsEveDestruction1>(), damage, 3f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 8f, 0f, ModContent.ProjectileType<HallowsEveDestruction1>(), damage, 3f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, -8f, 0f, ModContent.ProjectileType<HallowsEveDestruction1>(), damage, 3f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 6f, 6f, ModContent.ProjectileType<HallowsEveDestruction2>(), damage, 3f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, -6f, -6f, ModContent.ProjectileType<HallowsEveDestruction2>(), damage, 3f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 6f, -6f, ModContent.ProjectileType<HallowsEveDestruction2>(), damage, 3f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, -6f, 6f, ModContent.ProjectileType<HallowsEveDestruction2>(), damage, 3f, player.whoAmI);
                return false;
            }
            return true;
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 55;
            Item.useTime = 28;
            Item.useAnimation = (int)(Item.useTime);
            Item.knockBack = 0;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.UseSound = SoundID.Item1;
            LuxCost = 10;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 55;
            Item.useTime = 14;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 5;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<HallowsEveIllusion1>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            VisCost = 8;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.damage = 55;
            Item.useTime = 30;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<HallowsEveConjuration1>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            MundusCost = 15;
        }

       


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpookyWood, 40);
            recipe.AddIngredient(ItemID.Pumpkin, 12);
            recipe.AddIngredient(ItemID.Ectoplasm, 8);
            recipe.AddIngredient(Mod, nameof(SoulOfHaught), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}