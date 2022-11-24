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
    
    public async Task<TreeDomainFunctionality> GetAsync(int id)
    {
        /*var tree = await _sampleDbContext.Trees!.FindAsync(id);

        if (tree is null)
            throw new TreeNotFoundException();*/

        var tree = new Tree
        {
            Fruits = new List<Fruit>()
        };
        return new TreeDomainFunctionality(tree);
    }
    
    public async Task SaveAsync()
    {
        await _sampleDbContext.SaveChangesAsync();
    }
}