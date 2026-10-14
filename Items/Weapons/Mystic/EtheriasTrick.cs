using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Mystic
{
	public class EtheriasTrick : MysticItem
    {

		public override void SetMysticDefaults()
		{
			Item.damage = 90;
            Item.width = 44;
			Item.height = 60;
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
                    return "Shoot a homing rocket bolt";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Shoots a giant lazer that inflicts 'Eeriness', which\ncauses enemies to take more damage the longer they go without taking damage";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Spawns energy orbs that create stalagmites and stalactites.";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            if(player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Illusion)
            {
                SoundEngine.PlaySound(NewDawn.CreateSoundStyle("Sounds/Etherlaser"));
            }

            return base.MysticShoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 250;
            Item.useAnimation = Item.useTime = 60;
            Item.knockBack = 8;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<EtherialRocket>();
            Item.UseSound = SoundID.Item20;
            Item.scale = 1f;
            LuxCost = 20;
            Item.channel = false;
            Item.autoReuse = true;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 26;
            Item.useAnimation = Item.useTime = 2 * 60;
            Item.knockBack = 5;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<EtheriaIllusion>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item20;
            Item.scale = 1f;
            VisCost = 50;
            Item.channel = true;
            Item.autoReuse = false;
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
            Item.channel = false;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 16);
            recipe.AddIngredient(ModContent.ItemType<EtherialEssence>(), 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}