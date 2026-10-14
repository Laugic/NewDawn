using Microsoft.Xna.Framework;
using NewDawn.Tiles;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Worldgen
{
    public class FissioniteGeneration : GlobalTile
    {
        // Prevent the 2x2 Shadow Orb / Crimson Heart from triggering
        // the generation more than once during the same update.
        private static readonly HashSet<Point> processedOrbs = new();

        public override void KillTile(
            int i,
            int j,
            int type,
            ref bool fail,
            ref bool effectOnly,
            ref bool noItem)
        {
            if (type != TileID.ShadowOrbs)
                return;

            if (fail || effectOnly)
                return;

            // Only change the world on the server / singleplayer.
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            Tile tile = Main.tile[i, j];

            // Shadow Orbs and Crimson Hearts are 2x2.
            // Their individual pieces use 18x18 frames.
            int localFrameX = tile.TileFrameX % 36;
            int localFrameY = tile.TileFrameY % 36;

            int left = i - (localFrameX / 18);
            int top = j - (localFrameY / 18);

            Point orbPosition = new Point(left, top);

            // Multiple pieces may invoke KillTile.
            if (!processedOrbs.Add(orbPosition))
                return;

            int centerX = left + 1;
            int centerY = top + 1;

            GenerateOreBurst(centerX, centerY);
        }

        private static void GenerateOreBurst(int centerX, int centerY)
        {
            int oreType = ModContent.TileType<FissioniteTile>();

            // Number of separate veins shooting out from the orb.
            int veinCount = Main.rand.Next(3, 7);

            // Random initial rotation means every burst looks different.
            float startingAngle = Main.rand.NextFloat(MathHelper.TwoPi);

            // Keep track of the affected area for multiplayer syncing.
            int minX = centerX;
            int maxX = centerX;
            int minY = centerY;
            int maxY = centerY;

            for (int v = 0; v < veinCount; v++)
            {
                // Roughly distribute veins around the orb,
                // but don't make them perfectly symmetrical.
                float angle =
                    startingAngle +
                    (MathHelper.TwoPi * v / veinCount) +
                    Main.rand.NextFloat(-0.45f, 0.45f);

                int length = Main.rand.Next(10, 30);

                GenerateWindyVein(
                    centerX,
                    centerY,
                    angle,
                    length,
                    oreType,
                    ref minX,
                    ref maxX,
                    ref minY,
                    ref maxY
                );
            }

            // Sync the changed section with multiplayer clients.
            if (Main.netMode == NetmodeID.Server)
            {
                int padding = 4;

                minX -= padding;
                minY -= padding;
                maxX += padding;
                maxY += padding;

                minX = Math.Max(0, minX);
                minY = Math.Max(0, minY);
                maxX = Math.Min(Main.maxTilesX - 1, maxX);
                maxY = Math.Min(Main.maxTilesY - 1, maxY);

                NetMessage.SendTileSquare(
                    -1,
                    minX,
                    minY,
                    maxX - minX + 1,
                    maxY - minY + 1
                );
            }
        }

        private static void GenerateWindyVein(
            int startX,
            int startY,
            float angle,
            int length,
            int oreType,
            ref int minX,
            ref int maxX,
            ref int minY,
            ref int maxY)
        {
            Vector2 position = new Vector2(startX, startY);

            float currentAngle = angle;

            // How quickly the vein is currently bending.
            float turnVelocity = Main.rand.NextFloat(-0.03f, 0.03f);

            for (int step = 0; step < length; step++)
            {
                /*
                 * WINDINESS
                 *
                 * Instead of choosing a completely new direction every step,
                 * gently change the existing turning velocity.
                 *
                 * This produces smooth serpentine curves rather than
                 * noisy zig-zags.
                 */
                turnVelocity += Main.rand.NextFloat(-0.025f, 0.025f);

                // Stop curves from becoming circles.
                turnVelocity = MathHelper.Clamp(
                    turnVelocity,
                    -0.09f,
                    0.09f
                );

                currentAngle += turnVelocity;

                Vector2 direction = currentAngle.ToRotationVector2();

                position += direction;

                int x = (int)position.X;
                int y = (int)position.Y;

                if (!WorldGen.InWorld(x, y, 10))
                    break;

                /*
                 * Thickness varies down the length.
                 *
                 * Most of the vein is thin, occasionally swelling
                 * into small ore pockets.
                 */
                int radius;

                float roll = Main.rand.NextFloat();

                if (roll < 0.08f)
                    radius = 2;
                else if (roll < 0.40f)
                    radius = 1;
                else
                    radius = 0;

                PlaceOreCircle(
                    x,
                    y,
                    radius,
                    oreType,
                    ref minX,
                    ref maxX,
                    ref minY,
                    ref maxY
                );

                /*
                 * Occasionally fork off a tiny side spur.
                 */
                if (step > 5 &&
                    step < length - 5 &&
                    Main.rand.NextBool(18))
                {
                    float branchAngle =
                        currentAngle +
                        Main.rand.NextFloat(-1.1f, 1.1f);

                    GenerateSmallBranch(
                        position,
                        branchAngle,
                        Main.rand.Next(4, 10),
                        oreType,
                        ref minX,
                        ref maxX,
                        ref minY,
                        ref maxY
                    );
                }
            }
        }

        private static void GenerateSmallBranch(
            Vector2 position,
            float angle,
            int length,
            int oreType,
            ref int minX,
            ref int maxX,
            ref int minY,
            ref int maxY)
        {
            float turn = Main.rand.NextFloat(-0.05f, 0.05f);

            for (int step = 0; step < length; step++)
            {
                turn += Main.rand.NextFloat(-0.03f, 0.03f);
                turn = MathHelper.Clamp(turn, -0.12f, 0.12f);

                angle += turn;

                position += angle.ToRotationVector2();

                int x = (int)position.X;
                int y = (int)position.Y;

                if (!WorldGen.InWorld(x, y, 10))
                    return;

                PlaceOreCircle(
                    x,
                    y,
                    0,
                    oreType,
                    ref minX,
                    ref maxX,
                    ref minY,
                    ref maxY
                );
            }
        }

        private static void PlaceOreCircle(
            int centerX,
            int centerY,
            int radius,
            int oreType,
            ref int minX,
            ref int maxX,
            ref int minY,
            ref int maxY)
        {
            for (int x = centerX - radius; x <= centerX + radius; x++)
            {
                for (int y = centerY - radius; y <= centerY + radius; y++)
                {
                    if (!WorldGen.InWorld(x, y, 10))
                        continue;

                    // Gives radius-2 portions an actual round-ish shape.
                    if (Vector2.Distance(
                            new Vector2(x, y),
                            new Vector2(centerX, centerY))
                        > radius + 0.5f)
                    {
                        continue;
                    }

                    Tile tile = Main.tile[x, y];

                    if (!CanConvertToOre(tile))
                        continue;

                    tile.TileType = (ushort)oreType;

                    minX = Math.Min(minX, x);
                    maxX = Math.Max(maxX, x);
                    minY = Math.Min(minY, y);
                    maxY = Math.Max(maxY, y);

                    WorldGen.SquareTileFrame(x, y);
                }
            }
        }

        private static bool CanConvertToOre(Tile tile)
        {
            if (!tile.HasTile)
                return false;

            // Choose exactly what the magical burst is allowed to consume.
            return tile.TileType == TileID.Stone
                || tile.TileType == TileID.Ebonstone
                || tile.TileType == TileID.Crimstone;
        }
        public static void ClearProcessedOrbs()
        {
            processedOrbs.Clear();
        }
    }

    public class ShadowOrbOreBurstSystem : ModSystem
    {
        public override void PostUpdateWorld()
        {
            // By the next game update the orb destruction is finished,
            // so its position can safely trigger again if ever needed.
            FissioniteGeneration.ClearProcessedOrbs();
        }
    }
}
