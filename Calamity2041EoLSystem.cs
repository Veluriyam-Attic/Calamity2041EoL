namespace Calamity2041EoL;

public class Calamity2041EoLSystem : ModSystem
{
    public static bool LegendaryMode = false;

    public override void OnWorldLoad()
    {
        FieldInfo gameModeInfo = typeof(Main).GetField("_currentGameModeInfo", BindingFlags.Static | BindingFlags.NonPublic);
        LegendaryMode = Main.getGoodWorld && ((GameModeData)gameModeInfo.GetValue(null)).IsMasterMode;
    }

    public override void OnWorldUnload()
    {
        LegendaryMode = false;
    }

    public override void PostSetupContent()
    {
        // 女皇之翼：飞行时间 150 -> 100，WingStats(100, 8, 2)。
        ArmorIDs.Wing.Sets.Stats[44] = new WingStats(100, 8f, 2f); // 44 = Empress Wings
    }
    public override void PostAddRecipes()
    {
        // 2.0.4.1 元素斧改成消耗泰拉棱镜；CalRD 有配方系统。
        foreach (Recipe recipe in Main.recipe)
        {
            if (recipe.createItem.type == ModContent.ItemType<CalRD.Items.Weapons.Summon.ElementalAxe>())
                recipe.DisableRecipe();
        }

        Recipe.Create(ModContent.ItemType<CalRD.Items.Weapons.Summon.ElementalAxe>())
            .AddIngredient(ItemID.EmpressBlade)
            .AddIngredient<CalRD.Items.Weapons.Summon.PlantationStaff>()
            .AddIngredient(ItemID.LunarBar, 5)
            .AddIngredient<CalRD.Items.Materials.BarofLife>(5)
            .AddIngredient<CalRD.Items.Materials.GalacticaSingularity>(5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
    }
}