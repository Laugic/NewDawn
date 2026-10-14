using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class PrismVeil : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+20% Mystic Burst Damage\nDecreased Mystic Burst Cooldown\nIncreased Movement Speed while Mystic Burst is on Cooldown\nBecome immune for a time after Mystic Bursts");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100;
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.MysticBurstDamage += .2f;
            mysticPlayer.MysticSwitchCoolRate += 1;
            if(mysticPlayer.MysticSwitchCool > 0)
            {
                player.moveSpeed += 1f;
                player.maxRunSpeed += 2f;
            }
            modPlayer.PrismVeil = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrystalShard, 12);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}