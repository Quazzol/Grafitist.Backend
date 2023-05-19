using System.ComponentModel.DataAnnotations;

namespace Grafitist.Models.Stock;

public class StockModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public int ReserveQty { get; set; } = 0;
    public int OrderQty { get; set; } = 0;
}