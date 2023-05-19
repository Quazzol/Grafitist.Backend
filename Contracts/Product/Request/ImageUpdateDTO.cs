using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class ImageUpdateDTO
{
    [Required] public int Id { get; set; }
    public bool IsCover { get; set; }
    public bool IsActive { get; set; }
}