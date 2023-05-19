using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.Product;

[Table("Item")]
public class ItemModel
{
    [Key] public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public CategoryModel? Category { get; set; }
    public int MaterialId { get; set; }
    public MaterialModel? Material { get; set; }
}