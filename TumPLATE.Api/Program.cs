using Microsoft.AspNetCore.OpenApi;
using TumPLATE.Infrastructure.KafkaIntegration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKafkaIntegration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!")
    .WithOpenApi();

app.Run();