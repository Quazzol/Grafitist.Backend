namespace Grafitist.Misc.Interfaces;

public interface IPriceManager
{
    public Task SetDiscountedPrice(ITransactionLine transactionLine);
    public Task SetDiscountedPrice(IEnumerable<ITransactionLine>? transactionLines);
    public Task SetDiscountedPrice(IProduct product);
    public Task SetDiscountedPrice(IEnumerable<IProduct>? products);
    public Task<double> CalculateTotalDiscountedPrice(double totalAmount);
}