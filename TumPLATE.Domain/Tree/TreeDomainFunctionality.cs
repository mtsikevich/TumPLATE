namespace TumPLATE.Domain.Tree;

public class TreeDomainFunctionality
{
    private readonly Tree _tree;
    public TreeDomainFunctionality(Tree tree)
    {
        _tree = tree;
    }

    public void AddFruit()
    {
        //check business rules, and then next step
        _tree.Fruits.Add(new Fruit {State = FruitState.Good});
    }

    public IReadOnlyList<Fruit> GetFruits()
    {
        return _tree.Fruits.ToList();
    }
    
    public Fruit TakeFruit()
    {
        return _tree.Fruits.First();
    }
}