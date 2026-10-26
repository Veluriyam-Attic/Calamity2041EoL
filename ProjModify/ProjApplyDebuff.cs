namespace Calamity2041EoL.ProjModify;

public class ProjApplyDebuff : GlobalProjectile
{
    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) =>
        (entity.type == ProjectileID.FairyQueenLance ||
         entity.type == ProjectileID.HallowBossRainbowStreak ||
         entity.type == ProjectileID.HallowBossSplitShotCore ||
         entity.type == ProjectileID.HallowBossLastingRainbow ||
         entity.type == ProjectileID.FairyQueenSunDance) && lateInstantiation;

    public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
        int duration = projectile.type switch
        {
            ProjectileID.HallowBossLastingRainbow => 120,
            ProjectileID.FairyQueenSunDance => 180,
            _ => 60
        };

        int buffType = BossRushEvent.BossRushActive || NPC.ShouldEmpressBeEnraged()
            ? ModContent.BuffType<HolyFlames>()
            : ModContent.BuffType<Nightwither>();

        target.AddBuff(buffType, duration);
    }
}
