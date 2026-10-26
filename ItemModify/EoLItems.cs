using Terraria.Localization;

namespace Calamity2041EoL.ItemModify;

public class EoLItems : GlobalItem
{
    public override bool InstancePerEntity => true;

    public override void SetDefaults(Item item)
    {
        switch (item.type)
        {
            case ItemID.EmpressBlade:
                item.autoReuse = true;
                item.damage = 60;
                item.useTime = 20;
                item.useAnimation = 20;
                break;

            case ItemID.FairyQueenMagicItem:
                item.damage = 54;
                break;

            case ItemID.RainbowWhip:
                item.autoReuse = true;
                break;

            case ItemID.EmpressFlightBooster:
                item.rare = ItemRarityID.Yellow;
                item.expert = false;
                break;
        }
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        EoLPlayer mp = player.GetModPlayer<EoLPlayer>();

        if (item.type == ItemID.MoonStone)
            mp.reducedNightwitherDamage = true;

        if (item.type == ItemID.SunStone)
            mp.reducedHolyFlamesDamage = true;

        if (item.type == ItemID.CelestialStone || item.type == ItemID.CelestialShell)
        {
            mp.reducedNightwitherDamage = true;
            mp.reducedHolyFlamesDamage = true;
        }
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (item.type != ItemID.EmpressFlightBooster)
            return;

        tooltips.Add(new TooltipLine(Mod, "EoLSoaringFlight",
            Language.GetTextValue("Mods.Calamity2041EoL.ItemTooltip.SoaringFlight")));
        tooltips.Add(new TooltipLine(Mod, "EoLSoaringMovement",
            Language.GetTextValue("Mods.Calamity2041EoL.ItemTooltip.SoaringMovement")));
    }
}

public class EoLProjectiles : GlobalProjectile
{
    public override void SetDefaults(Projectile projectile)
    {
        switch (projectile.type)
        {
            case ProjectileID.PiercingStarlight:
                projectile.Calamity().trueMelee = true;
                break;

            case ProjectileID.EmpressBlade:
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 30;
                break;

            case ProjectileID.FairyQueenRangedItemShot:
                projectile.penetrate = 7;
                projectile.extraUpdates = 1;
                break;
        }
    }
}
