namespace Calamity2041EoL;

public class EoLPlayer : ModPlayer
{
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
