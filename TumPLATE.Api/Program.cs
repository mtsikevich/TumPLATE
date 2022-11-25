using Hangfire;
using MediatR;
using TumPLATE.Api.FeatureEndpoints;
using TumPLATE.Application;
using TumPLATE.Application.Features.BackgroundJobs;
using TumPLATE.Infrastructure.KafkaIntegration;
using TumPLATE.Infrastructure.Observability;
using TumPLATE.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddObservability("TumPLATE", "1.0");
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("TumPLATE.Application"));
builder.Services.AddApplicationHangfireBackgroundServices(useMemoryStorage:true);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddSingleton<TreeEndpointHandlers>();

builder.Services.AddHttpClient<IKafkaIntegration,KafkaHttpApiIntegration>();

var app = builder.Build();

app.AddApiHandlers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.StartPollingTheKafkaTopic("claim_sample_topic");

app.Run();
