using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WebmilioCommons;
using WebmilioCommons.Extensions;

namespace NewDawn.Items.AnimaCrystals
{
    public abstract class AnimaCrystal : ModItem
    {
        private Texture2D _texture;
        private string _specificTexture = "";
        private Texture2D _glowmask;

        public override void SetStaticDefaults()
        {

        }

        public sealed override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = item.useAnimation = 30;
            item.accessory = true;
            item.rare = ItemRarityID.Blue;
            item.uniqueStack = true;
            Essences = new List<Essence>();

            SetCrystalDefaults();
        }

        public virtual void SetCrystalDefaults()
        {
        }


        public void AddEssence(Essence essence)
        {
            if (Essences.Count < EssenceCapacity && essence.Level <= MaxEssenceLevel)
                Essences.Add(essence);

            if (Essences.Count == EssenceCapacity)
                GetPotionCrystalTexture();
        }

        private string GetRemainingEssences()
        {
            string tip = "";
            int numEssences = Essences.Count;
            int slotsLeft = (EssenceCapacity - numEssences);
            if (slotsLeft > 0)
                tip = "Can hold " + slotsLeft + " more Essence" + ((slotsLeft != 1) ? "s" : "");

            return tip;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (GetRemainingEssences() != "")
                tooltips.Add(new TooltipLine(mod, "ListEssences", GetRemainingEssences()));

            for (int i = 0; i < Essences.Count; i++)
            {
                tooltips.Add(new TooltipLine(mod, "ListEssences", "-" + Essences[i].Name + "-")
                {
                    overrideColor = Essences[i].EssenceColor
                });
                tooltips.Add(new TooltipLine(mod, "ListEssences", Essences[i].EffectDescription)
                {
                    overrideColor = Essences[i].EssenceColor
                });
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Essences.Count > 0)
            {
                foreach (Essence essence in Essences)
                {
                    essence.Effect(player);
                }
            }
        }

        public Color GetEssenceColor()
        {
            short red = 0;
            short green = 0;
            short blue = 0;

            if (Essences.Count > 0)
            {

                for (int i = 0; i < Essences.Count; i++)
                {
                    red += Essences[i].EssenceColor.R;
                    green += Essences[i].EssenceColor.G;
                    blue += Essences[i].EssenceColor.B;
                }

                return new Color(red / Essences.Count, green / Essences.Count, blue / Essences.Count);
            }

            return Color.White;
        }

        public override TagCompound Save()
        {
            var essenceNames = new List<string>(Essences.Count);
            Essences.ForEach(e => essenceNames.Add(e.UnlocalizedName));

            TagCompound tag = new TagCompound
            {
                {"Essences", essenceNames}
            };

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            var essenceNames = tag.GetList<string>("Essences");
            Essences = new List<Essence>(essenceNames.Count);


            essenceNames.Do(en => Essences.Add(EssenceLoader.Instance.New(en)));
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (_texture == null)
            {
                GetPotionCrystalTexture();
                _texture = GetType().GetTexture();
                if (mod.TextureExists(_specificTexture))
                    _texture = mod.GetTexture(_specificTexture);
                _glowmask = mod.GetTexture(GetType().GetPath() + "_glowmask");
            }

            var widthDiff = (GetType().GetTexture().Width - _texture.Width) / 2;
            var heightDiff = (GetType().GetTexture().Height - _texture.Height) / 2;
            var newPos = position;
            newPos.X += widthDiff;
            newPos.Y += heightDiff;

            spriteBatch.Draw(_texture, newPos, new Rectangle(0, 0, _texture.Width, _texture.Height), drawColor, 0, origin, scale, SpriteEffects.None, 0);
            
            if (!mod.TextureExists(_specificTexture) || Essences.Count < EssenceCapacity)
                spriteBatch.Draw(_glowmask, position, frame, GetEssenceColor(), 0, origin, scale, SpriteEffects.None, 0);
                
            return false;
        }

        private void GetPotionCrystalTexture()
        {
            StringBuilder sb = new StringBuilder(GetType().GetRootPath() + "/SpecificCrystals/");
            List<Essence> tempEss = Essences;
            
            tempEss.Sort();
            tempEss.Do(es => sb.Append(es.Name));
            
            _specificTexture = sb.ToString();
            _texture = null;
        }

        public override ModItem Clone(Item item)
        {
            ModItem newItem = item.modItem;
            //((AnimaCrystal)newItem).Essences = ((AnimaCrystal)item.modItem).Essences;

            return newItem;
        }


        public abstract int EssenceCapacity { get; }
        public List<Essence> Essences { get; set; } = new List<Essence>();
        public abstract Essence.Levels MaxEssenceLevel { get; }
    }
}
