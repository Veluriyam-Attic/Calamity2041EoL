namespace Calamity2041EoL.PartialGlobalNPC;

public partial class GlobalNPC_2041 : GlobalNPC
{
    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        CalcuDayTDR(npc, ref modifiers);
    }

    #region 计算白天的动态伤害减免
    private static MethodInfo DefaultDRMath = null;

    private void CalcuDayTDR(NPC npc, ref NPC.HitModifiers modifiers)
    {
        CalamityGlobalNPC cNPC = npc.Calamity();

        if (cNPC.KillTime <= 0 || cNPC.AITimer >= cNPC.KillTime || BossRushEvent.BossRushActive || !NPC.ShouldEmpressBeEnraged())
            return;

        float baseDR = (float)DefaultDRMath.Invoke(cNPC, [npc, cNPC.DR]);

        // CalRD 的 ApplyDR 已经先套过一遍 1.5x 通用限时 DR。
        // 这里把“通用 1.5x”还原成“光女 10x”，避免重复叠乘。
        float calRDGenericExtra = GetTimedDRExtra(npc, cNPC, 1.5f);
        float desiredExtra = GetTimedDRExtra(npc, cNPC, 10f);

        float calRDTotal = MathHelper.Clamp(baseDR + calRDGenericExtra, 0f, 0.999f);
        float desiredTotal = MathHelper.Clamp(baseDR + desiredExtra, 0f, 0.999f);

        float correction = (1f - desiredTotal) / (1f - calRDTotal);
        modifiers.FinalDamage *= correction;
    }

    private static float GetTimedDRExtra(NPC npc, CalamityGlobalNPC cNPC, float drScalar)
    {
        float extraDRLimit = (1f - cNPC.DR) * drScalar;
        float currentHPRatio = npc.life / (float)npc.lifeMax;
        float killTimeRatio = cNPC.AITimer / (float)cNPC.KillTime;
        float extraDRScalar = currentHPRatio + killTimeRatio;
        if (extraDRScalar >= 1f)
            return 0f;

        return extraDRLimit - (extraDRLimit / (1f + (1f - extraDRScalar)));
    }
    #endregion
}
