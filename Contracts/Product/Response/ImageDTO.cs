namespace Grafitist.Contracts.Product.Response;

public class ImageDTO
{
    public int Id { get; set; }
    public bool IsCover { get; set; }
    public bool IsActive { get; set; }
    public string? Type { get; set; }
    public byte[]? Data { get; set; }
    public int ProductModelId { get; set; }
}