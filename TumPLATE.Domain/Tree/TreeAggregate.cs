using TumPLATE.Domain.Common;

namespace TumPLATE.Domain.Tree;

public class TreeAggregate: Aggregate<TreeState>
{
    public TreeAggregate(TreeState treeState)
        :base(treeState)
    {
        
    }

    public void AddFruit(Fruit newFruit)
    {
        
        State.Fruits.Add(newFruit);
    }

    public IReadOnlyList<Fruit> GetFruits()
    {
        return State.Fruits.ToList();
    }
    
    public Fruit TakeFruit()
    {
        return State.Fruits.First();
    }
}