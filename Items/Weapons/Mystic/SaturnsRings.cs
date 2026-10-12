using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Mystic
{
    public class SaturnsRings : MysticItem
    {
        int charge = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Saturn's Rings");
            // Tooltip.SetDefault("'The majesty of Space'");
        }
        
        public override void SetMysticDefaults()
        {
            charge = 0;
            Item.damage = 60;
            Item.width = 68;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 13);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "Shoots a burst of stars after charging 3 times";
                case 2:
                    return "Shoots rings that inflict 'Orbital', which\nmakes enemies take 33% more damage when you're in Space";
                case 3:
                    return "Creates rings that orbit around you and periodically shoot stars";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 40f;
            position += muzzleOffset;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
            {
                charge++;
                SoundEngine.PlaySound(SoundID.Item60, position);
                if(charge > 2)
                {
                    int numberProjectiles = 24;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(7));
                        float scale = 1f - (Main.rand.NextFloat() * .4f);
                        perturbedSpeed = perturbedSpeed * scale;
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                    }

                    charge = 0;
                    SoundEngine.PlaySound(SoundID.Item84, position);
                }
                return false;
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.useStyle = 5;
            Item.damage = 65;
            Item.useTime = 45;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 4;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<SaturnDestruction>();
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.useStyle = 5;
            Item.damage = 50;
            Item.useTime = 24;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 1;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<SaturnIllusion1>();
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.useStyle = 5;
            Item.damage = 60;
            Item.useTime = 24;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<SaturnConjuration1>();
        }
    }
}