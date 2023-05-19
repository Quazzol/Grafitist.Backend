using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class ImageInsertDTO
{
    public bool IsCover { get; set; }
    public bool IsActive { get; set; } = true;
    public int ProductId { get; set; }
    [Required] public IFormFile? Image { get; set; }
}