namespace Grafitist.Misc;

public class ProductFilter
{
    public List<int>? CategoryId { get; set; }
    public List<int>? MaterialId { get; set; }
    public List<int?>? ColorId { get; set; }
    public List<int?>? VariantId { get; set; }
    public DoubleRange? Price { get; set; }
}