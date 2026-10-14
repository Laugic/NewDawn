using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class RuneOfTheAncients : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 36;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (mysticPlayer.IsOnOverflow())
            {
                mysticPlayer.OverflowDamage += .15f;
                mysticPlayer.OverflowVelocity += .15f;
                player.moveSpeed += .15f;
                player.jumpSpeedBoost += 3;
                player.maxRunSpeed += 2;
                player.accRunSpeed += 2;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(169, 24);
            recipe.AddIngredient(607, 16);
            recipe.AddIngredient(ModContent.ItemType<AncientShard>(), 1);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}