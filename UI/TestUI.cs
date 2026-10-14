
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using NewDawn.Players;
//using NewDawn.UI;
//using ReLogic.Content;
//using System;
//using System.Collections.Generic;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.UI;

//public class TestUI : ModSystem
//{
//    private Asset<Texture2D> overlayTexture;

//    public static Asset<Texture2D> LuxBGTexture;
//    public static Asset<Texture2D> LuxBarTexture;
//    public static Asset<Texture2D> LuxFGTexture;
//    public static Asset<Texture2D> MundusBGTexture;
//    public static Asset<Texture2D> MundusBarTexture;
//    public static Asset<Texture2D> MundusFGTexture;
//    public static Asset<Texture2D> VisBGTexture;
//    public static Asset<Texture2D> VisBarTexture;
//    public static Asset<Texture2D> VisFGTexture;
//    public static Asset<Texture2D> OverflowBGTexture;
//    public static Asset<Texture2D> OverflowBarTexture;
//    public static Asset<Texture2D> OverflowFGTexture;
//    public static Asset<Texture2D> LuxFGOverflowTexture;
//    public static Asset<Texture2D> VisFGOverflowTexture;
//    public static Asset<Texture2D> MundusOverflowTexture;
//    public static Asset<Texture2D> MundusFGOverflowTexture;

//    public override void Load()
//    {
//        if (!Main.dedServ)
//        {
//            overlayTexture = ModContent.Request<Texture2D>(
//                "NewDawn/UI/Test"
//            );

//            LuxBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/LuxBG");
//            LuxBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/LuxBar");
//            LuxFGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/LuxFG");
//            MundusBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusBG");
//            MundusBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusBar");
//            MundusFGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusFG");
//            VisBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/VisBG");
//            VisBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/VisBar");
//            VisFGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/VisFG");
//            OverflowBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/OverflowBG");
//            OverflowBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/OverflowBar");
//            OverflowFGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/OverflowFG");
//            LuxFGOverflowTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/LuxFGOverflow");
//            VisFGOverflowTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/VisFGOverflow");
//            MundusOverflowTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusOverflow");
//            MundusFGOverflowTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusFGOverflow");
//        }
//    }

//    public override void ModifyInterfaceLayers(
//        List<GameInterfaceLayer> layers)
//    {
//        int index = layers.FindIndex(
//            layer => layer.Name == "Vanilla: Mouse Text"
//        );

//        if (index == -1)
//            return;

//        layers.Insert(index, new LegacyGameInterfaceLayer(
//            "NewDawn: Held Item Overlay",
//            DrawOverlay,
//            InterfaceScaleType.UI
//        ));
//    }

//    private bool DrawOverlay()
//    {
//        Player player = Main.LocalPlayer;
//        MysticPlayer mysticPlayer = player.GetModPlayer<MysticPlayer>();
//        // Only draw while holding the specified item.
//        if (!mysticPlayer.IsHoldingMystic())
//            return true;

//        NewDawnUI.Draw(Main.spriteBatch);

//        Texture2D texture = LuxBGTexture.Value;

//        Vector2 position = NewDawnUI.TopPosBase;
//        Vector2 origin = texture.Size() / 2f;
//        float tempLux = Math.Min(1, mysticPlayer.Lux / mysticPlayer.LuxMax);
//        Main.spriteBatch.Draw(LuxBGTexture.Value, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
//        Main.spriteBatch.Draw(LuxBarTexture.Value, position, new Rectangle(0, 0, (int)(LuxBarTexture.Value.Width * tempLux), LuxBarTexture.Value.Height), Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
//        Main.spriteBatch.Draw(LuxFGTexture.Value, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

//        return true;
//    }
//}
