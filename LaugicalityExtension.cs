using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn
{
    internal static class NewDawnExtension
    {
        public static void AddLine(this LocalizedText modTranslation, string line)
        {
            // modTranslation.SetDefault(modTranslation.GetDefault() + "\n" + line);
        }
    }
}
