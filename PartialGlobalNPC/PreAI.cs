namespace Calamity2041EoL.PartialGlobalNPC;

public partial class GlobalNPC_2041 : GlobalNPC
{
    public override bool PreAI(NPC npc)
    {
        if ((CalamityWorld.revenge || BossRushEvent.BossRushActive))
        {
            return EmpressofLightAI.BuffedEmpressofLightAI(npc,Mod);
        }

        return EmpressofLightAI.VanillaEmpressofLightAI(npc, Mod) ;
    } 
}
