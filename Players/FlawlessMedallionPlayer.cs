using Terraria.ModLoader;

namespace NewDawn.Players
{
    public sealed class FlawlessMedallionPlayer : ModPlayer
    {
        public string StoredSetBonusText { get; set; } = string.Empty;

        public override void ResetEffects()
        {
            StoredSetBonusText = string.Empty;
        }
    }
}
