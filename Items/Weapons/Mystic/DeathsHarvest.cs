using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using NewDawn.Projectiles.Mystic.Conjuration;
using Terraria;
using NewDawn.Projectiles.Special;
using Microsoft.Xna.Framework;
using System;
using NewDawn.Buffs;
using NewDawn.NPCs;

namespace NewDawn.Items.Weapons.Mystic
{
    public class DeathsHarvest : MysticItem
    {
        bool IllusionCharged { get; set; }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Death's Harvest");
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 10;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.noMelee = false;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
            IllusionCharged = false;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "The scythe grows as you swing it";
                case 2:
                    return "Charge the scythe, then dash forward\nInflicts 'Death Mark', which makes enemies drop Souls on death\nSouls restore Potentia and grant the 'Reaper' buff";
                case 3:
                    return "Throws Scythes that hover around and follow you\nSpawn Skull Bursts upon picking up a Soul";
                default:
                    return "";
            }
        }
        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1 && modPlayer.Lux >= LuxCost)
            {
                Item.scale = Math.Min(Math.Max(1.5f, Item.scale + .05f), 2.5f);
                return false;
            }
            else
                Item.scale = 1;
            if(modPlayer.MysticMode == 2)
            {
                if (!IllusionCharged)
                    IllusionCharged = true;
                else
                {
                    IllusionCharged = false;
                    player.immune = true;
                    player.immuneTime = 30;
                    player.velocity.X += speedX;
                    player.velocity.Y += speedY;
                }
            }
            else
                IllusionCharged = false;
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, damage, knockback, crit);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 2 && modPlayer.Vis > VisCost)
            {
                target.AddBuff(ModContent.BuffType<DeathMark>(), (int)(8 * 60 * modPlayer.MysticDuration) + Main.rand.Next(1 * 60));
                target.GetGlobalNPC<LaugicalGlobalNPCs>().DeathMarked = true;
            }
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 20;
            Item.noMelee = false;
            Item.noUseGraphic = false;
            Item.useAnimation = Item.useTime = 25;
            Item.knockBack = 3;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.UseSound = SoundID.Item71;
            LuxCost = 5;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 40;
            Item.noMelee = false;
            Item.noUseGraphic = false;
            VisCost = 10;
            if (modPlayer.Vis < VisCost)
            {
                Item.damage = 15;
                IllusionCharged = false;
            }
            Item.useTime = 60;
            Item.useAnimation = IllusionCharged ? 60 : 20;
            Item.useStyle = IllusionCharged ? ItemUseStyleID.Shoot : ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.UseSound = IllusionCharged ? SoundID.Item60: SoundID.Item71;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTime = 60;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<DeathConjuration>();
            Item.UseSound = SoundID.Item71;
            MundusCost = 20;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(NewDawn.EVIL_BARS_GROUP, 16);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
