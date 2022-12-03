namespace TumPLATE.Domain.Common;

public abstract class Aggregate<T>
{
    protected T State { get; set; }
    
    protected Aggregate(T state)
    {
        State = state;
    }
}