namespace Grafitist.Contracts.Product.Response;

public class ColorDTO
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? HexCode { get; set; }
    public bool IsActive { get; set; }
}