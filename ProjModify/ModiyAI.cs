namespace Calamity2041EoL.ProjModify;

public class ModiyHallowBossRainbowStreakAI : GlobalProjectile
{
    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.type == ProjectileID.HallowBossRainbowStreak && lateInstantiation;

    // Empress of Light variables
    private const float EmpressRainbowStreakSpreadOutCutoff = 140f;

    public override bool PreAI(Projectile projectile)
    {
        if (projectile.hostile)
        {
            bool revMasterMode = (Main.masterMode && CalamityWorld.revenge) || BossRushEvent.BossRushActive;

            bool spreadOut = false;
            bool homeIn = false;
            float spreadOutCutoffTime = EmpressRainbowStreakSpreadOutCutoff;
            float homeInCutoffTime = NPC.ShouldEmpressBeEnraged() ? (revMasterMode ? 55f : 65f) : (revMasterMode ? 70f : 80f);
            float spreadDeceleration = 0.97f;
            float minAcceleration = revMasterMode ? 0.075f : 0.05f;
            float maxAcceleration = revMasterMode ? 0.15f : 0.1f;
            float homingVelocity = revMasterMode ? 36f : 30f;
            float maxVelocity = homingVelocity * 1.5f;
            float accelerationToMaxVelocity = 1.01f;

            if (projectile.timeLeft > spreadOutCutoffTime)
                spreadOut = true;
            else if (projectile.timeLeft > homeInCutoffTime)
                homeIn = true;

            if (spreadOut)
            {
                float spreadVelocity = (float)Math.Cos(projectile.whoAmI % 6f / 6f + projectile.position.X / 320f + projectile.position.Y / 160f);
                projectile.velocity *= spreadDeceleration;
                projectile.velocity = projectile.velocity.RotatedBy(spreadVelocity * MathHelper.TwoPi * 0.125f * 1f / 30f);
            }

            if (homeIn)
            {
                int playerIndex = (int)projectile.ai[0];
                Vector2 velocity = projectile.velocity;
                if (Main.player.IndexInRange(playerIndex))
                {
                    Player player = Main.player[playerIndex];
                    velocity = projectile.DirectionTo(player.Center) * homingVelocity;
                }

                float amount = MathHelper.Lerp(minAcceleration, maxAcceleration, Utils.GetLerpValue(spreadOutCutoffTime, 30f, projectile.timeLeft, clamped: true));
                projectile.velocity = Vector2.SmoothStep(projectile.velocity, velocity, amount);
            }
            else
            {
                if (projectile.velocity.Length() < maxVelocity)
                {
                    projectile.velocity *= accelerationToMaxVelocity;
                    if (projectile.velocity.Length() > maxVelocity)
                    {
                        projectile.velocity.Normalize();
                        projectile.velocity *= maxVelocity;
                    }
                }
            }

            projectile.Opacity = spreadOut ? 0.4f : Utils.GetLerpValue(240f, 220f, projectile.timeLeft, clamped: true);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

            return false;
        }

        return true;
    }

    public override bool? CanDamage(Projectile projectile)
    {
        if (projectile.hostile)
            return projectile.timeLeft <= EmpressRainbowStreakSpreadOutCutoff;

        return null;
    }
}
public class ModiyHallowBossLastingRainbowAI : GlobalProjectile
{
    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.type == ProjectileID.HallowBossLastingRainbow && lateInstantiation;

    public override void AI(Projectile projectile)
    {
        if (CalamityWorld.revenge || BossRushEvent.BossRushActive)
        {
            int spreadOutTime = 90;
            if (projectile.timeLeft > EmpressLastingRainbowTotalDuration - spreadOutTime)
                projectile.velocity *= ((Main.masterMode || BossRushEvent.BossRushActive) ? 1.017078f : 1.015525f);
        }
    }

    private const int EmpressLastingRainbowTotalDuration = 660;
    private const int EmpressLastingRainbowTimeBeforeDealingDamage = 60;
    public override bool? CanDamage(Projectile projectile)
    {
        if (projectile.timeLeft > EmpressLastingRainbowTotalDuration - EmpressLastingRainbowTimeBeforeDealingDamage)
            return false;

        return null;
    }
}
