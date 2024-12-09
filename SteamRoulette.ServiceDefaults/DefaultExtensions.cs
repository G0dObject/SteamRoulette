using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class DefaultExtensions
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            _ = services.AddEndpointsApiExplorer();
            _ = services.AddSwaggerGen();
            _ = services.AddControllers();

            return services;
        }
    }
}