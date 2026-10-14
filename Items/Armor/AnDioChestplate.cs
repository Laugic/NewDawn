using NewDawn.Control;
using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AnDioChestplate : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 16;
        }

        public override void UpdateEquip(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.zImmune = true;
            modPlayer.zaWarudoDuration += 2 * 60;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<DioritusHelmet>() && legs.type == ModContent.ItemType<AndesiaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Deal increased damage while Time is Stopped";

            if (NewDawn.zaWarudo > 0)
                player.GetDamage(DamageClass.Generic) += .25f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
            recipe.AddIngredient(3086, 32);
            recipe.AddRecipeGroup(RecipeManager.TitaniumBars, 24);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}