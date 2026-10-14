using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Weapons.Mystic
{
    public class FreyasFley : MysticItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
}

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (Main.LocalPlayer.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    return "Rapidly shoots shrooms";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Shoots a spore cloud that inflict 'Spored', which\nmakes Shrooms become Mega Shrooms on hit";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Spawns a shroom cap that launches spores into the sky";
                default:
                    return "";
            }
        }


        public override void SetMysticDefaults()
        {
            Item.damage = 11;
            Item.width = 52;
            Item.height = 50;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.shootSpeed = 6f;
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 10;
            Item.useTime = 9;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 1;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<FreyaDestruction>();
            LuxCost = 3;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 10;
            Item.damage = (int)(Item.damage * modPlayer.IllusionDamage);
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.knockBack = 1;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<FreyaIllusion>();
            VisCost = 10;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 12;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.knockBack = 5;
            Item.shootSpeed = 2f;
            Item.shoot = ModContent.ProjectileType<FreyaConjuration1>();
            MundusCost = 14;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(183, 16);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}