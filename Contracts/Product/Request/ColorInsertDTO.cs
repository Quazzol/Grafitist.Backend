using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class ColorInsertDTO
{
    [Required] public string? Code { get; set; }
    public string? Description { get; set; }
    [Required] public string? HexCode { get; set; }
    public bool IsActive { get; set; } = true;
}