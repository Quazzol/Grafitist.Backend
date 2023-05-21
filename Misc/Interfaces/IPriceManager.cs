namespace Grafitist.Misc.Interfaces;

public interface IPriceManager
{
    public Task CalculateDiscountedPrices(IEnumerable<IHasCalculatableAmount>? amounts);
    public Task<double> CalculateTotalDiscountedPrice(double totalAmount);
}