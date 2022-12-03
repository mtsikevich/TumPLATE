using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

using Microsoft.FeatureManagement.Mvc;
using TumPLATE.Application.Features.Tree.Commands.CreateTree;

namespace TumPLATE.Application.Features.Tree;

[ApiController]
[Route("[controller]")]
public class TreeController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ActivitySource _source;

    public TreeController(IMediator mediator, ActivitySource source)
    {
        _mediator = mediator;
        _source = source;
    }

    [HttpPost("create/tree")]
    public async Task<IActionResult> CreateTreeAsync(CreateTreeCommand createTreeCommand)
    {
        var result = await _mediator.Send(createTreeCommand);
        return Ok(result);
    }
    
    [HttpGet]
    public string GetString()
    {
        using(var activity = _source.StartActivity())
        {
            activity!.DisplayName = "Wiiiii";
            Task.Delay(TimeSpan.FromSeconds(1)).GetAwaiter().GetResult();
            
        }

        using (var activity2 = _source.StartActivity("getting from HTTP server"))
        {
            activity2!.DisplayName = "getting from HTTP server";
            Task.Delay(TimeSpan.FromSeconds(2)).GetAwaiter().GetResult();
        }
        
        return "stringgggg";
    }
}