using NewDawn.Items.Loot;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Items.Materials;

namespace NewDawn.Items.Weapons.Mystic
{
    public class JanusHunt : MysticItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Janus's Hunt");
            // Tooltip.SetDefault("'Stalking through the sands'");
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 20;
            Item.width = 44;
            Item.height = 74;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "Rapidly shoots crystilla shards";
                case 2:
                    return "Shoots a phase arrow that teleports you to it when it hits something\nCreate a burst of sandballs where you teleport to and from\nInflicts 'Sandy', which makes enemies take more damage based on the speed of what damages them";
                case 3:
                    return "Shoots an arrow that spawns a Sandnado upon hitting a tile";
                default:
                    return "";
            }
        }


        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                float scale = 1.25f - (Main.rand.NextFloat() * .5f);
                perturbedSpeed = perturbedSpeed * scale;
                speedX = perturbedSpeed.X;
                speedY = perturbedSpeed.Y;
            }
            return true;
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 15;
            Item.useAnimation = Item.useTime  = 10;
            Item.knockBack = 2;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<JanusDestruction>();
            LuxCost = 3;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 40;
            Item.useAnimation = Item.useTime = 120;
            Item.knockBack = 1;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<JanusIllusion>();
            VisCost = 50;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.damage = 35;
            Item.useAnimation = Item.useTime = 120;
            Item.knockBack = 2;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<JanusConjuration>();
            MundusCost = 33;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilOre, 8);
            recipe.AddIngredient(ModContent.ItemType<Crystilla>(), 8);
            recipe.AddIngredient(ModContent.ItemType<AncientShard>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}