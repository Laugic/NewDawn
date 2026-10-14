using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Abstracts;
using NewDawn.Temple;
using NewDawn.Temple.RoomGeneration;
using SubworldLibrary;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Temple.Consumables
{
    public class Keystone : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
}

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.CloneDefaults(ItemID.FragmentVortex);
            Item.width = 32;
            Item.height = 32;
            Item.useTime = Item.useAnimation = 20;
            Item.maxStack = 16;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item29;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Lighting.AddLight(Item.Center, new Vector3(0.4f, 0.8f, 0.9f) * Main.essScale);
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }

        public override void UpdateInventory(Player player)
        {
            //Lighting.AddLight(base.Center, new Vector3(0.2f, 0.4f, 0.5f) * Main.essScale);
            base.UpdateInventory(player);
            if(SubworldSystem.Current is not TempleWorld)
                Item.stack= 0;
        }
    }
}