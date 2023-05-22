namespace Grafitist.Misc.Interfaces;

public interface IPriceManager
{
    public Task CalculateDiscountedPrice(ITransactionLine transactionLine);
    public Task CalculateDiscountedPrices(IEnumerable<ITransactionLine>? transactionLines);
    public Task<double> CalculateTotalDiscountedPrice(double totalAmount);
}