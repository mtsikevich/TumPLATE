using TumPLATE.Domain.Common;

namespace TumPLATE.Domain.Tree;

public interface ITreeRepository: IRepository<TreeState, TreeAggregate>
{
    public Task<TreeState> CreateTreeAsync(string name);
}
