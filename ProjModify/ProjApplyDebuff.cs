namespace Calamity2041EoL.ProjModify;

public class ProjApplyDebuff : GlobalProjectile
{
    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) =>
        (entity.type == ProjectileID.FairyQueenLance || entity.type == ProjectileID.HallowBossRainbowStreak || entity.type == ProjectileID.HallowBossSplitShotCore) && lateInstantiation;

    public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
        target.AddBuff(NPC.ShouldEmpressBeEnraged() ? ModContent.BuffType<HolyFlames>() : ModContent.BuffType<Nightwither>(), 60);
    }
}
