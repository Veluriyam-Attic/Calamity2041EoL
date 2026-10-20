namespace Calamity2041EoL;

public partial class Eol_2041_Modify : GlobalNPC
{
    public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.HallowBoss && lateInstantiation;
}
