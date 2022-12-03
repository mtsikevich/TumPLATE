using MediatR;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Application.Features.Tree.Commands.AddFruit
{
    public class AddFruitCommand: IRequest<Fruit>
    {
        public int TreeId { get; set; }
    }
}
