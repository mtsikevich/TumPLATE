using Microsoft.FeatureManagement.Mvc;
using TumPLATE.Domain.Tree;
using TumPLATE.Domain.Tree.Exception;

namespace TumPLATE.Infrastructure.Persistence.TreePersistence;

public class TreeRepository: ITreeRepository
{
    private readonly SampleDbContext _sampleDbContext;

    public TreeRepository(SampleDbContext sampleDbContext)
    {
        _sampleDbContext = sampleDbContext;
    }

    public async Task<TreeState> CreateTreeAsync(string name)
    {
        var newTree = new TreeState
        {
            Name = name,
            Fruits = new List<Fruit>(),
            LastFruitAddingDate = DateTime.Now
        };
        
        await _sampleDbContext.Trees.AddAsync(newTree);

        return newTree;
    }
    
    public async Task<TreeAggregate> GetAsync(int id)
    {
        var tree = await _sampleDbContext.Trees!.FindAsync(id);

        if (tree is null)
            throw new TreeNotFoundException();
        
        return new TreeAggregate(tree);
    }
    
    public async Task SaveAsync()
    {
        await _sampleDbContext.SaveChangesAsync();
    }
}