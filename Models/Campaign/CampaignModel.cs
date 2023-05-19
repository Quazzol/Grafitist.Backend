using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Grafitist.Misc.Enums;

namespace Grafitist.Models.Campaign;

[Table("Campaign")]
public class CampaignModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string? Code { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public CampaignType Type { get; set; }
    public int? CampaignTypeId { get; set; }
    public double MinimumPrice { get; set; }
    public double Percent { get; set; }
}