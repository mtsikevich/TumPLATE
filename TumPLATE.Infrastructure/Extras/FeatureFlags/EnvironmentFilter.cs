using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

namespace TumPLATE.Infrastructure.Extras.FeatureFlags;

[FilterAlias("HostingEnvironment")]
public class HostingEnvironmentFilter: IFeatureFilter
{
    private readonly IHostEnvironment _environment;

    public HostingEnvironmentFilter(IHostEnvironment environment)
    {
        _environment = environment;
    }
    
    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var settings = context.Parameters.Get<EnvironmentFilterSettings>();
        return Task.FromResult(settings!.ActivateOnEnvironments.Contains(_environment.EnvironmentName));
    }
}

public class EnvironmentFilterSettings
{
    public string[] ActivateOnEnvironments { get; set; } 
}