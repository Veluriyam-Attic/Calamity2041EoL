using CalRD.Buffs.Placeables;
using CalRD.NPCs.Providence;
using CalRD.NPCs.SupremeCalamitas;
using Terraria.WorldBuilding;

namespace Calamity2041EoL.PartialGlobalNPC;

public partial class GlobalNPC_2041 : GlobalNPC
{
    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        // 计算白天的动态伤害减免
        CalcuDayTDR(npc,ref modifiers);
    }

    #region 计算白天的动态伤害减免
    private static MethodInfo DefaultDRMath = null;
    private void CalcuDayTDR(NPC npc, ref NPC.HitModifiers modifiers)
    {
        CalamityGlobalNPC cNPC = npc.Calamity();

        if (cNPC.DR <= 0f && cNPC.KillTime == 0)
            return;

        float finalMultiplier = 1f;

        // If the NPC currently has unbreakable DR, it cannot be reduced by any means.
        // If custom DR is enabled, use that instead of normal DR.
        float effectiveDR = (float)DefaultDRMath.Invoke(cNPC, [npc, cNPC.DR]);

        // DR floor is 0%. Nothing can have negative DR.
        if (effectiveDR <= 0f)
            effectiveDR = 0f;

        // Add Yellow Candle damage if the NPC isn't supposed to be "near invincible"
        // Armor penetration has already been applied as bonus damage.
        // Yellow Candle provides +5% damage which ignores both DR and defense.
        // This means Yellow Candle is buffing armor penetration and technically not ignoring defense,
        // but it's small enough to let it slide.
        if (npc.HasBuff<YellowDamageCandle>() && cNPC.DR < 0.99f && npc.takenDamageMultiplier > 0.05f)
            finalMultiplier += 0.05f;

        bool dayEmpress = npc.type == NPCID.HallowBoss && NPC.ShouldEmpressBeEnraged();
        if (cNPC.KillTime > 0 && cNPC.AITimer < cNPC.KillTime && !BossRushEvent.BossRushActive && dayEmpress)
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

        // Final DR calculation
        finalMultiplier -= effectiveDR;

        modifiers.FinalDamage *= finalMultiplier;
    }
    #endregion
}
