using NewDawn.Control;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class TransmutationTable : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 64;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.createTile = ModContent.TileType<Tiles.TransmutationTable>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddRecipeGroup(RecipeManager.LargeGems);
            recipe.AddRecipeGroup(RecipeManager.DungeonTables);
            recipe.AddIngredient(ItemID.WaterCandle);
            recipe.AddIngredient(ItemID.Book);
            recipe.Register();

            //Dungeon Table Recipes
            //Green
            Recipe grecipe = Recipe.Create(1400);
            grecipe.AddIngredient(ItemID.Bone, 10);
            grecipe.AddIngredient(137, 24);
            grecipe.AddTile(16);
            grecipe.Register();
            //Blue
            Recipe brecipe = Recipe.Create(1397);
            brecipe.AddIngredient(ItemID.Bone, 10);
            brecipe.AddIngredient(134, 24);
            brecipe.AddTile(16);
            brecipe.Register();
            //Pink
            Recipe precipe = Recipe.Create(1403);
            precipe.AddIngredient(ItemID.Bone, 10);
            precipe.AddIngredient(139, 24);
            precipe.AddTile(16);
            precipe.Register();

            //Metal Transmutation!!
            //Ores
            //Copper to Tin
            Recipe cTrecipe = Recipe.Create(ItemID.TinOre, 3);
            cTrecipe.AddIngredient(ItemID.CopperOre, 3);
            cTrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cTrecipe.Register();

            //Tin to Copper
            Recipe crecipe = Recipe.Create(ItemID.CopperOre, 3);
            crecipe.AddIngredient(ItemID.TinOre, 3);
            crecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            crecipe.Register();

            //Tin to Iron
            Recipe irecipe = Recipe.Create(ItemID.IronOre, 3);
            irecipe.AddIngredient(ItemID.TinOre, 4);
            irecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            irecipe.Register();

            //Iron to Tin
            Recipe trecipe = Recipe.Create(ItemID.TinOre, 3);
            trecipe.AddIngredient(ItemID.IronOre, 3);
            trecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            trecipe.Register();

            //Iron to Lead
            Recipe lrecipe = Recipe.Create(ItemID.LeadOre, 3);
            lrecipe.AddIngredient(ItemID.IronOre, 3);
            lrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            lrecipe.Register();

            //Lead to Iron
            Recipe lIrecipe = Recipe.Create(ItemID.IronOre, 3);
            lIrecipe.AddIngredient(ItemID.LeadOre, 3);
            lIrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            lIrecipe.Register();

            //Lead to Silver
            Recipe lSrecipe = Recipe.Create(ItemID.SilverOre, 3);
            lSrecipe.AddIngredient(ItemID.LeadOre, 5);
            lSrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            lSrecipe.Register();

            //Silver to Lead
            Recipe sLrecipe = Recipe.Create(ItemID.LeadOre, 3);
            sLrecipe.AddIngredient(ItemID.SilverOre, 3);
            sLrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            sLrecipe.Register();

            //Silver to Tungsten
            Recipe sTrecipe = Recipe.Create(ItemID.TungstenOre, 4);
            sTrecipe.AddIngredient(ItemID.SilverOre, 4);
            sTrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            sTrecipe.Register();

            //Tungsten to Silver
            Recipe srecipe = Recipe.Create(ItemID.SilverOre, 4);
            srecipe.AddIngredient(ItemID.TungstenOre, 4);
            srecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            srecipe.Register();

            //Tungsten to Gold
            Recipe gRecipe = Recipe.Create(ItemID.GoldOre, 4);
            gRecipe.AddIngredient(ItemID.TungstenOre, 5);
            gRecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            gRecipe.Register();

            //Gold to Tungsten
            Recipe gTrecipe = Recipe.Create(ItemID.TungstenOre, 4);
            gTrecipe.AddIngredient(ItemID.GoldOre, 4);
            gTrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            gTrecipe.Register();

            //Gold to Platinum
            Recipe gPrecipe = Recipe.Create(ItemID.PlatinumOre, 4);
            gPrecipe.AddIngredient(ItemID.GoldOre, 4);
            gPrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            gPrecipe.Register();

            //Platinum to Gold
            Recipe pGrecipe = Recipe.Create(ItemID.GoldOre, 4);
            pGrecipe.AddIngredient(ItemID.PlatinumOre, 4);
            pGrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            pGrecipe.Register();

            //Platinum to Cobalt
            Recipe pCrecipe = Recipe.Create(ItemID.CobaltOre, 4);
            pCrecipe.AddIngredient(ItemID.PlatinumOre, 6);
            pCrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            pCrecipe.AddTile(ModContent.TileType<Tiles.MineralEnchanterTile>());
            pCrecipe.Register();

            //Cobalt to Platinum
            Recipe cPrecipe = Recipe.Create(ItemID.PlatinumOre, 3);
            cPrecipe.AddIngredient(ItemID.CobaltOre, 3);
            cPrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cPrecipe.Register();

            //Cobalt to Palladium
            Recipe cParecipe = Recipe.Create(ItemID.PalladiumOre, 3);
            cParecipe.AddIngredient(ItemID.CobaltOre, 3);
            cParecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cParecipe.Register();

            //Palladium to Cobalt
            Recipe paCrecipe = Recipe.Create(ItemID.CobaltOre, 3);
            paCrecipe.AddIngredient(ItemID.PalladiumOre, 3);
            paCrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            paCrecipe.Register();

            //Palladium to Mythril
            Recipe pMrecipe = Recipe.Create(ItemID.MythrilOre, 3);
            pMrecipe.AddIngredient(ItemID.PalladiumOre, 5);
            pMrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            pMrecipe.Register();

            //Mythril to Palladium
            Recipe mPrecipe = Recipe.Create(ItemID.PalladiumOre, 3);
            mPrecipe.AddIngredient(ItemID.MythrilOre, 3);
            mPrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            mPrecipe.Register();

            //Mythril to Orichalcum
            Recipe mOrecipe = Recipe.Create(ItemID.OrichalcumOre, 4);
            mOrecipe.AddIngredient(ItemID.MythrilOre, 4);
            mOrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            mOrecipe.Register();

            //Orichalcum to Mythril
            Recipe oMrecipe = Recipe.Create(ItemID.MythrilOre, 4);
            oMrecipe.AddIngredient(ItemID.OrichalcumOre, 4);
            oMrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            oMrecipe.Register();

            //Orichalcum to Adamantite
            Recipe oArecipe = Recipe.Create(ItemID.AdamantiteOre, 4);
            oArecipe.AddIngredient(ItemID.OrichalcumOre, 6);
            oArecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            oArecipe.Register();

            //Adamantite to Orichalcum
            Recipe aOrecipe = Recipe.Create(ItemID.OrichalcumOre, 4);
            aOrecipe.AddIngredient(ItemID.AdamantiteOre, 4);
            aOrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            aOrecipe.Register();

            //Adamantite to Titanium
            Recipe aTrecipe = Recipe.Create(ItemID.TitaniumOre, 5);
            aTrecipe.AddIngredient(ItemID.AdamantiteOre, 5);
            aTrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            aTrecipe.Register();

            //Titanium to Adamantite
            Recipe arecipe = Recipe.Create(ItemID.AdamantiteOre, 5);
            arecipe.AddIngredient(ItemID.TitaniumOre, 5);
            arecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            arecipe.Register();

            //Bars
            //Copper to Tin
            Recipe ctBrecipe = Recipe.Create(ItemID.TinBar, 3);
            ctBrecipe.AddIngredient(ItemID.CopperBar, 3);
            ctBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            ctBrecipe.Register();

            //Tin to Copper
            Recipe tcBrecipe = Recipe.Create(ItemID.CopperBar, 3);
            tcBrecipe.AddIngredient(ItemID.TinBar, 3);
            tcBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            tcBrecipe.Register();

            //Tin to Iron
            Recipe tiBrecipe = Recipe.Create(ItemID.IronBar, 3);
            tiBrecipe.AddIngredient(ItemID.TinBar, 4);
            tiBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            tiBrecipe.Register();

            //Iron to Tin
            Recipe itBrecipe = Recipe.Create(ItemID.TinBar, 3);
            itBrecipe.AddIngredient(ItemID.IronBar, 3);
            itBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            itBrecipe.Register();

            //Iron to Lead
            Recipe ilBrecipe = Recipe.Create(ItemID.LeadBar, 3);
            ilBrecipe.AddIngredient(ItemID.IronBar, 3);
            ilBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            ilBrecipe.Register();

            //Lead to Iron
            Recipe liBrecipe = Recipe.Create(ItemID.IronBar, 3);
            liBrecipe.AddIngredient(ItemID.LeadBar, 3);
            liBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            liBrecipe.Register();

            //Lead to Silver
            Recipe lsBrecipe = Recipe.Create(ItemID.SilverBar, 3);
            lsBrecipe.AddIngredient(ItemID.LeadBar, 5);
            lsBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            lsBrecipe.Register();

            //Silver to Lead
            Recipe slBrecipe = Recipe.Create(ItemID.LeadBar, 3);
            slBrecipe.AddIngredient(ItemID.SilverBar, 3);
            slBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            slBrecipe.Register();

            //Silver to Tungsten
            Recipe stBrecipe = Recipe.Create(ItemID.TungstenBar, 4);
            stBrecipe.AddIngredient(ItemID.SilverBar, 4);
            stBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            stBrecipe.Register();

            //Tungsten to Silver
            Recipe tsBrecipe = Recipe.Create(ItemID.SilverBar, 4);
            tsBrecipe.AddIngredient(ItemID.TungstenBar, 4);
            tsBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            tsBrecipe.Register();

            //Tungsten to Gold
            Recipe tgBrecipe = Recipe.Create(ItemID.GoldBar, 4);
            tgBrecipe.AddIngredient(ItemID.TungstenBar, 5);
            tgBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            tgBrecipe.Register();

            //Gold to Tungsten
            Recipe gtBrecipe = Recipe.Create(ItemID.TungstenBar, 4);
            gtBrecipe.AddIngredient(ItemID.GoldBar, 4);
            gtBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            gtBrecipe.Register();

            //Gold to Platinum
            Recipe gpBrecipe = Recipe.Create(ItemID.PlatinumBar, 4);
            gpBrecipe.AddIngredient(ItemID.GoldBar, 4);
            gpBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            gpBrecipe.Register();

            //Platinum to Gold
            Recipe pgBrecipe = Recipe.Create(ItemID.GoldBar, 4);
            pgBrecipe.AddIngredient(ItemID.PlatinumBar, 4);
            pgBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            pgBrecipe.Register();

            //Platinum to Cobalt
            Recipe pcBrecipe = Recipe.Create(ItemID.CobaltBar, 4);
            pcBrecipe.AddIngredient(ItemID.PlatinumBar, 6);
            pcBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            pcBrecipe.AddTile(ModContent.TileType<Tiles.MineralEnchanterTile>());
            pcBrecipe.Register();

            //Cobalt to Platinum
            Recipe cpBrecipe = Recipe.Create(ItemID.PlatinumBar, 3);
            cpBrecipe.AddIngredient(ItemID.CobaltBar, 3);
            cpBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cpBrecipe.Register();

            //Cobalt to Palladium
            Recipe cPaBrecipe = Recipe.Create(ItemID.PalladiumBar, 3);
            cPaBrecipe.AddIngredient(ItemID.CobaltBar, 3);
            cPaBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cPaBrecipe.Register();

            //Palladium to Cobalt
            Recipe paCBrecipe = Recipe.Create(ItemID.CobaltBar, 3);
            paCBrecipe.AddIngredient(ItemID.PalladiumBar, 3);
            paCBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            paCBrecipe.Register();

            //Palladium to Mythril
            Recipe pmBrecipe = Recipe.Create(ItemID.MythrilBar, 3);
            pmBrecipe.AddIngredient(ItemID.PalladiumBar, 5);
            pmBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            pmBrecipe.Register();

            //Mythril to Palladium
            Recipe mpBrecipe = Recipe.Create(ItemID.PalladiumBar, 3);
            mpBrecipe.AddIngredient(ItemID.MythrilBar, 3);
            mpBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            mpBrecipe.Register();

            //Mythril to Orichalcum
            Recipe moBrecipe = Recipe.Create(ItemID.OrichalcumBar, 4);
            moBrecipe.AddIngredient(ItemID.MythrilBar, 4);
            moBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            moBrecipe.Register();

            //Orichalcum to Mythril
            Recipe omBrecipe = Recipe.Create(ItemID.MythrilBar, 4);
            omBrecipe.AddIngredient(ItemID.OrichalcumBar, 4);
            omBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            omBrecipe.Register();

            //Orichalcum to Adamantite
            Recipe oaBrecipe = Recipe.Create(ItemID.AdamantiteBar, 4);
            oaBrecipe.AddIngredient(ItemID.OrichalcumBar, 6);
            oaBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            oaBrecipe.Register();

            //Adamantite to Orichalcum
            Recipe aoBrecipe = Recipe.Create(ItemID.OrichalcumBar, 4);
            aoBrecipe.AddIngredient(ItemID.AdamantiteBar, 4);
            aoBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            aoBrecipe.Register();

            //Adamantite to Titanium
            Recipe atBrecipe = Recipe.Create(ItemID.TitaniumBar, 5);
            atBrecipe.AddIngredient(ItemID.AdamantiteBar, 5);
            atBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            atBrecipe.Register();

            //Titanium to Adamantite
            Recipe taBrecipe = Recipe.Create(ItemID.AdamantiteBar, 5);
            taBrecipe.AddIngredient(ItemID.TitaniumBar, 5);
            taBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            taBrecipe.Register();


            //Crimson to Corruption

            //Undertaker to Musket
            Recipe uMrecipe = Recipe.Create(ItemID.Musket);
            uMrecipe.AddIngredient(ItemID.TheUndertaker);
            uMrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            uMrecipe.Register();

            //Musket to Undertaker
            Recipe mUrecipe = Recipe.Create(ItemID.TheUndertaker);
            mUrecipe.AddIngredient(ItemID.Musket);
            mUrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            mUrecipe.Register();

            //Rotted Fork to Ball o' Hurt
            Recipe rBrecipe = Recipe.Create(162);
            rBrecipe.AddIngredient(802);
            rBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            rBrecipe.Register();

            //Ball o' Hurt to Rotted Fork
            Recipe bRrecipe = Recipe.Create(802);
            bRrecipe.AddIngredient(162);
            bRrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            bRrecipe.Register();

            //CrimsonRod to Vilethorn
            Recipe cVrecipe = Recipe.Create(ItemID.Vilethorn);
            cVrecipe.AddIngredient(ItemID.CrimsonRod);
            cVrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cVrecipe.Register();

            //Vilethorn to CrimsonRod
            Recipe vCrecipe = Recipe.Create(ItemID.CrimsonRod);
            vCrecipe.AddIngredient(ItemID.Vilethorn);
            vCrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            vCrecipe.Register();

            //Panic Necklace to Band of Starpower
            Recipe pBrecipe = Recipe.Create(111);
            pBrecipe.AddIngredient(ItemID.PanicNecklace);
            pBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            pBrecipe.Register();

            //Band of Starpower to Panic Necklace
            Recipe bPrecipe = Recipe.Create(ItemID.PanicNecklace);
            bPrecipe.AddIngredient(111);
            bPrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            bPrecipe.Register();

            //Crimson Heart to Shadow Orb
            Recipe cSrecipe = Recipe.Create(ItemID.ShadowOrb);
            cSrecipe.AddIngredient(3062);
            cSrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cSrecipe.Register();

            //Shadow Orb to Crimson Heart
            Recipe sCrecipe = Recipe.Create(3062);
            sCrecipe.AddIngredient(ItemID.ShadowOrb);
            sCrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            sCrecipe.Register();

            //Demonite to Crimtane
            Recipe dCrecipe = Recipe.Create(ItemID.CrimtaneOre, 3);
            dCrecipe.AddIngredient(ItemID.DemoniteOre, 3);
            dCrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            dCrecipe.Register();

            //Crimtane to Demonite
            Recipe cDrecipe = Recipe.Create(ItemID.DemoniteOre, 3);
            cDrecipe.AddIngredient(ItemID.CrimtaneOre, 3);
            cDrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cDrecipe.Register();

            //CrimtaneBar to DemoniteBar
            Recipe cdBrecipe = Recipe.Create(ItemID.DemoniteBar, 3);
            cdBrecipe.AddIngredient(ItemID.CrimtaneBar, 3);
            cdBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cdBrecipe.Register();

            //DemoniteBar to CrimtaneBar
            Recipe dcBrecipe = Recipe.Create(ItemID.CrimtaneBar, 3);
            dcBrecipe.AddIngredient(ItemID.DemoniteBar, 3);
            dcBrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            dcBrecipe.Register();

            //Ebonstone to Crimstone
            Recipe eCrecipe = Recipe.Create(ItemID.CrimstoneBlock);
            eCrecipe.AddIngredient(ItemID.EbonstoneBlock);
            eCrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            eCrecipe.Register();

            //Crimstone to Ebonstone
            Recipe cErecipe = Recipe.Create(ItemID.EbonstoneBlock);
            cErecipe.AddIngredient(ItemID.CrimstoneBlock);
            cErecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cErecipe.Register();

            //CursedFlame to Ichor
            Recipe cIrecipe = Recipe.Create(ItemID.Ichor);
            cIrecipe.AddIngredient(ItemID.CursedFlame);
            cIrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cIrecipe.Register();

            //Ichor to CursedFlame
            Recipe cRecipe = Recipe.Create(ItemID.CursedFlame);
            cRecipe.AddIngredient(ItemID.Ichor);
            cRecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cRecipe.Register();

            //Ebonkoi to Hemopiranha
            Recipe eHrecipe = Recipe.Create(ItemID.Hemopiranha);
            eHrecipe.AddIngredient(ItemID.Ebonkoi);
            eHrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            eHrecipe.Register();

            //Hemopiranha to Ebonkoi
            Recipe hErecipe = Recipe.Create(ItemID.Ebonkoi);
            hErecipe.AddIngredient(ItemID.Hemopiranha);
            hErecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            hErecipe.Register();

            //Ebonkoi to CrimsonTigerfish
            Recipe eTrecipe = Recipe.Create(ItemID.CrimsonTigerfish);
            eTrecipe.AddIngredient(ItemID.Ebonkoi);
            eTrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            eTrecipe.Register();

            //CrimsonTigerfish to Ebonkoi
            Recipe erecipe = Recipe.Create(ItemID.Ebonkoi);
            erecipe.AddIngredient(ItemID.CrimsonTigerfish);
            erecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            erecipe.Register();

            //CrimsonSeeds to CorruptSeeds
            Recipe cCrecipe = Recipe.Create(ItemID.CorruptSeeds);
            cCrecipe.AddIngredient(ItemID.CrimsonSeeds);
            cCrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cCrecipe.Register();

            //CorruptSeeds to CrimsonSeeds
            Recipe cCsrecipe = Recipe.Create(ItemID.CrimsonSeeds);
            cCsrecipe.AddIngredient(ItemID.CorruptSeeds);
            cCsrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            cCsrecipe.Register();

            //Crafting Life Fruits
            Recipe lFrecipe = Recipe.Create(ItemID.LifeFruit);
            lFrecipe.AddIngredient(ItemID.LifeCrystal);
            lFrecipe.AddIngredient(1006, 5);
            lFrecipe.AddTile(ModContent.TileType<Tiles.TransmutationTable>());
            lFrecipe.Register();
        }
    }
}