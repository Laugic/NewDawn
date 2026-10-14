using Terraria.ModLoader;

namespace NewDawn.Control
{
    internal class TimeStop : ModSystem
    {
        public override void PostUpdateWorld()
        {
            if (NewDawn.zaWarudo > 0)
            {
                NewDawn.zaWarudo--;
            }
        }
    }
}
