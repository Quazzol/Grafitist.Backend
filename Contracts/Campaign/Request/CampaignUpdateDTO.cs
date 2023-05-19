using System.ComponentModel.DataAnnotations;
using Grafitist.Misc.Enums;

namespace Grafitist.Contracts.Campaign.Request;

public class CampaignUpdateDTO
{
    [Required] public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public CampaignType Type { get; set; }
    public int? CampaignTypeId { get; set; }
    public double MinimumPrice { get; set; }
    public double Percent { get; set; }
}