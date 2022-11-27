using Azure.Identity;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using TumPLATE.Application;
using TumPLATE.Application.Features.BackgroundJobs;
using TumPLATE.Infrastructure.Extras.FeatureFlags;
using TumPLATE.Infrastructure.KafkaIntegration;
using TumPLATE.Infrastructure.Observability;
using TumPLATE.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var appConfigurationConnectionString = builder.Configuration.GetConnectionString("AzureAppConfiguration");
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(appConfigurationConnectionString)
        .Select("apiname:*")
        .ConfigureRefresh(refreshOptions => refreshOptions.Register("apiname:sentinel", refreshAll: true))
        .UseFeatureFlags(flagOptions =>
        {
            flagOptions.Select("apiname:*", LabelFilter.Null);
            flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(30);
        });
});

builder.Services.AddAzureAppConfiguration()
    .AddFeatureManagement()
    .AddFeatureFilter<PercentageFilter>()
    .AddFeatureFilter<HostingEnvironmentFilter>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddObservability("TumPLATE", "1.0");
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("TumPLATE.Application"));
builder.Services.AddApplicationHangfireBackgroundServices(useMemoryStorage:true);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddKafkaIntegration();
//builder.Services.AddHttpClient<IKafkaIntegration,KafkaHttpApiIntegration>();

var app = builder.Build();

app.UseAzureAppConfiguration();
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

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHangfireDashboard("/hangfire");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.StartPollingTheKafkaTopic("claim_sample_topic");

app.Run();
