namespace Grafitist.Contracts.Stock.Response;

public class StockDTO
{
    public Guid Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public int OrderQty { get; set; } = 0;
    public int AvailableQty => Quantity - OrderQty;
}