using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using Scalar.AspNetCore;

using Test.Api.Filters;
using Test.Api.Helpers;

using TestApp.Domain.Filters;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Services;
using TestApp.Mapper.Profiles;
using TestApp.Repository;
using TestApp.Repository.Repositories;

using JsonSerializer = TestApp.Domain.Tools.JsonSerializer;

namespace Test.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(static options =>
            {
                options.Filters.Add<CustomExceptionFilterAttribute>();
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
            }).AddNewtonsoftJson(static jsonOptions =>
            {
                jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                jsonOptions.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                jsonOptions.SerializerSettings.Converters.Add(new NullToDefaultJsonConverter());
            });

            // Add CORS policy
            services.AddCors(static options =>
            {
                options.AddPolicy("AllowAll", static builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            AddDependencyInjections(services);

            services.AddDbContext<EntityFrameworkDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), static opt => opt.EnableRetryOnFailure()));
            services.AddAutoMapper(typeof(Startup), typeof(GlobalProfile));

            services.AddOpenApi();
        }

        /// <summary>
        /// Configure App
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        public static void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                // This middleware is used to report app runtime errors in a development environment.
                app.UseDeveloperExceptionPage();
            }

            app.MapOpenApi();
            app.MapScalarApiReference(); // scalar/v1

            app.UseHttpsRedirection();

            // Difference between UseRouting/UseEndpoints https://thecodeblogger.com/2021/05/27/asp-net-core-web-application-routing-and-endpoint-internals/
            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();
        }

        /// <summary>
        /// Add Dependency Injections
        /// </summary>
        /// <param name="services"></param>
        private static void AddDependencyInjections(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<GenericLogClass>>();
            if (logger is null)
            {
                throw new InternalValidationException("GetService return NULL for ILogger<GenericLogClass>");
            }

            services.AddSingleton(typeof(ILogger), logger);

            services.AddSingleton<ILocalSettings, LocalSettings>();

            // Repo
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISerializer, JsonSerializer>();

            // Domain Service
            services.AddTransient<IObjectService, ObjectService>();
            services.AddTransient<IRandomUserService, RandomUserService>();

            // Repo
        }
    }
}