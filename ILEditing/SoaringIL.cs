using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Calamity2041EoL.ILEditing;

internal static class SoaringIL
{
    public static void Load()
    {
        IL_Player.Update += NerfSoaringRunAccelerationAndRocketBoots;
        IL_Player.WingMovement += RemoveSoaringInfiniteFlight;

        // 2.0.4.1：Boss Rush 中光女强制按白天狂暴处理。
        On_NPC.ShouldEmpressBeEnraged += orig => BossRushEvent.BossRushActive || orig();
    }

    private static void RemoveSoaringInfiniteFlight(ILContext il)
    {
        ILCursor cursor = new(il);
        if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("empressBrooch")))
            return;

        cursor.Emit(OpCodes.Ldc_I4_0);
        cursor.Emit(OpCodes.And);
    }

    private static void NerfSoaringRunAccelerationAndRocketBoots(ILContext il)
    {
        ILCursor cursor = new(il);

        if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("empressBrooch")))
            return;

        if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.75f)))
            return;

        cursor.Next.Operand = 1.1f;

        // 火箭靴的无限飞同样依赖 empressBrooch，一并封掉。
        if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("empressBrooch")))
            return;

        cursor.Emit(OpCodes.Ldc_I4_0);
        cursor.Emit(OpCodes.And);
    }
}
