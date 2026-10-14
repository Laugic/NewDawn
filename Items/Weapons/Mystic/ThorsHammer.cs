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
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Misc;

namespace NewDawn.Items.Weapons.Mystic
{
    public class ThorsHammer : MysticItem
    {
        bool IllusionCharged { get; set; }

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
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
            IllusionCharged = false;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (Main.LocalPlayer.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    return "Throw the hammer";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Swing the hammer when you land on the ground after a fall to create a Lightning Explosion\nInflicts 'Thunder Charged', which makes enemies release lightning bolts after being hit";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Creates Shadow Portals that shoot lightning";
                default:
                    return "";
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Illusion && mysticPlayer.Vis > VisCost)
            {
                target.AddBuff(ModContent.BuffType<ThunderCharged>(), (int)(8 * 60 * mysticPlayer.MysticDuration) + Main.rand.Next(1 * 60));
            }
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 100;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = Item.useTime = 20;
            Item.knockBack = 3;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<ThorHammerDestruction>();
            Item.UseSound = SoundID.Item71;
            Item.shootSpeed = 36f;
            LuxCost = 10;
            Item.autoReuse = true;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 300;
            Item.noMelee = false;
            Item.noUseGraphic = false;
            Item.autoReuse = false;
            VisCost = 40;
            Item.useAnimation = Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.UseSound = SoundID.Item71;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 140;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useTime = 60;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<ThorShadowConjuration>();
            Item.UseSound = SoundID.Item93;
            MundusCost = 40;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GalacticFragment>(), 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
