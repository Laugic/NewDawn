using NewDawn.Items.Loot;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class CoreOfMysticism : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Core of Mysticism");
            // Tooltip.SetDefault("+15% Mystic Damage\n+50 Potentia");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.value = 100;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.MysticDamage += .15f;
            mysticPlayer.MundusMax += 50;
            mysticPlayer.LuxMax += 50;
            mysticPlayer.VisMax += 50;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DestructionCore>(), 1);
            recipe.AddIngredient(ModContent.ItemType<IllusionCore>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ConjurationCore>(), 1);
            recipe.AddIngredient(Mod, nameof(SoulOfWrought), 8);
            recipe.AddTile(null, nameof(MineralEnchanterTile));
            recipe.Register();
        }
    }
}