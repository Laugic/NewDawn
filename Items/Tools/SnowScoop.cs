using Microsoft.Xna.Framework;
using NewDawn.Items.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Tools
{
    public class SnowScoop : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.pick = 25;
            Item.useAnimation = Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void HoldItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                int x;
                int y;
                if(Main.SmartCursorShowing)
                {
                    x = Main.SmartCursorX;
                    y = Main.SmartCursorY;
                }
                else
                {
                    x = (int)Main.MouseWorld.X / 16;
                    y = (int)Main.MouseWorld.Y / 16;
                }
                
                if (Main.myPlayer == Main.LocalPlayer.whoAmI && Main.myPlayer == player.whoAmI && Main.tile[x, y] != null && Vector2.Distance(new Vector2(x, y), new Vector2(player.position.X / 16, player.position.Y / 16)) <= player.lastTileRangeX + .25)
                {
                    if(Main.tile[x, y].TileType == TileID.SnowBlock)
                        Item.NewItem(player.GetSource_FromAI(), new Vector2(x * 16 + 4, y * 16), ItemID.Snowball, Main.rand.Next(1, 3));
                    if (Main.tile[x, y].TileType == TileID.Ash)
                        Item.NewItem(player.GetSource_FromAI(), new Vector2(x * 16 + 4, y * 16), ModContent.ItemType<Sootball>(), Main.rand.Next(1, 3));
                    if (Main.tile[x, y].TileType == TileID.Pearlsand)
                        Item.NewItem(player.GetSource_FromAI(), new Vector2(x * 16 + 4, y * 16), ModContent.ItemType<Pearlsandball>(), Main.rand.Next(1, 3));
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
