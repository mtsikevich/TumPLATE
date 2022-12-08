using System.Net;
using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;

namespace TumPLATE.Tests.Acceptance.Hooks;

[Binding]
public class DockerComposeHooks
{
    private static ICompositeService? _compositeService;
    private readonly IObjectContainer _objectContainer;

    public DockerComposeHooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }
    
    [BeforeTestRun]
    public static void DockerComposeUp()
    {
        const string dockerComposeFileName = "docker-compose.yml";
        var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);
        const string apiBaseUrl = "http://localhost:7357";

        _compositeService = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(dockerComposePath)
            .RemoveOrphans()
            .WaitForHttp("tumplate.api", $"{apiBaseUrl}/",
                continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
            .Build()
            .Start();

    }
    
    [AfterTestRun]
    public static void DockerComposeDown()
    {
        _compositeService?.Stop();
        _compositeService?.Dispose();
    }

    [BeforeScenario]
    public void AddHttpClient()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:7357")
        };
        
        _objectContainer.RegisterInstanceAs(httpClient);
    }
    
    private static string GetDockerComposeLocation(string dockerComposeFileName)
    {
        var directory = Directory.GetCurrentDirectory();
        while (!Directory.EnumerateFiles(directory, "*.yml").Any(s  => s.EndsWith(dockerComposeFileName)))
        {
            directory = directory[..directory.LastIndexOf(Path.DirectorySeparatorChar)];
        }

        return Path.Combine(directory, dockerComposeFileName);
    }
}