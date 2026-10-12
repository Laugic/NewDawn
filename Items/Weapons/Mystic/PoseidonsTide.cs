using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Weapons.Mystic
{
    public class PoseidonsTide : MysticItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Poseidon's Tide");
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 70;
            Item.width = 76;
            Item.height = 76;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item92;
            Item.autoReuse = true;
            Item.shootSpeed = 8f;
            Item.scale = 1;
        }


        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "A spear that shoots bubbles";
                case 2:
                    return "Shoots a trident that is chained to you. Inflicts 'Bubbly', \nwhich makes enemies emit bubbles";
                case 3:
                    return "Creates mini cthulunados that shoot bubbles and fly at enemies";
                default:
                    return "";
            }
        }

        public override bool CanUseItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
                return player.ownedProjectileCounts[Item.shoot] < 1;
            return true;
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 65;
            Item.UseSound = SoundID.Item76;
            Item.useAnimation = Item.useTime = 30;
            Item.knockBack = 8;
            Item.shootSpeed = 8f;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.shoot = ModContent.ProjectileType<PoseidonDestruction>();
            LuxCost = 15;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 80;
            Item.UseSound = SoundID.Item92;
            Item.useAnimation = Item.useTime = 21;
            Item.knockBack = 8;
            Item.shootSpeed = 18f;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.shoot = ModContent.ProjectileType<PoseidonIllusion>();
            VisCost = 15;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.damage = 65;
            Item.UseSound = SoundID.Item21;
            Item.useAnimation = Item.useTime = 53;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.useTurn = true;
            Item.noUseGraphic = false;
            Item.useStyle = 1;
            Item.shoot = ModContent.ProjectileType<PoseidonConjuration1>();
            MundusCost = 30;
        }
    }
}