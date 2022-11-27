using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace TumPLATE.Application
{
    public static class ApplicationMediatorExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
        }
    }
}
