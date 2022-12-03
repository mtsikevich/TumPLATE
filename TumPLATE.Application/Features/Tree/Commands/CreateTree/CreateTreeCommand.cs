using MediatR;

namespace TumPLATE.Application.Features.Tree.Commands.CreateTree;

public class CreateTreeCommand: IRequest<Domain.Tree.TreeState>
{
    public string Name { get; set; }
}