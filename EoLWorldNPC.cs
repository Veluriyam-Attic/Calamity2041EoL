using CalRD.NPCs.TownNPCs;
using Terraria.ModLoader.Utilities;

namespace Calamity2041EoL;

public class EoLWorldNPC : GlobalNPC
{
    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        // 七彩蝴蝶：黄昏到午夜、地表/天空、困难模式、圣洁环境，10% 圣洁刷新率。
        if (Main.dayTime || Main.time >= 16200D || !Main.hardMode)
            return;

        if (!spawnInfo.Player.ZoneOverworldHeight && !spawnInfo.Player.ZoneSkyHeight)
            return;

        if (!NPC.AnyNPCs(NPCID.EmpressButterfly))
            pool[NPCID.EmpressButterfly] = SpawnCondition.OverworldHallow.Chance * 0.1f;
    }

    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType != ModContent.NPCType<FAP>())
            return;

        shop.Add(new Item(ItemID.EmpressButterfly)
        {
            shopCustomPrice = Item.buyPrice(0, 10)
        }, Condition.DownedPlantera);
    }
}
