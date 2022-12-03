using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TumPLATE.Application.Features.Tree.Queries.GetAllFruits;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Application.Features.Tree.Commands.AddFruit;

public class AddFruitCommandHandler : IRequestHandler<AddFruitCommand, Fruit>
{
    private readonly IServiceProvider _serviceProvider;

    public AddFruitCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<Fruit> Handle(AddFruitCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ITreeRepository>();
        
        var tree = await repository.GetAsync(request.TreeId);
        var newFruit = new Fruit
        {
            Condition = FruitCondition.Good
        };
        tree.AddFruit(newFruit);

        await repository.SaveAsync();
        return newFruit;
    }
}
