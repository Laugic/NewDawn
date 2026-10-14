//using NewDawn.Players;
//using NewDawn.Projectiles.Mystic.Fungon;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using static NewDawn.Players.MysticPlayer;

//namespace NewDawn.Items.Weapons.Mystic
//{
//    public class FungonsWand : MysticItem
//    {
//        public override void SetStaticDefaults()
//        {
//            Item.staff[Item.type] = true;
//            Item.useStyle = ItemUseStyleID.Shoot;
//        }

//        public override string GetExtraTooltip()
//        {
//            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();

//            switch (mysticPlayer.CurrentMysticMode)
//            {
//                case MysticMode.Destruction:
//                    return "Rapidly shoots shrooms";
//                case MysticMode.Illusion:
//                    return "Shoots a spore cloud that inflict 'Spored', which\nmakes Spores become Mega Spores on hit, and\nmakes enemies occasionally release Spores.";
//                case MysticMode.Conjuration:
//                    return "Spawns a shroom cap that launches spores into the sky";
//                default:
//                    return "";
//            }
//        }


//        public override void SetMysticDefaults()
//        {
//            Item.damage = 11;
//            Item.width = 52;
//            Item.height = 50;
//            Item.useTime = 18;
//            Item.useAnimation = 18;
//            Item.useStyle = ItemUseStyleID.Shoot;
//            Item.noMelee = true;
//            Item.knockBack = 2;
//            Item.value = 10000;
//            Item.rare = ItemRarityID.Blue;
//            Item.UseSound = SoundID.Item20;
//            Item.autoReuse = true;
//            Item.shoot = ModContent.ProjectileType<ShroomBurst>();
//            Item.shootSpeed = 6f;
//        }

//        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
//        {
//            Item.damage = 10;
//            Item.useTime = 9;
//            Item.useAnimation = Item.useTime;
//            Item.knockBack = 1;
//            Item.shootSpeed = 10f;
//            Item.shoot = ModContent.ProjectileType<FungonDestruction>();
//            LuxCost = 3;
//        }

//        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
//        {
//            Item.damage = 10;
//            Item.damage = (int)(Item.damage * modPlayer.IllusionDamage);
//            Item.useTime = 35;
//            Item.useAnimation = 35;
//            Item.knockBack = 1;
//            Item.shootSpeed = 12f;
//            Item.shoot = ModContent.ProjectileType<FungonIllusion>();
//            VisCost = 10;
//        }

//        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
//        {
//            Item.damage = 12;
//            Item.useTime = 45;
//            Item.useAnimation = 45;
//            Item.knockBack = 5;
//            Item.shootSpeed = 2f;
//            Item.shoot = ModContent.ProjectileType<FungonConjuration1>();
//            MundusCost = 14;
//        }

//        public override void AddRecipes()
//        {
//            Recipe recipe = CreateRecipe();
//            recipe.AddIngredient(ItemID.GlowingMushroom, 16);
//            recipe.AddIngredient(ItemID.ManaCrystal, 1);
//            recipe.AddTile(TileID.Anvils);
//            recipe.Register();
//        }
//    }
//}