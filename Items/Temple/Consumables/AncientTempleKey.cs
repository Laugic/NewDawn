using Microsoft.Xna.Framework;
using NewDawn.Abstracts;
using NewDawn.Temple;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Temple.Consumables
{
    public class AncientTempleKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Allows you to access the ancient temple");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 48;
            Item.height = 24;
            Item.useTime = Item.useAnimation = 20;
            Item.maxStack = 16;
            Item.useStyle = ItemUseStyleID.HoldUp;
            //Item.consumable = true;
            Item.consumable = false;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Unlock;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
        }

        public override bool? UseItem(Player player)
        {
            TempleMain.EnterFloor();
            return true;
        }
    }
}