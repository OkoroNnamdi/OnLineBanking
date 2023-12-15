using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using OnLineBanking.Infrastructure.Extensions;
using Serilog;

namespace OnLineBanking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            var services = builder.Services;
            var env = builder.Environment;

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddScoped<IUrlHelper>(x =>
                    x.GetRequiredService<IUrlHelperFactory>()
                        .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));

            //For Entity Framework

            //builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer
            //(builder.Configuration.GetConnectionString("ConnStr")));

            builder.Services.AddControllers();
            // Configure Mailing Service
            builder.Services.ConfigureMailService(config);

            builder.Services.AddSingleton(Log.Logger);

            // Adds our Authorization Policies to the Dependecy Injection Container
           // builder.Services.AddPolicyAuthorization();

            // Configure Identity
            builder.Services.ConfigureIdentity();

            builder.Services.AddAuthentication();

            // Add Jwt Authentication and Authorization
            services.ConfigureAuthentication(config);

            //Add cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44351", "http://localhost:4200", "http://localhost:3000")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

            // Configure AutoMapper
            services.ConfigureAutoMappers();

            // Configure Cloudinary
            builder.Services.AddCloudinary(CloudinaryServiceExtension.GetAccount(config));

            //builder.Services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling
            //= Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //builder.Services.AddControllers()
            //    .AddNewtonsoftJson(op => op.SerializerSettings
            //        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //builder.Services.AddMvc().AddFluentValidation(fv =>
            //{
            //    fv.DisableDataAnnotationsValidation = true;
            //    fv.RegisterValidatorsFromAssemblyContaining<Program>();
            //    fv.ImplicitlyValidateChildProperties = true;
            //});

            builder.Services.AddSwagger();

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            // Register Dependency Injection Service Extension
            builder.Services.AddDependencyInjection();



            //builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}