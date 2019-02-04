using Microsoft.Extensions.DependencyInjection;

namespace QIQO.Monitor.Api.Services
{
    public static class EntityServiceExtensions
    {
        public static IServiceCollection AddEntityServices(this IServiceCollection services)
        {
            return services.AddTransient<IBlockingEntityService, BlockingEntityService>()
                .AddTransient<IOpenTranactionEntityService, OpenTranactionEntityService>();
        }
    }
}
