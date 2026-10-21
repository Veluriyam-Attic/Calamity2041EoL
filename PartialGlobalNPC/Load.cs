namespace Calamity2041EoL.PartialGlobalNPC;

public partial class GlobalNPC_2041 : GlobalNPC
{
    public override void Load()
    {
        // 提前反射一些东西存起来节省性能
        LoadReflections();

        // 光女专家接触倍率、接触伤害表、弹幕伤害表修改，哪个傻逼给设的internal
        Fuck_NPCStats_BossStats();
        // 动态伤害减免
        Fuck_TDR();
    }

    #region 光女专家接触倍率、接触伤害表、弹幕伤害表修改
    private static void Fuck_NPCStats_BossStats()
    {
        Type Type_NPCStats = typeof(NPCStats);
        Type Type_BossStats = Type_NPCStats.GetNestedType("BossStats", BindingFlags.Public | BindingFlags.NonPublic);
        FieldInfo FieldInfo_ExpertDamageMultiplier = Type_BossStats.GetField("ExpertDamageMultiplier", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        FieldInfo FieldInfo_ContactDamageValues = Type_BossStats.GetField("ContactDamageValues", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        FieldInfo FieldInfo_ProjectileDamageValues = Type_BossStats.GetField("ProjectileDamageValues", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        SortedDictionary<int, double> ExpertDamageMultiplier = FieldInfo_ExpertDamageMultiplier.GetValue(Type_BossStats) as SortedDictionary<int, double>;
        SortedDictionary<int, int[]> ContactDamageValues = FieldInfo_ContactDamageValues.GetValue(Type_BossStats) as SortedDictionary<int, int[]>;
        SortedDictionary<Tuple<int, int>, int[]> ProjectileDamageValues = FieldInfo_ProjectileDamageValues.GetValue(Type_BossStats) as SortedDictionary<Tuple<int, int>, int[]>;

        ExpertDamageMultiplier.Add(NPCID.HallowBoss, 0.6875f);
        ContactDamageValues.Add(NPCID.HallowBoss, 
            [
            80, // 120 during charge
            110,// 165 during charge
            143,// 215 during charge
            176,// 264 during charge
            231 // 347 during charge
            ]);
        ProjectileDamageValues.Add(new Tuple<int, int>(NPCID.HallowBoss, ProjectileID.HallowBossRainbowStreak), [100, 152, 172, 192, 300]);
        ProjectileDamageValues.Add(new Tuple<int, int>(NPCID.HallowBoss, ProjectileID.FairyQueenSunDance), [120, 180, 204, 228, 360]);
        ProjectileDamageValues.Add(new Tuple<int, int>(NPCID.HallowBoss, ProjectileID.HallowBossLastingRainbow), [100, 152, 172, 192, 300]);
        ProjectileDamageValues.Add(new Tuple<int, int>(NPCID.HallowBoss, ProjectileID.FairyQueenLance), [130, 196, 216, 236, 390]);

        FieldInfo_ExpertDamageMultiplier.SetValue(Type_BossStats, ExpertDamageMultiplier);
        FieldInfo_ContactDamageValues.SetValue(Type_BossStats, ContactDamageValues);
        FieldInfo_ProjectileDamageValues.SetValue(Type_BossStats, ProjectileDamageValues);
    }
    #endregion

    #region 动态伤害减免
    private static void Fuck_TDR()
    {
        CalRD.CalRD.bossKillTimes.Add(NPCID.HallowBoss, 10800);
        CalRD.CalRD.DRValues.Add(NPCID.HallowBoss, 0.15f);
    }
    #endregion

    #region 提前反射一些东西存起来节省性能
    private void LoadReflections()
    {
        Type Type_CalamityGlobalNPC = typeof(CalamityGlobalNPC);
        MethodInfo MethodInfo_DefaultDRMath = Type_CalamityGlobalNPC.GetMethod("DefaultDRMath", BindingFlags.Instance | BindingFlags.NonPublic);
        DefaultDRMath = MethodInfo_DefaultDRMath;
    }
    #endregion
}
