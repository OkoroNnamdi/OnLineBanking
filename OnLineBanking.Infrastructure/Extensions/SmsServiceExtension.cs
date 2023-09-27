using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Infrastructure.Extensions
{
    public static  class SmsServiceExtension
    {
        public static void ConfigureSmsService(this IServiceCollection services, IConfiguration Configuration)
        {
            //EmailService registration
            var smsConfig = Configuration
               .GetSection("SMSConfiguration")
               .Get<SMSConfiguration>();
            services.AddSingleton(smsConfig);

        }
    }
}
