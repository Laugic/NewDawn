using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AncientArmor : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            float moveSpeed = 0;
            moveSpeed = (float)player.velocity.Length() / 50f;
            if (moveSpeed > .25f)
                moveSpeed = .25f;
            player.GetDamage(DamageClass.Generic) += moveSpeed;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Crystilla>(), 4);
            recipe.AddIngredient(ModContent.ItemType<AncientShard>(), 1);
            recipe.AddIngredient(ItemID.DesertFossil, 16);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}