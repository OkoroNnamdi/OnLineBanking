

using Microsoft.Extensions.DependencyInjection;
using OnLineBanking.Infrastructure.Extensions.Automapper;

namespace OnLineBanking.Infrastructure.Extensions
{
    public static  class AutoMapperServiceExtension
    {
        public static void ConfigureAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapInitializer));
        }
    }
}
