using NewDawn.Items.Accessories;
using NewDawn.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Control
{
    internal sealed class FlawlessMedallionArmorSetSystem : ModSystem
    {
        public override void Load()
        {
            On_Player.UpdateArmorSets += ApplyStoredArmorSets;
        }

        public override void Unload()
        {
            On_Player.UpdateArmorSets -= ApplyStoredArmorSets;
        }

        private static void ApplyStoredArmorSets(On_Player.orig_UpdateArmorSets orig, Player player, int i)
        {
            orig(player, i);
            FlawlessMedallionPlayer medallionPlayer = player.GetModPlayer<FlawlessMedallionPlayer>();
            medallionPlayer.StoredSetBonusText = string.Empty;

            for (int accessorySlot = 3; accessorySlot < player.armor.Length && accessorySlot < 10; accessorySlot++)
            {
                if (!player.IsItemSlotUnlockedAndUsable(accessorySlot))
                    continue;

                Item equippedAccessory = player.armor[accessorySlot];
                if (equippedAccessory?.ModItem is not FlawlessMedallion medallion)
                    continue;

                Item[] storedArmor = medallion.Items;
                if (storedArmor == null || storedArmor.Length < 3 ||
                    storedArmor[0] == null || storedArmor[0].IsAir ||
                    storedArmor[1] == null || storedArmor[1].IsAir ||
                    storedArmor[2] == null || storedArmor[2].IsAir)
                    continue;

                Item originalHead = player.armor[0];
                Item originalBody = player.armor[1];
                Item originalLegs = player.armor[2];
                int originalHeadSlot = player.head;
                int originalBodySlot = player.body;
                int originalLegSlot = player.legs;
                string displayedSetBonus = player.setBonus;
                string storedSetBonus = string.Empty;

                try
                {
                    player.armor[0] = storedArmor[0];
                    player.armor[1] = storedArmor[1];
                    player.armor[2] = storedArmor[2];
                    player.head = storedArmor[0].headSlot;
                    player.body = storedArmor[1].bodySlot;
                    player.legs = storedArmor[2].legSlot;

                    orig(player, i);
                    storedSetBonus = player.setBonus;
                }
                finally
                {
                    player.armor[0] = originalHead;
                    player.armor[1] = originalBody;
                    player.armor[2] = originalLegs;
                    player.head = originalHeadSlot;
                    player.body = originalBodySlot;
                    player.legs = originalLegSlot;
                    player.setBonus = displayedSetBonus;
                }

                if (!string.IsNullOrWhiteSpace(storedSetBonus))
                    medallionPlayer.StoredSetBonusText = string.IsNullOrWhiteSpace(medallionPlayer.StoredSetBonusText)
                        ? storedSetBonus
                        : $"{medallionPlayer.StoredSetBonusText}\n{storedSetBonus}";
            }

            if (!player.setStardust)
            {
                foreach (Projectile projectile in Main.projectile)
                {
                    if (projectile.active &&
                        projectile.owner == player.whoAmI &&
                        projectile.type == ProjectileID.StardustGuardian)
                    {
                        projectile.Kill();
                    }
                }
            }
        }
    }
}
