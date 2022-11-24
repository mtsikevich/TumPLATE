using MediatR;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Application.Features.Tree.GetAllFruits;

public class GetAllFruitsQuery: IRequest<IReadOnlyList<Fruit>>
{
    public int TreeId { get; set; }
}