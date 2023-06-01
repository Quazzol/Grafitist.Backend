using Grafitist.Contracts.Product.Response;

namespace Grafitist.Misc;

public static class Utils
{
    public static string Linkify(ItemDTO? item, ColorDTO? color, VariantDTO? variant)
    {
        var link = string.Format("{0}-{1}-{2}", item?.Description, color?.Description, variant?.Description);
        return Uri.EscapeDataString(link.Replace(' ', '-'));
    }
}