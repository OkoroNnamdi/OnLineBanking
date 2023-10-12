using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnLineBanking.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Infrastructure.Extensions
{
    public static  class MailServiceExtensions
    {
        public static void ConfigureMailService(this IServiceCollection services,  IConfiguration Configuration)
        {
            //EmailService registration
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailMessage>();
            services.AddSingleton(emailConfig);
        }
    }
}
