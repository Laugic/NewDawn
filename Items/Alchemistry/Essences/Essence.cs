using NewDawn.Abstracts;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Tiles.Alchemistry;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Essences
{
    public abstract class Essence : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 25);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 999;

            CustomDefaults();
        }

        public abstract string GetTipAtIndex(AbstractStorageItem item, int index);
        public abstract void GetEffectAtIndex(Player player, AbstractStorageItem item, int index);

        public virtual void CustomDefaults() { }
    }
}
