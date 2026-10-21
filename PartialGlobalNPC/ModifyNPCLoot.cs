namespace Calamity2041EoL.PartialGlobalNPC;

public partial class GlobalNPC_2041 : GlobalNPC
{
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        var empressRootRules = npcLoot.Get(false);
        IItemDropRule notExpert = empressRootRules.Find((rule) => rule is LeadingConditionRule empressLCR && empressLCR.condition is Conditions.NotExpert);
        if (notExpert is LeadingConditionRule LCR_NotExpert)
        {
            LCR_NotExpert.ChainedRules.RemoveAll((chainAttempt) =>
                chainAttempt is Chains.TryIfSucceeded c && c.RuleToChain is OneFromOptionsDropRule empressWeapons);

            // Define a replacement rule which drops the items Calamity style.
            // This includes her wings, because they have a pathetically low drop rate normally.
            var empressItems = new int[]
            {
                 ItemID.PiercingStarlight, // Starlight
                 ItemID.FairyQueenRangedItem, // Eventide
                 ItemID.FairyQueenMagicItem, // Nightglow
                 ItemID.SparkleGuitar, // Stellar Tune
                 ItemID.EmpressBlade, // Terraprisma
                 ItemID.RainbowWhip, // Kaleidoscope
                 ItemID.RainbowWings // Empress Wings
            };
            LCR_NotExpert.Add(DropHelper.CalamityStyle(new Fraction(1, 4), empressItems));

            // Remove rules for Empress Wings and Stellar Tune because they're in the Calamity Style set.
            LCR_NotExpert.ChainedRules.RemoveAll((chain) =>
            {
                var rule = chain.RuleToChain;
                return rule is CommonDrop c && (c.itemId == ItemID.RainbowWings || c.itemId == ItemID.SparkleGuitar);
            });
        }
    }
}
