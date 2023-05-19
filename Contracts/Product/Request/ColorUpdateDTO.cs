using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class ColorUpdateDTO
{
    [Required] public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? HexCode { get; set; }
    public bool IsActive { get; set; }
}