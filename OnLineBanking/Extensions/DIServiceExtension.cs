using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Infrastructure.Extensions
{
    public static   class DIServiceExtension
    {
       public static void AddDependencyInjection(this IServiceCollection services)
        {
           //services.Configure<CloudinarySetting>(config.GetSection(""));
            // service Injection 
            services.AddScoped<ITokenDetails,TokenDetails>();
            services.AddScoped <ITokenServices,TokenServices>();
        }
    }
}
