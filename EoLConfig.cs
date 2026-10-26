namespace Calamity2041EoL;

public class EoLConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(true)]
    [ReloadRequired]
    public bool FightInBossRush;
}
