using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewDawn.Dusts
{
	public class White : ModDust
    {
        public static int maxLife = 3 * 60;
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = false;
            dust.noLight = true;
            dust.scale *= 2.5f;
            dust.frame = new Rectangle(0, 0, 8, 8);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += 1f / 30f;
            dust.scale *= .99f;
            float light = 0.8f;
            Lighting.AddLight(dust.position, light, light, light);
            if (dust.scale <= .4f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}