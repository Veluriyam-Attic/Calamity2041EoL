namespace Calamity2041EoL;

internal static class ExtensionMethods
{
    // 2.0.4.1 的 NPC.SyncExtraAI 在 CalRD 里没有，复用 CalRD 自己的 newAI 同步包。
    public static void SyncExtraAI(this NPC npc)
    {
        if (Main.netMode == NetmodeID.SinglePlayer)
            return;

        CalamityGlobalNPC calamityNPC = npc.Calamity();
        ModPacket packet = ModLoader.GetMod("CalRD").GetPacket();
        packet.Write((byte)CalRDMessageType.SyncCalamityNPCAIArray);
        packet.Write((byte)npc.whoAmI);

        for (int i = 0; i < calamityNPC.newAI.Length; i++)
            packet.Write(calamityNPC.newAI[i]);

        packet.Send();
    }
}
