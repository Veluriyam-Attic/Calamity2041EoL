namespace Calamity2041EoL.PartialGlobalNPC;

public partial class GlobalNPC_2041 : GlobalNPC
{
    public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
        // 添加debuff
        target.AddBuff(BossRushEvent.BossRushActive || NPC.ShouldEmpressBeEnraged() ? ModContent.BuffType<HolyFlames>() : ModContent.BuffType<Nightwither>(), 240);
    }
}
