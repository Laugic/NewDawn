using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class ArtifactOfEnhancement : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 72;
            Item.value = 10000;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
            Item.defense = 4;
            Item.lifeRegen = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.SoulStoneVisuals)
            {
                if (modPlayer.spelunker)
                    player.findTreasure = true;
                Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
                if (modPlayer.owl)
                    player.nightVision = true;
                if (modPlayer.hunter)
                    player.detectCreature = true;
                if (modPlayer.danger)
                    player.dangerSense = true;
                if(modPlayer.inf)
                    player.AddBuff(116, 2);
            }
            if (modPlayer.SoulStoneMovement)
            {
                if (modPlayer.feather)
                    player.slowFall = true;
            }
            player.maxMinions++;
            player.resistCold = true;
            player.lifeMagnet = true;
            player.statLifeMax2 += (player.statLifeMax + player.statLifeMax2) / 5 / 20 * 20 - (player.statLifeMax / 5 / 20 * 20);
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[24] = true;
            if (modPlayer.ww)
                player.waterWalk = true;
            player.gills = true;
            player.ignoreWater = true;
            player.accFlipper = true;
            player.cratePotion = true;
            player.sonarPotion = true;
            player.fishingSkill += 15;
            player.tileSpeed += 0.25f;
            player.wallSpeed += 0.25f;
            player.blockRange++;
            player.pickSpeed -= 0.25f;
            player.moveSpeed += 0.25f;
            player.endurance += 0.10f;
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
            player.manaRegenBonus += 25;
            player.GetDamage(DamageClass.Magic) += 0.20f;
            player.kbBuff = true;
            if (player.thorns < 1f)
            {
                player.thorns = 0.333333343f;
            }
            player.ammoCost80 = true;
            player.GetDamage(DamageClass.Ranged) += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AncientRelic>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CursedRelic>(), 1);
            recipe.AddIngredient(ModContent.ItemType<HallowedRelic>(), 1);
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}