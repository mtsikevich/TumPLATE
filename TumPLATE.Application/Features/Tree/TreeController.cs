using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace TumPLATE.Application.Features.Tree;

[ApiController]
[Route("[controller]")]
public class TreeController
{
    private readonly ActivitySource _source;

    public TreeController(ActivitySource source)
    {
        _source = source;
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