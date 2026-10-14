using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class BysmalBoots : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.Etherable > 0 || NewDawnWorld.downedEtheria)
            {
                player.moveSpeed += 0.25f;
                player.maxRunSpeed += 1f;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 12);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}