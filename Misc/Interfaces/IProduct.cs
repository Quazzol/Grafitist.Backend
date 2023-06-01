namespace Grafitist.Misc.Interfaces;

public interface IProduct
{
    public int ProductId { get; }
    public double Price { get; set; }
    public double DiscountedPrice { get; set; }
}