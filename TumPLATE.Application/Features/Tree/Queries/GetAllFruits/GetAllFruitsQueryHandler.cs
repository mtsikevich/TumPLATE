using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Application.Features.Tree.Queries.GetAllFruits;

public class GetAllFruitsQueryHandler : IRequestHandler<GetAllFruitsQuery, IReadOnlyList<Fruit>>
{
    private readonly IServiceProvider _serviceProvider;

    public GetAllFruitsQueryHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<IReadOnlyList<Fruit>> Handle(GetAllFruitsQuery request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ITreeRepository>();
        
        var tree = await repository.GetAsync(request.TreeId);

        return tree.GetFruits();
    }
}