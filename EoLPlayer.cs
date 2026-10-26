namespace Calamity2041EoL;

public class EoLPlayer : ModPlayer
{
    public bool reducedNightwitherDamage;
    public bool reducedHolyFlamesDamage;

    public override void ResetEffects()
    {
        reducedNightwitherDamage = false;
        reducedHolyFlamesDamage = false;
    }

    // 2.0.4.1：翱翔徽章不再无限飞，改为 +25% 飞行时间。
    public override void PostUpdateEquips()
    {
        if (Player.empressBrooch)
        {
            Player.wingTimeMax = (int)(Player.wingTimeMax * 1.25f);
            Player.moveSpeed += 0.1f;
            Player.jumpSpeedBoost += 0.5f;
        }
    }

    // 月光石/太阳石/天界石/天界壳：对应 DoT 伤害减半。
    public override void UpdateBadLifeRegen()
    {
        CalRD.CalPlayer.CalamityPlayer calamityPlayer = Player.Calamity();
        if (reducedNightwitherDamage && calamityPlayer.nightwither)
            Player.lifeRegen += 8;
        if (reducedHolyFlamesDamage && calamityPlayer.hFlames)
            Player.lifeRegen += 8;
    }

    // 2.0.4.1：持久彩虹拖尾越靠后伤害越低（40% → 100%）。
    public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
        if (proj.type != ProjectileID.HallowBossLastingRainbow)
            return;

        Rectangle hitbox = proj.Hitbox;
        const int trailLength = 80;
        const int startOfDamageFalloff = 20;

        for (int k = 0; k < trailLength; k += 2)
        {
            Vector2 oldPosition = proj.oldPos[k];
            if (oldPosition == Vector2.Zero)
                continue;

            hitbox.X = (int)oldPosition.X;
            hitbox.Y = (int)oldPosition.Y;

            if (!hitbox.Intersects(Player.Hitbox))
                continue;

            if (k > startOfDamageFalloff)
            {
                float falloff = 1f - (k - startOfDamageFalloff) / (float)(trailLength - startOfDamageFalloff);
                modifiers.SourceDamage *= MathHelper.Lerp(0.4f, 1f, falloff);
            }

            break;
        }
    }
}
