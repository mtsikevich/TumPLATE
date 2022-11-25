namespace TumPLATE.Api.FeatureEndpoints
{
    public static class EndpointHandlerExtensions
    {
        public static void AddApiHandlers(this WebApplication app)
        {
            var treeEndpointsHandlers = app.Services.GetRequiredService<TreeEndpointHandlers>();

            app.MapPost("/strt", treeEndpointsHandlers.AddFruitAsync);
            app.MapGet("/fruits/{treeId:int}", treeEndpointsHandlers.GetAllFruitAsync);
        }
    }
}
