using Microsoft.Xna.Framework;
using NewDawn.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons
{
    public abstract class MysticWeapon_New : ModItem
    {
        public int LuxCost = 10, VisCost = 10, MundusCost = 10;
        public abstract void Destruction(MysticPlayer modPlayer);

        public abstract void Illusion(MysticPlayer modPlayer);

        public abstract void Conjuration(MysticPlayer modPlayer);
        public abstract void SetMysticDefaults();
        public sealed override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<MysticDamageClass>();
            SetMysticDefaults();
            //Item.UseSound = SoundID.Item1;
            //Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = player.GetDamage<MysticDamageClass>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            int index = tooltips.FindIndex(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (tt != null)
            {

                string[] split = tt.Text.Split(' ');

                tt.Text = split.First() + " mystic " + split.Last();
            }

            TooltipLine tt2 = new TooltipLine(Mod, "PlayerMysticChanneling", GetMysticType());
            TooltipLine tt3 = new TooltipLine(Mod, "PlayerMysticChanneling", GetPotentiaType());
            TooltipLine tt4 = new TooltipLine(Mod, "PlayerMysticChanneling", GetExtraTooltip());

            tooltips.Insert(++index, tt2);
            tooltips.Insert(++index, tt3);
            if (GetExtraTooltip() != "")
                tooltips.Insert(++index, tt4);
        }
        public virtual string GetExtraTooltip()
        {
            return "";
        }
        private string GetMysticType()
        {
            switch (Main.LocalPlayer.GetModPlayer<MysticPlayer>().CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    return "[c/F1C40F:- Destruction -]";
                case MysticPlayer.MysticMode.Illusion:
                    return "[c/8E44AD:- Illusion -]";
                case MysticPlayer.MysticMode.Conjuration:
                    return "[c/28B463:- Conjuration -]";
            }

            return "mystic";
        }
        private string GetPotentiaType()
        {
            switch (Main.LocalPlayer.GetModPlayer<MysticPlayer>().CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    return "Uses " + GetLuxCost() + " lux";
                case MysticPlayer.MysticMode.Illusion:
                    return "Uses " + GetVisCost() + " vis";
                case MysticPlayer.MysticMode.Conjuration:
                    return "Uses " + GetMundusCost() + " mundus";
            }

            return "mystica";
        }

        public override void UpdateInventory(Player player)
        {
            UpdateCosts();
            base.UpdateInventory(player);
        }

        private void UpdateCosts()
        {
            switch (Main.LocalPlayer.GetModPlayer<MysticPlayer>().CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    GetLuxCost();
                    break;
                case MysticPlayer.MysticMode.Illusion:
                    GetVisCost();
                    break;
                case MysticPlayer.MysticMode.Conjuration:
                    GetMundusCost();
                    break;
            }
        }

        public override void HoldItem(Player player)
        {

            MysticPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();

            //switch (modPlayer.MysticMode)
            //{
            //    case 1:
            //        player.AddBuff(ModContent.BuffType<Destruction>(), 1, true);
            //        break;
            //    case 2:
            //        player.AddBuff(ModContent.BuffType<Illusion>(), 1, true);
            //        break;
            //    case 3:
            //        player.AddBuff(ModContent.BuffType<Conjuration>(), 1, true);
            //        break;
            //}

            UpdateCosts();

            //modPlayer.CurrentLuxCost = LuxCost;
            //modPlayer.CurrentVisCost = VisCost;
            //modPlayer.CurrentMundusCost = MundusCost;

            //NewDawn.EnigmaMysticUI.Update();
        }

        private int GetLuxCost()
        {
            Destruction(Main.LocalPlayer.GetModPlayer<MysticPlayer>());
            return (int)Math.Round(LuxCost * Main.LocalPlayer.GetModPlayer<MysticPlayer>().LuxCostMod * Main.LocalPlayer.GetModPlayer<MysticPlayer>().PotentiaCostMod);
        }

        private int GetVisCost()
        {
            Illusion(Main.LocalPlayer.GetModPlayer<MysticPlayer>());
            return (int)Math.Round(VisCost * Main.LocalPlayer.GetModPlayer<MysticPlayer>().VisCostMod * Main.LocalPlayer.GetModPlayer<MysticPlayer>().PotentiaCostMod);
        }

        private int GetMundusCost()
        {
            Conjuration(Main.LocalPlayer.GetModPlayer<MysticPlayer>());
            return (int)Math.Round(MundusCost * Main.LocalPlayer.GetModPlayer<MysticPlayer>().MundusCostMod * Main.LocalPlayer.GetModPlayer<MysticPlayer>().PotentiaCostMod);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            MysticPlayer modPlayer = player.GetModPlayer<MysticPlayer>();

            ModifyMysticShoot(player, ref position, ref velocity, ref type, ref damage, ref knockback);
            GetMysticShots(player, ref position, ref velocity, ref type, ref damage, ref knockback);
            switch (modPlayer.CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    int luxCost = GetLuxCost();
                    if (modPlayer.Lux >= luxCost)
                    {
                        modPlayer.Lux -= luxCost;
                        if (modPlayer.Lux < 0)
                            modPlayer.Lux = 0;
                        if (modPlayer.Lux > modPlayer.GetMaxLux() * modPlayer.LuxOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Lux = modPlayer.GetMaxLux() * modPlayer.LuxOverflow * modPlayer.OverflowCapacity;
                        modPlayer.Vis += luxCost / 2f;
                        if (modPlayer.Vis > modPlayer.GetMaxVis() * modPlayer.VisOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Vis = modPlayer.GetMaxVis() * modPlayer.VisOverflow * modPlayer.OverflowCapacity;
                        modPlayer.Mundus += luxCost / 2f;
                        if (modPlayer.Mundus > modPlayer.GetMaxMundus() * modPlayer.MundusOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Mundus = modPlayer.GetMaxMundus() * modPlayer.MundusOverflow * modPlayer.OverflowCapacity;
                    }
                    else
                        return false;
                    break;
                case MysticPlayer.MysticMode.Illusion:
                    int visCost = GetVisCost();
                    if (modPlayer.Vis >= visCost)
                    {
                        modPlayer.Vis -= visCost;
                        if (modPlayer.Vis < 0)
                            modPlayer.Vis = 0;
                        modPlayer.Vis += visCost / 2f;
                        modPlayer.Mundus += visCost / 2f;

                        if (modPlayer.Lux > modPlayer.GetMaxLux() * modPlayer.LuxOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Lux = modPlayer.GetMaxLux() * modPlayer.LuxOverflow * modPlayer.OverflowCapacity;
                        if (modPlayer.Vis > modPlayer.GetMaxVis() * modPlayer.VisOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Vis = modPlayer.GetMaxVis() * modPlayer.VisOverflow * modPlayer.OverflowCapacity;
                        if (modPlayer.Mundus > modPlayer.GetMaxMundus() * modPlayer.MundusOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Mundus = modPlayer.GetMaxMundus() * modPlayer.MundusOverflow * modPlayer.OverflowCapacity;
                    }
                    else
                        return false;
                    break;
                case MysticPlayer.MysticMode.Conjuration:
                    int mundusCost = GetMundusCost();
                    if (modPlayer.Mundus >= mundusCost)
                    {
                        modPlayer.Mundus -= mundusCost;
                        if (modPlayer.Mundus < 0)
                            modPlayer.Mundus = 0;
                        modPlayer.Lux += mundusCost / 2f;
                        modPlayer.Vis += mundusCost / 2f;

                        if (modPlayer.Lux > modPlayer.GetMaxLux() * modPlayer.LuxOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Lux = modPlayer.GetMaxLux() * modPlayer.LuxOverflow * modPlayer.OverflowCapacity;
                        if (modPlayer.Vis > modPlayer.GetMaxVis() * modPlayer.VisOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Vis = modPlayer.GetMaxVis() * modPlayer.VisOverflow * modPlayer.OverflowCapacity;
                        if (modPlayer.Mundus > modPlayer.GetMaxMundus() * modPlayer.MundusOverflow * modPlayer.OverflowCapacity)
                            modPlayer.Mundus = modPlayer.GetMaxMundus() * modPlayer.MundusOverflow * modPlayer.OverflowCapacity;
                    }
                    else
                        return false;
                    break;
            }
            return MysticShoot(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        private void ModifyMysticShoot(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {
            MysticPlayer modPlayer = player.GetModPlayer<MysticPlayer>();
            //if (modPlayer.IsOnOverflow())
            //{
            //    velocity *= modPlayer.OverflowVelocity;
            //}
        }

        private void GetMysticShots(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {
            MysticPlayer modPlayer = player.GetModPlayer<MysticPlayer>();
            //if (modPlayer.Crystillium && modPlayer.IsOnOverflow())
            //    Projectile.NewProjectile(new EntitySource_Parent(Entity), position, new Vector2(velocity.X / 16f, velocity.Y / 16f), ModContent.ProjectileType<CrystilliumShard>(), damage, knockback, player.whoAmI);
            //if (modPlayer.OverflowFire && modPlayer.IsOnOverflow())
            //    Projectile.NewProjectile(new EntitySource_Parent(Entity), position, new Vector2(velocity.X, velocity.Y), ModContent.ProjectileType<OverflowThermalite>(), (int)(damage / 1.2f), knockback, player.whoAmI);
            //if (modPlayer.ShroomOverflow > 0 && modPlayer.IsOnOverflow())
            //    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(player.Center.X, player.position.Y), new Vector2(0, -10f), ModContent.ProjectileType<FreyaConjuration2>(), damage, 3, player.whoAmI);
        }

        public virtual bool MysticShoot(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            return true;
        }
    }

    public class MysticDamageClass : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
                return StatInheritanceData.Full;

            return new StatInheritanceData(
                damageInheritance: 0f,
                critChanceInheritance: 0f,
                attackSpeedInheritance: 0f,
                armorPenInheritance: 0f,
                knockbackInheritance: 0f
            );
        }

        public override void SetDefaultStats(Player player)
        {
            player.GetCritChance<MysticDamageClass>() += 4;
            player.GetArmorPenetration<MysticDamageClass>() += 2;
        }
        public override bool UseStandardCritCalcs => true;
    }
}
