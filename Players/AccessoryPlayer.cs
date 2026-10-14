using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Players
{
    public class AccessoryPlayer : ModPlayer
    {
        public bool SetOnFire { get; set; } = false;
        public float SnowDamage { get; set; } = 1f;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void ResetEffects()
        {
            SetOnFire = false;
            SnowDamage = 1f;
            base.ResetEffects();
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (item.useAmmo == AmmoID.Snowball)
                damage *= SnowDamage;

            base.ModifyWeaponDamage(item, ref damage);
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            InflictDebuffs(item, target, hit, damageDone);
        }

        private void InflictDebuffs(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetOnFire)
                target.AddBuff(BuffID.OnFire, (int)((5 * 60) + Main.rand.Next(60)));
        }

    }
}
