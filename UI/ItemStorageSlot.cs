using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using NewDawn.Items;
using System.Linq;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.ID;
using static Terraria.ModLoader.PlayerDrawLayer;
using NewDawn.Players;
using Terraria.Audio;
using NewDawn.Items.Alchemistry;
using NewDawn.Abstracts;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Terraria.UI.Chat;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using System.Collections;

namespace NewDawn.UI
{
    //UI Element that can display stored items. Depends on AbstractStorage and UIPlayer
    public class ItemStorageSlot : UIElement
    {
        private static UIImage SlotImage;
        internal int Index;
        internal Item DummyItem = new();

        public ItemStorageSlot(int index)
        {
            SlotImage = new UIImage(ModContent.Request<Texture2D>("NewDawn/UI/Slot"));
            Append(SlotImage);
            Index = index;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("NewDawn/UI/Slot");

            spriteBatch.Draw(texture, GetDimensions().Position(), texture.Frame(), Color.White * .8f, 0, Vector2.Zero, 1, 0, 0);

            Item item = GetItem();
            if (item != null && item.type > ItemID.None)
            {
                Main.instance.LoadItem(GetItem().type);

                //From Starlight River, thank you!
                Texture2D tex2 = item.type > ItemID.Count ? ModContent.Request<Texture2D>(item.ModItem.Texture).Value : Terraria.GameContent.TextureAssets.Item[item.type].Value;
                float scale = tex2.Frame().Size().Length() < 47 ? 1 : 47f / tex2.Frame().Size().Length();

                spriteBatch.Draw(tex2, GetDimensions().Center(), tex2.Frame(), Color.White, 0, tex2.Frame().Size() / 2, scale, 0, 0);

                Vector2 StackPosition = new Vector2(8, 24);
                if (item.stack > 1)
                    Utils.DrawBorderString(spriteBatch, item.stack.ToString(), GetDimensions().Position() + StackPosition, Color.White, .9f);


                //Get Tooltip from item. Basically taken from vanilla
                if (IsMouseHovering)
                {
                    Item hoverItem = item;
                    int yoyoLogo = -1;
                    int researchLine = -1;
                    int rare = hoverItem.rare;
                    float knockBack = hoverItem.knockBack;
                    float kbMod = 1f;
                    if (hoverItem.DamageType == DamageClass.Melee && Main.LocalPlayer.kbGlove)
                        kbMod += 1f;

                    if (Main.LocalPlayer.kbBuff)
                        kbMod += 0.5f;

                    if (kbMod != 1f)
                        hoverItem.knockBack *= kbMod;

                    if (hoverItem.DamageType == DamageClass.Ranged && Main.LocalPlayer.shroomiteStealth)
                        hoverItem.knockBack *= 1f + (1f - Main.LocalPlayer.stealth) * 0.5f;

                    int maxVal = 30;
                    int numLines = 1;
                    string[] text = new string[maxVal];
                    bool[] modifier = new bool[maxVal];
                    bool[] badModifier = new bool[maxVal];
                    for (int j = 0; j < maxVal; j++)
                    {
                        modifier[j] = false;
                        badModifier[j] = false;
                    }

                    string[] array = new string[maxVal];
                    int pref = 0;
                    Main.MouseText_DrawItemTooltip_GetLinesInfo(hoverItem, ref yoyoLogo, ref researchLine, knockback, ref numLines, text, modifier, badModifier, array, out pref);
                    float num3 = (float)(int)Main.mouseTextColor / 255f;
                    float num4 = num3;
                    int alpha = Main.mouseTextColor;

                    Vector2 zero = Vector2.Zero;
                    Color?[] overrideColor;
                    List<TooltipLine> list = ItemLoader.ModifyTooltips(hoverItem, ref numLines, array, ref text, ref modifier, ref badModifier, ref yoyoLogo, out overrideColor, 0);
                    int yOffset = 0;
                    for (int k = 0; k < numLines; k++)
                    {
                        Vector2 stringSize = ChatManager.GetStringSize(FontAssets.MouseText.Value, text[k], Vector2.One);
                        if (stringSize.X > zero.X)
                        {
                            zero.X = stringSize.X;
                        }

                        zero.Y += stringSize.Y + (float)yOffset;
                    }

                    if (yoyoLogo != -1)
                    {
                        zero.Y += 24f;
                    }

                    int X = Main.mouseX + 14;
                    int Y = Main.mouseY + 14;

                    if (Main.ThickMouse)
                    {
                        X += 6;
                        Y += 6;
                    }

                    if (!Main.mouseItem.IsAir)
                    {
                        X += 34;
                    }
                    X += 6;
                    Y += 6;
                    int num12 = 4;
                    if (Main.SettingsEnabled_OpaqueBoxBehindTooltips)
                    {
                        X += 8;
                        Y += 2;
                        num12 = 18;
                    }

                    int num13 = Main.screenWidth;
                    int num14 = Main.screenHeight;
                    if ((float)X + zero.X + (float)num12 > (float)num13)
                    {
                        X = (int)((float)num13 - zero.X - (float)num12);
                    }

                    if ((float)Y + zero.Y + (float)num12 > (float)num14)
                    {
                        Y = (int)((float)num14 - zero.Y - (float)num12);
                    }

                    num3 = (float)(int)Main.mouseTextColor / 255f;
                    if (Main.SettingsEnabled_OpaqueBoxBehindTooltips)
                    {
                        num3 = MathHelper.Lerp(num3, 1f, 1f);
                        int num16 = 14;
                        int num17 = 9;
                        Utils.DrawInvBG(spriteBatch, new Rectangle(X - num16, Y - num17, (int)zero.X + num16 * 2, (int)zero.Y + num17 + num17 / 2), new Color(23, 25, 81, 255) * 0.925f);
                    }

                    bool flag = ItemLoader.PreDrawTooltip(hoverItem, list.AsReadOnly(), ref X, ref Y);
                    List<DrawableTooltipLine> list2 = new List<DrawableTooltipLine>();
                    int yOff = 0;
                    for (int l = 0; l < numLines; l++)
                    {
                        Color rarityColor = Color.Black;
                        rarityColor = new Color(num4, num4, num4, num4);

                        if (rare == -11)
                            rarityColor = new Color((byte)(255f * num4), (byte)(175f * num4), (byte)(0f * num4), alpha);

                        if (rare == -1)
                            rarityColor = new Color((byte)(130f * num4), (byte)(130f * num4), (byte)(130f * num4), alpha);

                        if (rare == 1)
                        rarityColor = new Color((byte)(150f * num4), (byte)(150f * num4), (byte)(255f * num4), alpha);

                        if (rare == 2)
                        rarityColor = new Color((byte)(150f * num4), (byte)(255f * num4), (byte)(150f * num4), alpha);

                        if (rare == 3)
                        rarityColor = new Color((byte)(255f * num4), (byte)(200f * num4), (byte)(150f * num4), alpha);

                        if (rare == 4)
                        rarityColor = new Color((byte)(255f * num4), (byte)(150f * num4), (byte)(150f * num4), alpha);

                        if (rare == 5)
                        rarityColor = new Color((byte)(255f * num4), (byte)(150f * num4), (byte)(255f * num4), alpha);

                        if (rare == 6)
                        rarityColor = new Color((byte)(210f * num4), (byte)(160f * num4), (byte)(255f * num4), alpha);

                        if (rare == 7)
                        rarityColor = new Color((byte)(150f * num4), (byte)(255f * num4), (byte)(10f * num4), alpha);

                        if (rare == 8)
                        rarityColor = new Color((byte)(255f * num4), (byte)(255f * num4), (byte)(10f * num4), alpha);

                        if (rare == 9)
                        rarityColor = new Color((byte)(5f * num4), (byte)(200f * num4), (byte)(255f * num4), alpha);

                        if (rare == 10)
                        rarityColor = new Color((byte)(255f * num4), (byte)(40f * num4), (byte)(100f * num4), alpha);

                        if (rare == 11)
                        rarityColor = new Color((byte)(180f * num4), (byte)(40f * num4), (byte)(255f * num4), alpha);

                        if (rare > 11)
                            rarityColor = RarityLoader.GetRarity(rare).RarityColor * num4;

                        if (hoverItem.expert || rare == -12)
                        rarityColor = new Color((byte)((float)Main.DiscoR * num4), (byte)((float)Main.DiscoG * num4), (byte)((float)Main.DiscoB * num4), alpha);

                        if (hoverItem.master || rare == -13)
                        rarityColor = new Color((byte)(255f * num4), (byte)(Main.masterColor * 200f * num4), 0, alpha);

                        Color drawColor = Color.White;
                        if (l == 0)
                            drawColor = rarityColor;
                        else if (list[l].OverrideColor.HasValue)
                            drawColor = list[l].OverrideColor.Value;

                        list2.Add(new DrawableTooltipLine(list[l], l, X, Y + yOff, drawColor));
                        if (ItemLoader.PreDrawTooltipLine(hoverItem, list2[l], ref yOffset) && flag)
                        {
                            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, list2[l].Font, list2[l].Text, new Vector2(list2[l].X, list2[l].Y), drawColor, list2[l].Rotation, list2[l].Origin, list2[l].BaseScale, list2[l].MaxWidth, list2[l].Spread);
                        }
                        yOff += (int)(FontAssets.MouseText.Value.MeasureString(list[l].Text).Y + (float)yOffset);

                        ItemLoader.PostDrawTooltipLine(hoverItem, list2[l]);
                    }
                    ItemLoader.PostDrawTooltip(hoverItem, list2.AsReadOnly());
                }
            }

        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            if (IsMouseHovering && Main.LocalPlayer != null  && Main.LocalPlayer.whoAmI == Main.myPlayer && CheckSpecialFilters())
            {
                if(MaxStackOne())
                {
                    if(GetItem().stack == 0)
                    {
                        if(Main.mouseItem.stack == 1)
                        {
                            Utils.Swap(ref GetItem(), ref Main.mouseItem);

                            if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                                SoundEngine.PlaySound(SoundID.Grab, Main.LocalPlayer.position);
                        }
                        else if (Main.mouseItem.stack > 0)
                        {
                            Item droppedItem = Main.mouseItem.Clone();
                            droppedItem.stack = 1;
                            Utils.Swap(ref GetItem(), ref droppedItem);
                            Main.mouseItem.stack--;

                            if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                                SoundEngine.PlaySound(SoundID.Grab, Main.LocalPlayer.position);
                        }
                    }
                    else
                    {
                        if(Main.mouseItem.stack <= 1)
                        {
                            Utils.Swap(ref GetItem(), ref Main.mouseItem);
                            if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                                SoundEngine.PlaySound(SoundID.Grab, Main.LocalPlayer.position);
                        }
                    }
                }
                else if (Main.mouseItem.type == GetItem().type)
                {
                    if (Main.mouseItem.stack + GetItem().stack <= Main.mouseItem.maxStack)
                    {
                        GetItem().stack += Main.mouseItem.stack;
                        Main.mouseItem = new Item();
                    }
                    else if (Main.mouseItem.stack < Main.mouseItem.maxStack && GetItem().stack < GetItem().maxStack)
                    {
                        int diff = GetItem().maxStack - GetItem().stack;
                        Main.mouseItem.stack -= diff;
                        if (Main.mouseItem.stack <= 0)
                            Main.mouseItem = new Item();
                        GetItem().stack = GetItem().maxStack;
                    }
                    else
                        Utils.Swap(ref GetItem(), ref Main.mouseItem);

                    if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                        SoundEngine.PlaySound(SoundID.Grab, Main.LocalPlayer.position);
                }
                else
                {
                    Utils.Swap(ref GetItem(), ref Main.mouseItem);
                    if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                        SoundEngine.PlaySound(SoundID.Grab, Main.LocalPlayer.position);
                }
            }
        }

        public bool CheckSpecialFilters()
        {
            var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
            if (uiplayer != null && uiplayer.LinkedStorage != null && Index < uiplayer.LinkedStorage.Items.Length && Main.mouseItem.ModItem is not AbstractStorage && !(Main.mouseItem.type == ItemID.None && ItemID.None == uiplayer.LinkedStorage.Items[Index].type))
                return uiplayer.LinkedStorage.FilterCheck(Main.mouseItem, Index) || Main.mouseItem.type == ItemID.None;
            return false;
        }

        public bool MaxStackOne()
        {
            var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
            if (uiplayer != null && uiplayer.LinkedStorage != null && Index < uiplayer.LinkedStorage.Items.Length && Main.mouseItem.ModItem is not AbstractStorage && !(Main.mouseItem.type == ItemID.None && ItemID.None == uiplayer.LinkedStorage.Items[Index].type))
                return uiplayer.LinkedStorage.MaxStackOne();

            return false;
        }

        public ref Item GetItem()
        {
            var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
            if (uiplayer != null && uiplayer.LinkedStorage != null && Index < uiplayer.LinkedStorage.Items.Length)
                return ref uiplayer.LinkedStorage.Items[Index];
            return ref DummyItem;
        }
    }
}
