using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Boots
{
    public class BootPar : ModItem
    {
        private int _dashDelay = 0;
        private int _dashCooldown = 0;
        private int _rocketTime = 0;
        private int _trail = 0;
        private int _vertDashCount = 0;
        private BootDir _dashDir;
        public enum BootDir
        {
            NONE,
            RIGHT,
            UP,
            LEFT,
            DOWN
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boot");
            Tooltip.SetDefault("Put it on your feet");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.value = 100;
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
            _dashDelay = 0;
            _dashCooldown = 0;
            _rocketTime = 0;
            _dashDir = BootDir.NONE;
            _trail = 0;
            _vertDashCount = 0;
            RocketAccelCurrent = 0;
            SetBaseDefaults();
            UpdateBootStats();
        }

        private void SetBaseDefaults()
        {
            CanDash = false;
            MaxVertDash = 0;
            RunDust = 0;
            RunDustScale = 2;
            FlyDust = 0;
            FlyDustScale = 1;
            DashCooldownMax = 0;
            DashSpeed = 0;
            DashBurstNum = 0;
            TrailDust = 0;
            TrailMax = 0;
            RocketTimeMax = 0;
            RocketAccelCurrent = 0;
            RocketAccelSpeed = 0;
            RocketAccelMax = 0;
            MaxVel = 0;
            JumpBoost = 0;
            MoveBoost = 0;
            MaxRun = 0;
        }

        public override void HoldItem(Player player)
        {
            UpdateBootStats();
        }

        public virtual void UpdateBootStats()
        {

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            GetBonuses(player, hideVisual);
            Run(player, hideVisual);
            GetRocketBoots(player, hideVisual);
            Dashes(player, hideVisual);
            Trail(player, hideVisual);
            Delays(player, hideVisual);
        }

        public virtual void GetBonuses(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += JumpBoost;
            player.moveSpeed += MoveBoost;
            player.maxRunSpeed += MaxRun;
        }

        public virtual void Run(Player player, bool hideVisual)
        {
            if (RunDust != 0 &&  Main.tileSolid[Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].type] && Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].type != 0 && Math.Abs(player.velocity.X) > 4 && Main.rand.Next(3) == 0)
            {
                int newDust = Dust.NewDust(new Vector2(player.Center.X + player.velocity.X, player.position.Y + 4 + player.height - 10f + player.velocity.Y), 8, 8, RunDust, 0f, 0f, 0);
                Main.dust[newDust].scale = RunDustScale;
            }
        }

        public virtual void GetRocketBoots(Player player, bool hideVisual)
        {
            if (player.controlJump && _rocketTime < RocketTimeMax)
            {
                if (RocketAccelCurrent < RocketAccelMax)
                    RocketAccelCurrent += RocketAccelSpeed;
                if (player.velocity.Y > -MaxVel)
                    player.velocity.Y -= RocketAccelCurrent;
                if (player.velocity.Y > 0)
                    player.velocity.Y *= .8f;
                RocketDust(player, hideVisual);
                _rocketTime++;
                player.fallStart = (int)player.position.Y / 16;
                if (player.rocketDelay2 <= 0)
                {
                    Main.PlaySound(SoundID.Item24, player.position);
                    player.rocketDelay2 = 60 / 4;
                }
            }
            else
                RocketAccelCurrent = RocketAccelMax;
        }

        public virtual void Dashes(Player player, bool hideVisual)
        {
            if (!CanDash)
                return;
            if (!player.mount.Active && player.grappling[0] == -1 && _dashCooldown <= 0)
            {
                if (player.controlRight && player.releaseRight)
                {
                    if (_dashDelay > 0 && _dashDir == BootDir.RIGHT)
                    {
                        _dashCooldown = DashCooldownMax;
                        _trail = TrailMax;
                        player.velocity.X = DashSpeed;
                        _dashDir = BootDir.NONE;
                        DustBurst(player, TrailDust, DashBurstNum);
                    }
                    else
                    {
                        _dashDelay = 60 / 4;
                        _dashDir = BootDir.RIGHT;
                    }
                }
                if (player.controlLeft && player.releaseLeft)
                {
                    if (_dashDelay > 0 && _dashDir == BootDir.LEFT)
                    {
                        _dashCooldown = DashCooldownMax;
                        _trail = TrailMax;
                        player.velocity.X = -DashSpeed;
                        _dashDir = BootDir.NONE;
                        DustBurst(player, TrailDust, DashBurstNum);
                    }
                    else
                    {
                        _dashDelay = 60 / 4;
                        _dashDir = BootDir.LEFT;
                    }
                }
                if (player.controlDown && player.releaseDown && _vertDashCount < MaxVertDash)
                {
                    if (_dashDelay > 0 && _dashDir == BootDir.DOWN)
                    {
                        _vertDashCount++;
                        _dashCooldown = DashCooldownMax;
                        _trail = TrailMax;
                        player.velocity.Y = 2 * DashSpeed;
                        player.fallStart = (int)player.position.Y / 16;
                        _dashDir = BootDir.NONE;
                        DustBurst(player, TrailDust, DashBurstNum);
                    }
                    else
                    {
                        _dashDelay = 60 / 4;
                        _dashDir = BootDir.LEFT;
                    }
                }
                if (player.controlUp && player.releaseUp && _vertDashCount < MaxVertDash)
                {
                    if (_dashDelay > 0 && _dashDir == BootDir.UP)
                    {
                        _vertDashCount++;
                        _dashCooldown = DashCooldownMax;
                        _trail = TrailMax;
                        player.velocity.Y = -DashSpeed;
                        _dashDir = BootDir.NONE;
                        player.fallStart = (int)player.position.Y / 16;
                        DustBurst(player, TrailDust, DashBurstNum);
                    }
                    else
                    {
                        _dashDelay = 60 / 4;
                        _dashDir = BootDir.UP;
                    }
                }
            }
        }

        public virtual void RocketDust(Player player, bool hideVisual)
        {
            if (FlyDust == 0)
                return;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    int newDust = Dust.NewDust(new Vector2(player.position.X - 4f + player.velocity.X, player.position.Y + (float)player.height - 10f + player.velocity.Y), 8, 8, FlyDust, 0f, 0f, 0, default(Color), FlyDustScale);
                    Main.dust[newDust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    Main.dust[newDust].velocity.X = (Main.dust[newDust].velocity.X * 1f - 2f - player.velocity.X * 0.3f) / 2;
                    Main.dust[newDust].velocity.Y = (Main.dust[newDust].velocity.Y * 1f + 2f * player.gravDir - player.velocity.Y * 0.3f) / 2;
                    Main.dust[newDust].noGravity = true;
                }
                else
                {
                    int newDust = Dust.NewDust(new Vector2(player.position.X + (float)player.width - 4f + player.velocity.X, player.position.Y + (float)player.height - 10f + player.velocity.Y), 8, 8, FlyDust, 0f, 0f, 0, default(Color), FlyDustScale);
                    Main.dust[newDust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    Main.dust[newDust].velocity.X = (Main.dust[newDust].velocity.X * 1f + 2f - player.velocity.X * 0.3f) / 2;
                    Main.dust[newDust].velocity.Y = (Main.dust[newDust].velocity.Y * 1f + 2f * player.gravDir - player.velocity.Y * 0.3f) / 2;
                    Main.dust[newDust].noGravity = true;
                }
            }
        }

        public virtual void DustBurst(Player player, int dustType, int amount)
        {
            if (dustType == 0)
                return;
            for (int i = 0; i < amount; i++)
            {
                float theta = Main.rand.NextFloat() * (float)Math.PI * 2;
                Dust.NewDust(player.position + player.velocity, player.width, player.height, dustType, (float)Math.Cos(theta) * 6, (float)Math.Sin(theta) * 6);
            }
        }

        public virtual void Trail(Player player, bool hideVisual)
        {
            if(_trail > 0 && TrailDust != 0)
            {
                _trail--;
                Dust.NewDust(player.position + player.velocity, player.width, player.height, TrailDust, 0, 0);
            }
        }

        public virtual void Delays(Player player, bool hideVisual)
        {
            if (_dashDelay > 0)
                _dashDelay--;
            if (_dashCooldown > 0)
                _dashCooldown--;
            if (Main.tileSolid[Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].type] && Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].type != 0 && Math.Abs(player.velocity.Y) < .25f)
            {
                _rocketTime = 0;
                _vertDashCount = 0;
            }
            if (player.grappling[0] != -1)
            {
                _rocketTime = 0;
            }
        }


        public bool CanDash { get; set; }
        public int MaxVertDash { get; set; }
        public int RunDust { get; set; }
        public int RunDustScale { get; set; }
        public int FlyDust { get; set; }
        public int FlyDustScale { get; set; }
        public int DashCooldownMax { get; set; }
        public int DashSpeed { get; set; }
        public int DashBurstNum { get; set; }
        public int TrailDust { get; set; }
        public int TrailMax { get; set; }
        public int RocketTimeMax { get; set; }
        public float RocketAccelCurrent { get; set; }
        public float RocketAccelSpeed { get; set; }
        public float RocketAccelMax { get; set; }
        public float MaxVel { get; set; }
        public int JumpBoost { get; set; }
        public float MoveBoost { get; set; }
        public float MaxRun { get; set; }
    }
}