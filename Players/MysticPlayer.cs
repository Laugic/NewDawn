using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace NewDawn.Players
{
    public class MysticPlayer : ModPlayer
    {
        public float MysticDamage = 1f, MysticDuration = 1f, PotentiaRegenMult = 1f, IllusionDamage = 1f, DestructionDamage = 1f, ConjurationDamage = 1f,
            LuxRegen = 0.01f, VisRegen = 0.01f, MundusRegen = 0.01f, Lux = 100, Vis = 100, Mundus = 100, OverflowDamage = 1f,
            LuxCostMod = 1f, VisCostMod = 1f, MundusCostMod = 1f,
            LuxOverflow = 1f, VisOverflow = 1f, MundusOverflow = 1f, OverflowCapacity = 1.25f, PotentiaCostMod = 1f;

        private float MaxLux = 100, MaxVis = 100, MaxMundus = 100;

        public MysticMode CurrentMysticMode = MysticMode.Destruction;

        public enum MysticMode
        {
            Destruction,
            Illusion,
            Conjuration
        }

        public override void PostUpdate()
        {
            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("IsHoldingMystic: " + IsHoldingMystic()), Color.White);
            base.PostUpdate();
            PotentiaRegen();
            if (Main.mouseRight && Main.mouseRightRelease)
                MysticSwitch();
        }

        private void MysticReset()
        {
        }
        private void PotentiaRegen()
        {
            switch (CurrentMysticMode)
            {
                case MysticMode.Destruction:
                    Vis += VisRegen;
                    Mundus += MundusRegen;
                    break;
                case MysticMode.Illusion:
                    Lux += LuxRegen;
                    Mundus += MundusRegen;
                    break;
                case MysticMode.Conjuration:
                    Lux += LuxRegen;
                    Vis += VisRegen;
                    break;
            }
            Lux = Math.Min(Lux, GetMaxLux() * LuxOverflow * OverflowCapacity);
            Vis = Math.Min(Vis, GetMaxVis() * VisOverflow * OverflowCapacity);
            Mundus = Math.Min(Mundus, GetMaxMundus() * MundusOverflow * OverflowCapacity);
            if (Player.statLife < 1)
            {
                Lux = GetMaxLux();
                Vis = GetMaxVis();
                Mundus = GetMaxMundus();
            }
        }

        private void MysticSwitch()
        {
            if (!IsHoldingMystic())
                return;
            switch (CurrentMysticMode)
            {
                case MysticMode.Destruction:
                    CurrentMysticMode = MysticMode.Illusion;
                    break;
                case MysticMode.Illusion:
                    CurrentMysticMode = MysticMode.Conjuration;
                    break;
                case MysticMode.Conjuration:
                    CurrentMysticMode = MysticMode.Destruction;
                    break;
            }
            NewDawn.EnigmaMysticUI.CyclePositions(CurrentMysticMode);
            SoundStyle switchSound = new SoundStyle("NewDawn/Sounds/Sounds/MysticSwitch")
            {
                Volume = 0.9f,
                PitchVariance = 0.0f,
                MaxInstances = 3,
            };
            SoundEngine.PlaySound(switchSound, Player.Center);
        }

        public void AddLux(float amount)
        {
            Lux = Math.Min(Lux + amount, GetMaxLux() * LuxOverflow * OverflowCapacity);
        }

        public void AddVis(float amount)
        {
            Vis = Math.Min(Vis + amount, GetMaxVis() * VisOverflow * OverflowCapacity);
        }

        public void AddMundus(float amount)
        {
            Mundus = Math.Min(Mundus + amount, GetMaxMundus() * MundusOverflow * OverflowCapacity);
        }

        public int GetMaxLux()
        {
            return (int)MaxLux; //TODO: Add permanent boosts
        }

        public int GetMaxVis()
        {
            return (int)MaxVis; //TODO: Add permanent boosts
        }

        public int GetMaxMundus()
        {
            return (int)MaxMundus; //TODO: Add permanent boosts
        }

        public bool IsHoldingMystic()
        {
            return Player.HeldItem.ModItem is Items.Weapons.MysticItem;
        }

        public bool IsOnOverflow()
        {
            if (!IsHoldingMystic())
                return false;

            switch (CurrentMysticMode)
            {
                case MysticMode.Destruction:
                    return Lux > GetMaxLux();
                case MysticMode.Illusion:
                    return Vis > GetMaxVis();
                case MysticMode.Conjuration:
                    return Mundus > GetMaxMundus();
                default:
                    return false;
            }
        }
    }
}
