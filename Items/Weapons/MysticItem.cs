using Microsoft.Xna.Framework;
using NewDawn.Buffs.Mystic;
using NewDawn.Players;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Overflow;
using NewDawn.UI;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons
{
    public abstract class MysticItem : ModItem
    {
        public abstract void Destruction(MysticPlayer modPlayer);

        public abstract void Illusion(MysticPlayer modPlayer);

        public abstract void Conjuration(MysticPlayer modPlayer);

        public abstract void SetMysticDefaults();

        public sealed override void SetDefaults()
        {
            Hold = 0;
            Item.DamageType = DamageClass.Generic;
            Item.crit = 4;
            LuxCost = 10;
            VisCost = 10;
            MundusCost = 10;
            Item.GetGlobalItem<NewDawnGlobalItem>().Mystic = true;
            SetMysticDefaults();
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
            if (Hold > 0)
                Hold--;

            TooltipLine tt2 = new TooltipLine(Mod, "PlayerMysticChanneling", GetMysticType());
            TooltipLine tt3 = new TooltipLine(Mod, "PlayerMysticChanneling", GetMysticaType());
            TooltipLine tt4 = new TooltipLine(Mod, "PlayerMysticChanneling", GetExtraTooltip());

            tooltips.Insert(++index, tt2);
            tooltips.Insert(++index, tt3);
            if (GetExtraTooltip() != "")
                tooltips.Insert(++index, tt4);
            /*if (!item.social && item.prefix > 0)
            {
                if (destruction == 1f)
                {
                    TooltipLine line = new TooltipLine(mod, "Chaotic", "[c/63CA89:+5% Destruction Damage]");
                    //line.isModifier = true;
                    tooltips.Add(line);
                }
                if (destruction == 2f)
                {
                    TooltipLine line = new TooltipLine(mod, "Riotus", "c/63CA89:+10% Destruction Damage");
                    //line.isModifier = true;
                    tooltips.Add(line);
                }
                if (destruction == 3f)
                {
                    TooltipLine line = new TooltipLine(mod, "Anarchic", "c/63CA89:+12% Destruction Damage\n+1 Destruction Power");
                    //line.isModifier = true;
                    tooltips.Add(line);
                }
            }*/
        }

        /*
        public string ChooseMysticPrefix()
        {
            /* 50%: Destruction/Illusion/Conjuration
             * 25%: Cool Utility effects (Mystic Duration, various Mystic Burst effects)
             * 15%: Negative
             * 10%: Awesome Effects
             * 
            int pref = Main.rand.Next(100);
            if (pref < 51)//Destruction/Illusion/Conjuration
            {
                /*0-23 = level 1
                 * 24-41 = level 2
                 * 42-50 = level 3
                 * 
                if (pref < 8)//D1
                    return "Chaotic";
                if (pref < 16)//I1
                    return "Deceptive";
                if (pref < 24)//C1
                    return "Stable";
                if (pref < 30)//D2
                    return "Riotus";
                if (pref < 36)//I2
                    return "Illusive";
                if (pref < 42)//C2
                    return "Ordered";
                if (pref < 45)//D3
                    return "Anarchic";
                if (pref < 48)//I3
                    return "Manipulative";
                if (pref < 51)//C3
                    return "Authoritus";
            }
            else if (pref < 75)//Cool Utility effects (Mystic Duration, various Mystic Burst effects)
            {

            }
            else if (pref < 90)//Negative
            {

            }
            else//Awesome Effects
            {

            }
            return "";
        }

        public override int ChoosePrefix(UnifiedRandom rand)
        {
            if (this.mystic)
            {
                if (Main.rand.Next(10) != 9)
                {
                    string pref = ChooseMysticPrefix();
                    //return ModContent.PrefixType("Chaotic");
                    if (pref != "")
                        return ModContent.PrefixType(pref);
                    else
                        return -1;
                }
                return -1;
            }

            /*
            if (item.accessory || item.damage > 0 && item.maxStack == 1 && rand.NextBool(30))
            {
                return ModContent.PrefixType(rand.Next(2) == 0 ? "Awesome" : "ReallyAwesome");
            }

            return -1;
        }

        public override void GetWeaponDamage(Player player, ref int damage)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            float damageMult = 1f;
            damageMult = modPlayer.MysticDamage + (player.allDamage - 1);

            switch (modPlayer.MysticMode)
            {
                case 1:
                    damage = (int)(damage * modPlayer.DestructionDamage);
                    if (modPlayer.Lux > modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost)
                        damage = (int)(damage * modPlayer.OverflowDamage);
                    else
                        damage = (int)(damage * modPlayer.AntiflowDamage);
                    break;
                case 2:
                    damage = (int)(damage * modPlayer.IllusionDamage);
                    if (modPlayer.Vis > modPlayer.VisMax + modPlayer.VisMaxPermaBoost)
                        damage = (int)(damage * modPlayer.OverflowDamage);
                    else
                        damage = (int)(damage * modPlayer.AntiflowDamage);
                    break;
                case 3:
                    damage = (int)(damage * modPlayer.ConjurationDamage);
                    if (modPlayer.Mundus > modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost)
                        damage = (int)(damage * modPlayer.OverflowDamage);
                    else
                        damage = (int)(damage * modPlayer.AntiflowDamage);
                    break;
            }

            if (modPlayer.MysticBurstDisabled)
                damage = (int)(damage * 1.05f);

            modPlayer.MysticHold = 2;
        }*/

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            MysticPlayer modPlayer = player.GetModPlayer<MysticPlayer>();
            damage += modPlayer.MysticDamage - 1;

            switch (player.GetModPlayer<MysticPlayer>().CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    damage += modPlayer.DestructionDamage - 1;
                    if (modPlayer.Lux > modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost)
                        damage += modPlayer.OverflowDamage - 1;
                    else
                        damage += modPlayer.AntiflowDamage - 1;
                    break;
                case MysticPlayer.MysticMode.Illusion:
                    damage += modPlayer.IllusionDamage - 1;
                    if (modPlayer.Vis > modPlayer.VisMax + modPlayer.VisMaxPermaBoost)
                        damage += modPlayer.OverflowDamage - 1;
                    else
                        damage += modPlayer.AntiflowDamage - 1;
                    break;
                case MysticPlayer.MysticMode.Conjuration:
                    damage += modPlayer.ConjurationDamage - 1;
                    if (modPlayer.Mundus > modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost)
                        damage += modPlayer.OverflowDamage - 1;
                    else
                        damage += modPlayer.AntiflowDamage - 1;
                    break;
            }

            if (modPlayer.MysticBurstDisabled)
                damage += .05f;

            modPlayer.MysticHold = 2;
        }

        public override void UpdateInventory(Player player)
        {
            GetCosts(player);
            base.UpdateInventory(player);
        }

        private void GetCosts(Player player)
        {

            MysticPlayer modPlayer = player.GetModPlayer<MysticPlayer>();

            switch (player.GetModPlayer<MysticPlayer>().CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    Destruction(modPlayer);
                    break;
                case MysticPlayer.MysticMode.Illusion:
                    Illusion(modPlayer);
                    break;
                case MysticPlayer.MysticMode.Conjuration:
                    Conjuration(modPlayer);
                    break;
            }
        }

        public override void HoldItem(Player player)
        {
            MysticPlayer mysticPlayer = player.GetModPlayer<MysticPlayer>();

            switch (player.GetModPlayer<MysticPlayer>().CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    player.AddBuff(ModContent.BuffType<Destruction>(), 1, true);
                    break;
                case MysticPlayer.MysticMode.Illusion:
                    player.AddBuff(ModContent.BuffType<Illusion>(), 1, true);
                    break;
                case MysticPlayer.MysticMode.Conjuration:
                    player.AddBuff(ModContent.BuffType<Conjuration>(), 1, true);
                    break;
            }

            GetCosts(player);

            Hold = 2;
            mysticPlayer.CurrentLuxCost = LuxCost;
            mysticPlayer.CurrentVisCost = VisCost;
            mysticPlayer.CurrentMundusCost = MundusCost;

            NewDawnUI.Update();
        }

        private string GetMysticType()
        {
            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();

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

        private string GetMysticaType()
        {
            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();

            switch (Main.LocalPlayer.GetModPlayer<MysticPlayer>().CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    return "Uses " + (GetLuxCost() * mysticPlayer.LuxUseRate * mysticPlayer.GlobalPotentiaUseRate) + " lux";
                case MysticPlayer.MysticMode.Illusion:
                    return "Uses " + (GetVisCost() * mysticPlayer.VisUseRate * mysticPlayer.GlobalPotentiaUseRate) + " vis";
                case MysticPlayer.MysticMode.Conjuration:
                    return "Uses " + (GetMundusCost() * mysticPlayer.MundusUseRate * mysticPlayer.GlobalPotentiaUseRate) + " mundus";
            }

            return "mystica";
        }

        /*public override bool CanUseItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            switch (modPlayer.MysticMode)
            {
                case 1:
                    return modPlayer.Lux > luxCost;
                case 2:
                    return modPlayer.Vis > visCost;
                case 3:
                    return modPlayer.Mundus > mundusCost;
            }
            return true;
        }*/

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            MysticPlayer modPlayer = player.GetModPlayer<MysticPlayer>();

            ModifyMysticShoot(player, ref position, ref velocity.X, ref velocity.Y, ref type, ref damage, ref knockback);
            GetMysticShots(player, ref position, ref velocity.X, ref velocity.Y, ref type, ref damage, ref knockback);
            switch (player.GetModPlayer<MysticPlayer>().CurrentMysticMode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    if (modPlayer.Lux >= LuxCost * modPlayer.LuxUseRate * modPlayer.GlobalPotentiaUseRate)
                    {
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
                    else
                        return false;
                    break;
                case MysticPlayer.MysticMode.Illusion:
                    if (modPlayer.Vis >= VisCost * modPlayer.VisUseRate * modPlayer.GlobalPotentiaUseRate)
                    {
                        modPlayer.Vis -= VisCost * modPlayer.VisUseRate * modPlayer.GlobalPotentiaUseRate;
                        if (modPlayer.Vis < 0)
                            modPlayer.Vis = 0;
                        modPlayer.Lux += VisCost * modPlayer.GlobalAbsorbRate * modPlayer.LuxAbsorbRate * modPlayer.VisDischargeRate * modPlayer.VisUseRate * modPlayer.GlobalPotentiaUseRate;
                        if (modPlayer.Lux > (modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost) * modPlayer.LuxOverflow * modPlayer.GlobalOverflow)
                            modPlayer.Lux = (modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost) * modPlayer.LuxOverflow * modPlayer.GlobalOverflow;
                        if (modPlayer.Vis > (modPlayer.VisMax + modPlayer.VisMaxPermaBoost) * modPlayer.VisOverflow * modPlayer.GlobalOverflow)
                            modPlayer.Vis = (modPlayer.VisMax + modPlayer.VisMaxPermaBoost) * modPlayer.VisOverflow * modPlayer.GlobalOverflow;
                        modPlayer.Mundus += VisCost * modPlayer.GlobalAbsorbRate * modPlayer.MundusAbsorbRate * modPlayer.VisDischargeRate * modPlayer.VisUseRate * modPlayer.GlobalPotentiaUseRate;
                        if (modPlayer.Mundus > (modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost) * modPlayer.MundusOverflow * modPlayer.GlobalOverflow)
                            modPlayer.Mundus = (modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost) * modPlayer.MundusOverflow * modPlayer.GlobalOverflow;
                    }
                    else
                        return false;
                    break;
                case MysticPlayer.MysticMode.Conjuration:
                    if (modPlayer.Mundus >= MundusCost * modPlayer.MundusUseRate * modPlayer.GlobalPotentiaUseRate)
                    {
                        modPlayer.Mundus -= MundusCost * modPlayer.MundusUseRate * modPlayer.GlobalPotentiaUseRate;
                        if (modPlayer.Mundus < 0)
                            modPlayer.Mundus = 0;
                        modPlayer.Lux += MundusCost * modPlayer.GlobalAbsorbRate * modPlayer.LuxAbsorbRate * modPlayer.MundusDischargeRate * modPlayer.MundusUseRate * modPlayer.GlobalPotentiaUseRate;
                        if (modPlayer.Lux > (modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost) * modPlayer.LuxOverflow * modPlayer.GlobalOverflow)
                            modPlayer.Lux = (modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost) * modPlayer.LuxOverflow * modPlayer.GlobalOverflow;
                        modPlayer.Vis += MundusCost * modPlayer.GlobalAbsorbRate * modPlayer.VisAbsorbRate * modPlayer.MundusDischargeRate * modPlayer.MundusUseRate * modPlayer.GlobalPotentiaUseRate;
                        if (modPlayer.Vis > (modPlayer.VisMax + modPlayer.VisMaxPermaBoost) * modPlayer.VisOverflow * modPlayer.GlobalOverflow)
                            modPlayer.Vis = (modPlayer.VisMax + modPlayer.VisMaxPermaBoost) * modPlayer.VisOverflow * modPlayer.GlobalOverflow;
                        if (modPlayer.Mundus > (modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost) * modPlayer.MundusOverflow * modPlayer.GlobalOverflow)
                            modPlayer.Mundus = (modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost) * modPlayer.MundusOverflow * modPlayer.GlobalOverflow;
                    }
                    else
                        return false;
                    break;
            }
            return MysticShoot(player, ref position, ref velocity.X, ref velocity.Y, ref type, ref damage, ref knockback);
        }

        private void ModifyMysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (mysticPlayer.IsOnOverflow())
            {
                speedX *= mysticPlayer.OverflowVelocity;
                speedY *= mysticPlayer.OverflowVelocity;
            }
        }

        private void GetMysticShots(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (modPlayer.Crystillium && mysticPlayer.IsOnOverflow())
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, new Vector2(speedX / 16f, speedY / 16f), ModContent.ProjectileType<CrystilliumShard>(), damage, knockBack, player.whoAmI);
            if (modPlayer.OverflowFire && mysticPlayer.IsOnOverflow())
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, new Vector2(speedX, speedY), ModContent.ProjectileType<OverflowThermalite>(), (int)(damage / 1.2f), knockBack, player.whoAmI);
            if (mysticPlayer.ShroomOverflow > 0 && mysticPlayer.IsOnOverflow())
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), new Vector2(player.Center.X, player.position.Y), new Vector2(0, -10f), ModContent.ProjectileType<FreyaConjuration2>(), damage, 3, player.whoAmI);
        }

        public virtual bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return true;
        }

        public virtual string GetExtraTooltip()
        {
            return "";
        }

        private int GetLuxCost()
        {
            Destruction(Main.LocalPlayer.GetModPlayer<MysticPlayer>());
            return LuxCost;
        }

        private int GetVisCost()
        {
            Illusion(Main.LocalPlayer.GetModPlayer<MysticPlayer>());
            return VisCost;
        }

        private int GetMundusCost()
        {
            Conjuration(Main.LocalPlayer.GetModPlayer<MysticPlayer>());
            return MundusCost;
        }

        public int LuxCost { get; set; } = 10;

        public int MundusCost { get; set; } = 10;

        public int VisCost { get; set; } = 10;

        public int Hold { get; private set; }
    }
}