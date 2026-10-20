namespace Calamity2041EoL;

public partial class Eol_2041_Modify : GlobalNPC
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
    }
}
