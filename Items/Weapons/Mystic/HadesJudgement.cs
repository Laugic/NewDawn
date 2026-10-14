using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;
using NewDawn.Items.Placeable;
using NewDawn.Items.Loot;
using NewDawn.Items.Consumables;

namespace NewDawn.Items.Weapons.Mystic
{
    public class HadesJudgement : MysticItem
    {

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (Main.LocalPlayer.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    return "Shoots a short range fireball";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Shoots a sword beam that inflicts 'Judgement', which \nmakes enemies take more damage if they are close to you";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Shoots a sword beam that creates a spiral geyser";
                default:
                    return "";
            }
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 38;
            Item.width = 66;
            Item.height = 74;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.noMelee = false;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
            Item.scale = 1.5f;
        }
        /*
        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode != 1)
                return true;
            else return false;
        }*/

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 38;
            Item.useAnimation = Item.useTime = 20;
            Item.knockBack = 8;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<HadesDestruction>();
            LuxCost = 10;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 38;
            Item.useAnimation = Item.useTime = 20;
            Item.knockBack = 4;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<HadesIllusion>();
            VisCost = 10;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 20;
            Item.useTime = 65;
            Item.useAnimation = 65;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<HadesConjuration>();
            MundusCost = 30;
        }
        /*
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            LuxCost = 16;
            if (modPlayer.MysticMode == 1 && modPlayer.Lux >= LuxCost * modPlayer.LuxUseRate * modPlayer.GlobalPotentiaUseRate)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), target.Center.X + 32, target.Center.Y + 32, 0f, 0f, ModContent.ProjectileType<HadesExplosion>(), damage, knockback, Main.myPlayer);
                
                modPlayer.Lux -= LuxCost * modPlayer.LuxUseRate * modPlayer.GlobalPotentiaUseRate;
                if (modPlayer.Lux < 0)
                    modPlayer.Lux = 0;
                if (modPlayer.Lux > (modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost) * modPlayer.LuxOverflow * modPlayer.GlobalOverflow)
                    modPlayer.Lux = (modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost) * modPlayer.LuxOverflow * modPlayer.GlobalOverflow;
                modPlayer.Vis += LuxCost * modPlayer.GlobalAbsorbRate * modPlayer.VisAbsorbRate * modPlayer.LuxDischargeRate * modPlayer.LuxUseRate * modPlayer.GlobalPotentiaUseRate;
                if (modPlayer.Vis > (modPlayer.VisMax + modPlayer.VisMaxPermaBoost) * modPlayer.VisOverflow * modPlayer.GlobalOverflow)
                    modPlayer.Vis = (modPlayer.VisMax + modPlayer.VisMaxPermaBoost) * modPlayer.VisOverflow * modPlayer.GlobalOverflow;
                modPlayer.Mundus += LuxCost * modPlayer.GlobalAbsorbRate * modPlayer.MundusAbsorbRate * modPlayer.LuxDischargeRate * modPlayer.LuxUseRate * modPlayer.GlobalPotentiaUseRate;
                if (modPlayer.Mundus > (modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost) * modPlayer.MundusOverflow * modPlayer.GlobalOverflow)
                    modPlayer.Mundus = (modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost) * modPlayer.MundusOverflow * modPlayer.GlobalOverflow;
                
            }
            LuxCost = 0;

        }*/

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(ObsidiumBar), 16);
            recipe.AddIngredient(Mod, nameof(LavaGemItem), 8);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumHeart>(), 1);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}