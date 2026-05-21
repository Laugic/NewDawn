using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Abstracts;
using NewDawn.Temple;
using SubworldLibrary;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Temple.Consumables
{
    public class GoldenTempleKey : ModItem
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 32;
            Item.useTime = Item.useAnimation = 20;
            Item.maxStack = 16;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item29;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
        }
    }
}