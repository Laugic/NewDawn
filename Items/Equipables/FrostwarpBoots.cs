using NewDawn.Dusts;
using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class FrostwarpBoots : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
            NewDawnVars.RocketBoots.Add(Item.type);
}

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 45;
            VDashCooldownMax = 45;
            DashSpeed = 14;
            MaxVDashes = 4;
            JumpSpeed = 12;
            JumpDur = 20;
            ImmuneTime = 15;

            DustType = ModContent.DustType<White>();
            TrailLength = 45;
        }

        public override void OtherBonuses(Player player, bool hideVisual)
        {
            player.moveSpeed += .2f;
            player.accRunSpeed += 4.5f;
            player.rocketBoots = 3;
            player.jumpSpeedBoost += 3;
            player.iceSkate = true;
            player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();;
            player.noFallDmg = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WarpburstBoots>());
            recipe.AddIngredient(ModContent.ItemType<FrostburstBoots>());
            recipe.AddIngredient(ModContent.ItemType<SoulOfSought>(), 4);
            recipe.AddIngredient(ModContent.ItemType<SoulOfHaught>(), 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}