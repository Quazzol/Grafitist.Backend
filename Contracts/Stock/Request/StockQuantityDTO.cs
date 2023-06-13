using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Stock.Request;

public class StockQuantityDTO
{
    [Required] public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
}