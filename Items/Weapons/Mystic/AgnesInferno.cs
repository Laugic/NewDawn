using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Mystic
{
    public class AgnesInferno : MysticItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Agnes' Inferno");
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 32;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<AgnesDestruction>();
            Item.scale = 1.5f;
        }


        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "Shoots fireballs";
                case 2:
                    return "Shoots orbiting fireballs that inflict 'Infernal', which \nmakes enemies take damage over time based on their defense";
                case 3:
                    return "Shoots Javelins that spawn a stream of fireballs";
                default:
                    return "";
            }
        }

        public override bool CanUseItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
                return player.ownedProjectileCounts[Item.shoot] < 1;
            if (modPlayer.MysticMode == 2)
                return player.ownedProjectileCounts[Item.shoot] < 1;
            return true;
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 32;
            Item.useAnimation = Item.useTime = 30;
            Item.knockBack = 8;
            Item.shootSpeed = 8f;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.shoot = ModContent.ProjectileType<AgnesDestruction>();
            LuxCost = 8;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 28;
            Item.useAnimation = Item.useTime = 21;
            Item.knockBack = 8;
            Item.shootSpeed = 8f;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.shoot = ModContent.ProjectileType<AgnesIllusion>();
            VisCost = 10;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.damage = 24;
            Item.useAnimation = Item.useTime = 60;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.shoot = ModContent.ProjectileType<AgnesConjuration>();
            MundusCost = 25;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(Mod, nameof(ObsidiumBar), 12);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}