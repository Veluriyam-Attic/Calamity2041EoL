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
        if (projectile.type == ProjectileID.PiercingStarlight)
            projectile.Calamity().trueMelee = true;
    }
}
