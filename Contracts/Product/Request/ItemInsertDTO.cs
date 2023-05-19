using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class ItemInsertDTO
{
    [Required] public string? Code { get; set; }
    public string? Description { get; set; }
    [Required] public int CategoryId { get; set; }
    public int MaterialId { get; set; }
    public bool IsActive { get; set; } = true;
}