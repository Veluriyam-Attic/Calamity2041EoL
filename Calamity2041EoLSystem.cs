using Terraria.DataStructures;

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
}
