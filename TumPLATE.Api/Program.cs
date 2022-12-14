using Hangfire;
using MediatR;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using TumPLATE.Application;
using TumPLATE.Application.Features.BackgroundJobs;
using TumPLATE.Infrastructure.FeatureFlagFilters;
using TumPLATE.Infrastructure.KafkaIntegration;
using TumPLATE.Infrastructure.Observability;
using TumPLATE.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    var sampleDb = builder.Configuration.GetConnectionString("sampleDb");
    var appConfigurationConnectionString = builder.Configuration.GetConnectionString("AzureAppConfiguration");
    var configurationPrefix = "apiname"; 
    options.Connect(appConfigurationConnectionString)
        .Select($"{configurationPrefix}:*")
        .ConfigureRefresh(refreshOptions => refreshOptions.Register($"{configurationPrefix}:sentinel", refreshAll: true))
        .UseFeatureFlags(flagOptions =>
        {
            flagOptions.Select($"{configurationPrefix}:*");
            flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(30);
        });
});
builder.Services.AddAzureAppConfiguration()
    .AddFeatureManagement()
    .AddFeatureFilter<PercentageFilter>()
    .AddFeatureFilter<HostingEnvironmentFilter>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddObservability("TumPLATE", "1.0");
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("TumPLATE.Application"));
builder.Services.AddApplicationHangfireBackgroundServices(useMemoryStorage:true);

//var dbConnectionString = builder.Configuration.GetConnectionString("DbConnectionString");
//builder.Services.AddSqlPersistence(dbConnectionString);
builder.Services.AddKafkaIntegration();

var app = builder.Build();

app.UseAzureAppConfiguration();

//app.ApplyDataMigrations<SampleDbContext>();

app.MapGet("/", async (IFeatureManager featureManager, IConfiguration configuration) =>
{
    var fruits = new List<string>();
    
    if (await featureManager.IsEnabledAsync("newfruit"))
        fruits.Add("lettuce");
    
    return new
    {
        Environment = app.Environment.EnvironmentName,
        Fruits = fruits,
        section = configuration.GetValue<string>("apiname:sentinel")
    };
});


app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.StartPollingTheKafkaTopic("claim_sample_topic", "job 1");
app.StartPollingTheKafkaTopic("another_topic", "job 2");
app.Run();
