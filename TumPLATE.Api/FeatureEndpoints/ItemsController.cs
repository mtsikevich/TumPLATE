using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TumPLATE.Api.FeatureEndpoints;

[ApiController]
[Route("[controller]")]
public class ItemsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(new[] {"item1", "item2"});
    }
}