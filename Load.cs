namespace Calamity2041EoL;

public partial class Eol_2041_Modify : GlobalNPC
{
    public override void Load()
    {
        // 光女专家接触倍率、接触伤害表、弹幕伤害表修改，哪个傻逼给设的internal
        Fuck_NPCStats_BossStats();
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

        FieldInfo_ExpertDamageMultiplier.SetValue(Type_BossStats, ExpertDamageMultiplier);
        FieldInfo_ContactDamageValues.SetValue(Type_BossStats, ContactDamageValues);
        FieldInfo_ProjectileDamageValues.SetValue(Type_BossStats, ProjectileDamageValues);
    }
    #endregion
}
