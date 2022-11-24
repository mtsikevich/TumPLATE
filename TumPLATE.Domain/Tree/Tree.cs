namespace TumPLATE.Domain.Tree;

public class Tree
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Fruit> Fruits { get; set; }
    public DateTime LastFruitAddingDate { get; set; }
}