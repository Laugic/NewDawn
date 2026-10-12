using Microsoft.Xna.Framework;
using NewDawn.Dusts;
using NewDawn.Items.Accessories;
using NewDawn.Items.Materials;
using NewDawn.Items.Weapons.Snowball;
using NewDawn.Projectiles.Mystic.Fungon;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs
{
    public class NewDawnGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            var wadidget = new Wagidget();
            if (npc.type == NPCID.GoblinTinkerer)
            {
                int i = 0;
                bool found = false;
                while (i < items.Length && !found)
                {
                    if (items[i].stack == 0)
                        items[i].SetDefaults(ModContent.ItemType<Wagidget>());
                }
            }
        }
        public override void OnKill(NPC npc)
        {
            GetWeaponAndAccDrops(npc);
            GetMaterialDrops(npc);
        }

        private void GetMaterialDrops(NPC npc)
        {
            //if (Main.player[npc.FindClosestPlayer()].ZoneSnow && Main.rand.NextBool(20))
            //    Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Frostpetal>(), 1);

            if (Main.player[npc.FindClosestPlayer()].ZoneUnderworldHeight && Main.rand.NextBool(40))
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LavaGem>(), 1);

            if (Main.player[npc.FindClosestPlayer()].ZoneDungeon && Main.rand.NextBool(10))
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ArcaneShard>(), Main.rand.Next(2, 4));
        }

        private void GetWeaponAndAccDrops(NPC npc)
        {
            if ((npc.type == NPCID.SnowmanGangsta) && Main.rand.NextBool(40))
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Baller>(), 1);

            if ((npc.type == NPCID.IceQueen) && Main.rand.NextBool(Main.expertMode ? 13 : 22))
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<IceCrown>(), 1);
        }

        #region Buffs & Debuffs
        public bool Spored = false;
        public override void ResetEffects(NPC npc)
        {
            Spored = false;
            base.ResetEffects(npc);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Spored)
            {
                if (Main.rand.Next(13) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<ShroomDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.8f);
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (Spored)
            {
                var entitySource = new EntitySource_Parent(npc);
                if (Main.rand.Next(3 * 60) == 0 && Main.netMode != 1)
                    Projectile.NewProjectile(entitySource, npc.Center.X, npc.Center.Y, 0, -10f, ModContent.ProjectileType<FungonConjuration2>(), 8, 3, Main.myPlayer);
            }
        }


        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitByProjectile(npc, projectile, hit, damageDone);
            //if (Fertile && NewDawnVars.SeedProjectiles.Contains(projectile.type))
            //    NumSeeds++;
            if (Spored && NewDawnVars.ShroomProjectiles.Contains(projectile.type) && Main.projectile[projectile.whoAmI].ai[0] == 0)
            {
                Main.projectile[projectile.whoAmI].scale *= 1.5f;
                Main.projectile[projectile.whoAmI].penetrate = -1;
                Main.projectile[projectile.whoAmI].damage = (int)(Main.projectile[projectile.whoAmI].damage * 1.5f);
                Main.projectile[projectile.whoAmI].ai[0] = 1;
            }
        }
        #endregion
    }
}
