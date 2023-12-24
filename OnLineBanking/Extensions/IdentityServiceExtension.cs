using Microsoft.AspNetCore.Identity;
using OnLineBanking.Core;
using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Infrastructure.Extensions
{
    public static class IdentityServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<OnlineBankDBContext>()
                .AddDefaultTokenProviders();
        }
    }
}
