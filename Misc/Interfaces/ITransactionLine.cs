namespace Grafitist.Misc.Interfaces;

public interface ITransactionLine
{
    public double Amount { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public Guid CampaignId { get; set; }
}