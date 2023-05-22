namespace Grafitist.Misc.Interfaces;

public interface ITransaction
{
    public IEnumerable<ITransactionLine>? Lines { get; }
}