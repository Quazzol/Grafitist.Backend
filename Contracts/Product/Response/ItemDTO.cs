namespace Grafitist.Contracts.Product.Response;

public class ItemDTO
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public CategoryDTO? Category { get; set; }
    public int MaterialId { get; set; }
    public MaterialDTO? Material { get; set; }
}