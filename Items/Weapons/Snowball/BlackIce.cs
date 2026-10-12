using NewDawn.Items.Loot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Items.Materials;

namespace NewDawn.Items.Weapons.Range
{
    public class BlackIce : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Ice");
            // Tooltip.SetDefault("Fires faster in extreme heat\nFires more accurately in extreme cold\n50% chance not to consume ammo\n'They won't see it coming'");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }

        public override void HoldItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Item.useTime = Item.useAnimation = 8;
            if(player.ZoneDesert || player.ZoneUnderworldHeight || modPlayer.zoneObsidium)
                Item.useTime = Item.useAnimation = 6;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 40f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
                position.X += -4 + Main.rand.Next(8);
                position.Y += -4 + Main.rand.Next(8);
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity;
                int spread = 24;
                if (player.ZoneDesert || player.ZoneUnderworldHeight || modPlayer.zoneObsidium)
                    perturbedSpeed *= 1.25f;
                if (player.ZoneSnow || player.ZoneSkyHeight || NewDawnWorld.downedEtheria)
                {
                    spread = 10;
                }
                perturbedSpeed = perturbedSpeed.RotatedByRandom(MathHelper.ToRadians(spread));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(2) == 0)
                return false;
            return base.CanConsumeAmmo(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 5);
        }
    }
}