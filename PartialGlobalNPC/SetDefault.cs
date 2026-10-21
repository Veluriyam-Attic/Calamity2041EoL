namespace Calamity2041EoL.PartialGlobalNPC;

public partial class GlobalNPC_2041 : GlobalNPC
{
    public override void SetDefaults(NPC npc)
    {
        // 血量上限
        npc.lifeMax = 100000;
        // Boss价值喵
        npc.value = Item.buyPrice(gold:60);

        // 复仇模式更改喵
        if (CalamityWorld.revenge)
        {
            npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.2);
            npc.npcSlots = 32f;
        }

        // br的血量
        if (BossRushEvent.BossRushActive)
            npc.lifeMax = 200000;

        // 接触伤害
        npc.GetNPCDamage();
        npc.defDamage = npc.damage;

        // 免疫Buff喵
        npc.buffImmune[BuffID.OnFire] = true;
        npc.buffImmune[BuffID.OnFire3] = true;
        npc.buffImmune[ModContent.BuffType<HolyFlames>()] = true;
        npc.buffImmune[ModContent.BuffType<Nightwither>()] = true;
    }
}
