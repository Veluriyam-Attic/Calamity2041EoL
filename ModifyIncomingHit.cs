namespace Calamity2041EoL;

public partial class Eol_2041_Modify : GlobalNPC
{
    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        // 计算白天的动态伤害减免
        CalcuDayTDR(npc);
    }

    #region 计算白天的动态伤害减免
    private static MethodInfo DefaultDRMath = null;
    private void CalcuDayTDR(NPC npc)
    {
        CalamityGlobalNPC cNPC = npc.Calamity();

        float effectiveDR = (float)DefaultDRMath.Invoke(cNPC, [npc, cNPC.DR]);

        if (cNPC.KillTime > 0 && cNPC.AITimer < cNPC.KillTime && !BossRushEvent.BossRushActive && (NPC.ShouldEmpressBeEnraged()))
        {
            // Set the DR scaling factor
            float DRScalar = 10f;

            // The limit for how much extra DR the boss can have
            float extraDRLimit = (1f - cNPC.DR) * DRScalar;

            // Ranges from 1 to 0
            float currentHPRatio = npc.life / (float)npc.lifeMax;

            // Ranges from 0 to 1
            float killTimeRatio = cNPC.AITimer / (float)cNPC.KillTime;

            // If the player is damaging the boss too quickly
            float extraDRScalar = currentHPRatio + killTimeRatio;
            if (extraDRScalar < 1f)
            {
                // Ranges from 0 to (extraDRLimit / 2)
                effectiveDR += extraDRLimit - (extraDRLimit / (1f + (1f - extraDRScalar)));
            }
        }
    }
    #endregion
}
