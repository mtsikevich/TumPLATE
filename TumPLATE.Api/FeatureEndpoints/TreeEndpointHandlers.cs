using MediatR;
using TumPLATE.Application.Features.Tree.AddFruit;
using TumPLATE.Application.Features.Tree.GetAllFruits;

namespace TumPLATE.Api.FeatureEndpoints
{
    public class TreeEndpointHandlers
    {
        private readonly IMediator _mediator;
        public TreeEndpointHandlers(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IResult> AddFruitAsync(int treeId)
        {
            var result = await _mediator.Send(new AddFruitCommand());
            return TypedResults.Created("", result);
        }

        public async Task<IResult> GetAllFruitAsync(int treeId)
        {
            var result = await _mediator.Send(new GetAllFruitsQuery{TreeId = treeId});
            return TypedResults.Ok(result);
        }
    }
}
