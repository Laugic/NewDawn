using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;

namespace NewDawn.UI.NewAttempts.Mystic
{
    public class UIBar
    {
        public Vector2 Position;
        private UIImage BGImage, BarImage, FGImage;
        //private const int BAR_WIDTH = 96;
        public UIBar(UIImage BGImage, UIImage BarImage, UIImage FGImage)
        {
            this.BGImage = BGImage;
            this.BarImage = BarImage;
            this.FGImage = FGImage;
            this.Position = new Vector2(0, 0);
        }

        public UIBar(UIImage BGImage, UIImage BarImage, UIImage FGImage, Vector2 Position)
        {
            this.BGImage = BGImage;
            this.BarImage = BarImage;
            this.FGImage = FGImage;
            this.Position = Position;
        }

        public void Initialize()
        {
            BGImage.Left.Set(Position.X, 0f);
            BGImage.Top.Set(Position.Y, 0f);
            BGImage.Width.Set(BGImage.Width.Pixels, 0f);
            BGImage.Height.Set(BGImage.Height.Pixels, 0f);
            BarImage.Left.Set(Position.X, 0f);
            BarImage.Top.Set(Position.Y, 0f);
            BarImage.Width.Set(BarImage.Width.Pixels, 0f);
            BarImage.Height.Set(BarImage.Height.Pixels, 0f);
            FGImage.Left.Set(Position.X, 0f);
            FGImage.Top.Set(Position.Y, 0f);
            FGImage.Width.Set(FGImage.Width.Pixels, 0f);
            FGImage.Height.Set(FGImage.Height.Pixels, 0f);
        }

        public void Update(float barWidth)
        {
            BGImage.Left.Set(Position.X, 0f);
            BGImage.Top.Set(Position.Y, 0f);
            BarImage.Left.Set(Position.X, 0f);
            BarImage.Top.Set(Position.Y, 0f);
            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("barWidth value: " + barWidth), Color.White);
            BarImage.Width.Set(barWidth, 0f);
            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("BarImage.Width.Percent after update: " + BarImage.Width.Pixels), Color.White);
            FGImage.Left.Set(Position.X, 0f);
            FGImage.Top.Set(Position.Y, 0f);
        }

        public void UpdateImages(UIImage BGImage, UIImage BarImage, UIImage FGImage)
        {
            BGImage = this.BGImage;
            BarImage = this.BarImage;
            FGImage = this.FGImage;
        }
    }
}
