using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Application.Features.Tree.Commands.CreateTree;

public class CreateTreeCommandHandler : IRequestHandler<CreateTreeCommand, Domain.Tree.TreeState>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateTreeCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<Domain.Tree.TreeState> Handle(CreateTreeCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ITreeRepository>();

        var tree = await repository.CreateTreeAsync(request.Name);
        await repository.SaveAsync();

        return tree;
    }
}