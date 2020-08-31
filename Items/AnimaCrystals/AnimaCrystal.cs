using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.AnimaCrystals
{
    public abstract class AnimaCrystal : ModItem
    {
        public virtual void SetCrystalDefaults()
        {

        }
        public sealed override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = item.useAnimation = 30;
            item.accessory = true;
            item.rare = ItemRarityID.Blue;
            SetCrystalDefaults();
        }

        public void AddEssence(Essence essence)
        {
            if (Essences.Count < EssenceCapacity && essence.Level < MaxEssenceLevel)
                Essences.Add(essence);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "ListEssences", GetEssenceTooltips()));
        }

        private string GetEssenceTooltips()
        {
            string tip = "";
            int numEssences = Essences.Count;

            if (numEssences < EssenceCapacity)
                tip = "Can hold " + (EssenceCapacity - numEssences) + " more Essences";

            if (numEssences > 0)
            {
                foreach (Essence essence in Essences)
                    tip += "\n" + essence.EffectDescription;
            }

            return tip;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Essences.Count > 0)
            {
                foreach (Essence essence in Essences)
                {
                    essence.Effect(player);
                }
            }
        }

        public abstract int EssenceCapacity { get; }
        public List<Essence> Essences { get; set; } = new List<Essence>();
        public abstract Essence.Levels MaxEssenceLevel { get; }
    }
}
