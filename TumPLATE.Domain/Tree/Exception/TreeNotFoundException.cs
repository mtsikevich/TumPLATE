namespace TumPLATE.Domain.Tree.Exception;

public class TreeNotFoundException: System.Exception
{
    public TreeNotFoundException() { }

    public TreeNotFoundException(int id)
        :base($"TreeState with id: {id} not found")
    {
    }
}