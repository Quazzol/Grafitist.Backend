namespace Grafitist.Misc;

public class ProductFilter
{
    public int? CategoryId { get; set; }
    public int? MaterialId { get; set; }
    public int? ColorId { get; set; }
    public int? VariantId { get; set; }
    public DoubleRange? Price { get; set; }
}