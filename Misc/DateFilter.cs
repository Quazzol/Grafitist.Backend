namespace Grafitist.Misc;

public class DateFilter
{
    public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-90);
    public DateTime EndDate { get; set; } = DateTime.Now;
}