namespace Grafitist.Misc;

public struct DoubleRange
{
    public DoubleRange() : this(0, double.MaxValue)
    {
    }

    public DoubleRange(double start, double end)
    {
        Start = start;
        End = end;
    }

    public double Start { get; set; }
    public double End { get; set; }
}