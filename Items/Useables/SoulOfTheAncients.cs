using Microsoft.Xna.Framework;
using NewDawn.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Useables
{
    public class SoulOfTheAncients : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            //TODO: Add sound effect for this item
            //SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Sound, "Sounds/SoulStore"));
            player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().MysticBurstDisabled = !player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().MysticBurstDisabled;
            //NewDawnWorld.downedNecrodon = !NewDawnWorld.downedNecrodon;
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().MysticBurstDisabled)
                Main.NewText("You stored part of your Soul in the Vase.", 150, 100, 0);
            else
                Main.NewText("Your soul has been released.", 150, 100, 0);
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<Sandy>(), 0f, 0f);
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AncientShard>(), 2);
            recipe.AddIngredient(ModContent.ItemType<Crystilla>(), 8);
            recipe.AddIngredient(ItemID.SandBlock, 40);
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}