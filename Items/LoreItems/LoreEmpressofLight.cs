using CalRD.Items.LoreItems;
using Microsoft.Xna.Framework.Input;
using Terraria.Localization;

namespace Calamity2041EoL.Items.LoreItems;

public class LoreEmpressofLight : LoreItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemNoGravity[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.rare = ItemRarityID.Yellow;
        Item.consumable = false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "LoreShort",
            Language.GetTextValue("Mods.Calamity2041EoL.Items.LoreEmpressofLight.Tooltip")));

        bool shiftHeld = Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift);
        if (!shiftHeld)
            return;

        tooltips.Add(new TooltipLine(Mod, "LoreFull",
            Language.GetTextValue("Mods.Calamity2041EoL.Items.LoreEmpressofLight.Lore")));
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.FairyQueenTrophy)
            .AddTile(TileID.Bookcases)
            .Register();
    }
}
