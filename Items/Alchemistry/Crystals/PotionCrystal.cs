using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public abstract class PotionCrystal : ModItem
    {
        public static Condition Demystify = new Condition("Right Click with Fizzle", () => false);
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.maxStack = 99;

            Item.useTime = Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.autoReuse = true;
            Item.hasVanityEffects = true;
            Item.prefix = 0;

            CustomDefaults();
        }

        public override bool CanReforge()
        {
            if (Item.stack > 1)
                return false;
            return true;
        }

        public override void PostReforge()
        {
            if (Item.stack > 1)
                Item.prefix = 0;

            if (Item.prefix != 0)
                Item.maxStack = 1;
            else
                Item.maxStack = 99;
        }

        /*
        public override bool AllowPrefix(int pre)
        {
            return false;
        }*/


        public abstract void CustomDefaults();
    }
}
