namespace Grafitist.Misc.Interfaces;

public interface ITransactionLine : IProduct
{
    public int Quantity { get; set; }
    public double Amount { get; }
    public Guid CampaignId { get; set; }
}