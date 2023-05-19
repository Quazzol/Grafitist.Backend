namespace Grafitist.Contracts.Product.Response;

public class ImageDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsCover { get; set; }
    public bool IsActive { get; set; }
    public int ProductId { get; set; }
}