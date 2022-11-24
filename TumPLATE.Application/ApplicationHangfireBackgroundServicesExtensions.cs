using System.Drawing.Drawing2D;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TumPLATE.Application;

public static class ApplicationHangfireBackgroundServicesExtensions
{
    public static void AddApplicationHangfireBackgroundServices(this IServiceCollection services, string? hangfireConnectionString = null, bool useAppAsServer = true, bool useMemoryStorage = false)
    {
        HangfireSetup(services, hangfireConnectionString, useAppAsServer, useMemoryStorage);
    }

    private static void HangfireSetup(IServiceCollection services, string? hangfireConnectionString, bool useAppAsServer,
        bool useMemoryStorage)
    {
        if (useMemoryStorage)
            services.AddHangfire(c => c.UseMemoryStorage());
        else
        {
            if (string.IsNullOrWhiteSpace(hangfireConnectionString))
                throw new Exception("Connection string not provided");

            services.AddHangfire(c => c.UseSqlServerStorage(hangfireConnectionString));
        }

        if (useAppAsServer)
            services.AddHangfireServer();
    }
}