using Microsoft.Xna.Framework;
using NewDawn.Dusts;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Useables
{
    public class BysmalKey : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            //TODO: Add sound effect for this item
            //SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Sound, "Sounds/EtherialChange"));
            NewDawnPlayer.Get(player).BysmalAbsorbDisabled = !NewDawnPlayer.Get(player).BysmalAbsorbDisabled;
            if (NewDawnPlayer.Get(player).BysmalAbsorbDisabled)
                Main.NewText("Your Bysmal Armor has been locked, and won't absorb any more bonuses.", 0, 100, 150);
            else
                Main.NewText("Your Bysmal Armor has been unlocked, and will absorb bonuses again.", 0, 100, 150);
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 4);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 2);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}