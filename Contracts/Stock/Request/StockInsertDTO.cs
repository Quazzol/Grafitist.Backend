using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Stock.Request;

public class StockInsertDTO
{
    [Required] public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public int ReserveQty { get; set; } = 0;
}