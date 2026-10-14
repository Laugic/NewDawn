using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewDawn.Items.Weapons.Mystic
{
    public class PlutosFrost : MysticItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
}

        public override void SetMysticDefaults()
        {
            Item.damage = 60;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
            LuxCost = 10;
            VisCost = 10;
            MundusCost = 10;
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if ((NewDawnWorld.downedEtheria || NewDawnPlayer.Get(player).Etherable > 0) && NewDawnWorld.downedTrueEtheria)
            {
                mysticPlayer.MysticDamage += .25f;
                mysticPlayer.GlobalAbsorbRate *= 1.5f;
                mysticPlayer.GlobalOverflow += .5f;
            }
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Illusion)
            {
                int numberProjectiles = Main.rand.Next(1, 3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    if (Main.player[Main.myPlayer] == player)
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<PlutoIllusion>(), damage, knockBack, player.whoAmI);
                }

            }
            return true;
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 100;
            Item.useTime = 16;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 6;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<PlutoDestruction>();
            LuxCost = 5;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 60;
            Item.useTime = 10;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 5;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<PlutoIllusion>();
            Item.noUseGraphic = false;
            VisCost = 6;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 54;
            Item.useTime = 24;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 24f;
            Item.shoot = ModContent.ProjectileType<PlutoConjuration>();
            Item.noUseGraphic = false;
            MundusCost = 16;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Yuletide>(), 1);
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 12);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 15);
            recipe.AddIngredient(Mod, nameof(SoulOfSought), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}