namespace Calamity2041EoL.ItemModify;

public class TreasureLoot : GlobalItem
{
    public override void ModifyItemLoot(Item item, ItemLoot loot)
    {
        RemoveEmpressRules(loot); // Separately remove EoL's weapons and wings, because they're both included below
        int[] empressItems = new int[]
        {
                        ItemID.PiercingStarlight, // Starlight
                        ItemID.FairyQueenRangedItem, // Eventide
                        ItemID.FairyQueenMagicItem, // Nightglow
                        ItemID.SparkleGuitar, // Stellar Tune
                        ItemID.EmpressBlade, // Terraprisma
                        ItemID.RainbowWhip, // Kaleidoscope
                        ItemID.RainbowWings, // Empress Wings have a pathetically low drop rate.
        };
        loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, empressItems));
        loot.AddRevBagAccessories();
    }

    private static void RemoveEmpressRules(ItemLoot loot)
    {
        List<IItemDropRule> rules = loot.Get(false);
        IItemDropRule toRemove = null;

        // Step 1: Remove Empress Wings loot rule
        foreach (IItemDropRule rule in rules)
            if (rule is CommonDropNotScalingWithLuck c && c.itemId == ItemID.RainbowWings)
                toRemove = c;

        if (toRemove is not null)
            loot.Remove(toRemove);

        // Step 2: Remove Empress of Light's weapons
        foreach (IItemDropRule rule in rules)
            if (rule is OneFromOptionsNotScaledWithLuckDropRule o)
                foreach (int itemID in o.dropIds)
                    if (itemID == ItemID.PiercingStarlight)
                        toRemove = o;

        if (toRemove is not null)
            loot.Remove(toRemove);

        // Step 3: Remove Stellar Tune
        foreach (IItemDropRule rule in rules)
            if (rule is CommonDropNotScalingWithLuck c && c.itemId == ItemID.SparkleGuitar)
                toRemove = c;

        if (toRemove is not null)
            loot.Remove(toRemove);
    }
}
