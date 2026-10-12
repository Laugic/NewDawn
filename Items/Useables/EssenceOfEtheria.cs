using NewDawn.Dusts;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Useables
{
    public class EssenceOfEtheria : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The High Priestess");
            // Tooltip.SetDefault("Allows you to enter and leave the Etherial at will, as long as no powerful creatures are present to stop you. \nCan place shadows of itself, which can be wired to enter the Etherial. \nAn odd number of wires need to be connected, or it will immediately switch back.");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.expert = true;
            Item.createTile = ModContent.TileType<HighPriestess>();
        }

        public override bool CanUseItem(Player player)
        {
            bool boss = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].boss && Main.npc[i].active)
                    boss = true;
            }
            return !boss;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            NewDawnWorld.downedEtheria = !NewDawnWorld.downedEtheria;
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            modPlayer.etherialTrail = 80;
            //TODO: Add sound effect for this item
            //SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Sound, "Sounds/EtherialChange"));
            return true;
        }
    }
}