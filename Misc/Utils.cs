using Grafitist.Contracts.Product.Response;

namespace Grafitist.Misc;

public static class Utils
{
    public static string Linkify(string? productDescription, ColorDTO? color, VariantDTO? variant)
    {
        var link = string.Format("{0}-{1}-{2}", productDescription, color?.Description, variant?.Description);
        return link.Linkify();
    }
}