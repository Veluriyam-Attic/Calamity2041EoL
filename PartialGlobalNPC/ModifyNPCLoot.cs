using Calamity2041EoL.Items.LoreItems;
using CalRD.Items.Potions.Alcohol;

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

            var empressItems = new int[]
            {
                 ItemID.PiercingStarlight,
                 ItemID.FairyQueenRangedItem,
                 ItemID.FairyQueenMagicItem,
                 ItemID.SparkleGuitar,
                 ItemID.EmpressBlade,
                 ItemID.RainbowWhip,
                 ItemID.RainbowWings
            };
            LCR_NotExpert.Add(DropHelper.CalamityStyle(new Fraction(1, 4), empressItems));

            LCR_NotExpert.ChainedRules.RemoveAll((chain) =>
            {
                var rule = chain.RuleToChain;
                return rule is CommonDrop c && (c.itemId == ItemID.RainbowWings || c.itemId == ItemID.SparkleGuitar);
            });
        }

        // 普通模式才对每名玩家掉翱翔徽章。
        npcLoot.AddConditionalPerPlayer(() => !Main.expertMode, ItemID.EmpressFlightBooster, 1);

        // 复仇模式掉大师 Trophy / Pet。
        npcLoot.Add(ItemDropRule.ByCondition(DropHelper.If(() => CalamityWorld.revenge), ItemID.FairyQueenMasterTrophy));
        npcLoot.Add(ItemDropRule.ByCondition(DropHelper.If(() => CalamityWorld.revenge), ItemID.FairyQueenPetItem, 25));

        // GFB：Fabsol's Vodka + Terraformer。
        npcLoot.Add(ItemDropRule.ByCondition(DropHelper.If(() => Main.zenithWorld), ModContent.ItemType<FabsolsVodka>(), 1, 1, 9999));
        npcLoot.Add(ItemDropRule.ByCondition(DropHelper.If(() => Main.zenithWorld), ItemID.Clentaminator2));

        // 首次击杀 Lore。
        npcLoot.AddConditionalPerPlayer(() => !NPC.downedEmpressOfLight, ModContent.ItemType<LoreEmpressofLight>(), 1);
    }
}
