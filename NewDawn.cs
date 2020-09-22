using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn
{
	public class NewDawn : Mod
    {
        public const string FAST_FOOTWEAR_GROUP = "NewDawnFastFootwear";
        public const string EVIL_BARS_GROUP = "NewDawnEvilBars";
        public const string EVIL_PART_GROUP = "NewDawnEvilPart";
        public const string DOUBLE_JUMP_GROUP = "NewDawnDoubleJump";
        public const string COLORED_BALLOON_GROUP = "NewDawnColoredBalloon";

        #region Recipe Groups
        public override void AddRecipeGroups()
        {
            //Gems
            RecipeGroup.RegisterGroup("Gems", new RecipeGroup(() => Lang.misc[37] + " Gem", new int[]
            {
                ItemID.Amethyst,
                ItemID.Topaz,
                ItemID.Ruby,
                ItemID.Sapphire,
                ItemID.Emerald,
                ItemID.Ruby,
                ItemID.Diamond,
                ItemID.Amber
            }));

            //Evil Bars
            RecipeGroup.RegisterGroup(EVIL_BARS_GROUP, new RecipeGroup(() => Lang.misc[37] + " Evil Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            }));

            //Evil Bars
            RecipeGroup.RegisterGroup(EVIL_PART_GROUP, new RecipeGroup(() => Lang.misc[37] + " Evil Part", new int[]
            {
                ItemID.ShadowScale,
                ItemID.TissueSample
            }));

            //DoubleJumps
            RecipeGroup.RegisterGroup(DOUBLE_JUMP_GROUP, new RecipeGroup(() => Lang.misc[37] + " Double Jump Bottle", new int[]
            {
                ItemID.CloudinaBottle,
                ItemID.SandstorminaBottle,
                ItemID.BlizzardinaBottle,
                ItemID.TsunamiInABottle
            }));

            //ColoredBalloons
            RecipeGroup.RegisterGroup(COLORED_BALLOON_GROUP, new RecipeGroup(() => Lang.misc[37] + " Colored Balloon", new int[]
            {
                ItemID.BlueHorseshoeBalloon,
                ItemID.WhiteHorseshoeBalloon,
                ItemID.YellowHorseshoeBalloon,
                ItemID.BalloonHorseshoeFart,
                ItemID.BalloonHorseshoeHoney,
                ItemID.BalloonHorseshoeSharkron,
            }));

            //Footwear
            RecipeGroup.RegisterGroup(FAST_FOOTWEAR_GROUP, new RecipeGroup(() => Lang.misc[37] + " Speed Boots", new int[]
            {
                ItemID.HermesBoots,
                ItemID.FlurryBoots,
                ItemID.SailfishBoots
            }));
        }
        #endregion
    }
}