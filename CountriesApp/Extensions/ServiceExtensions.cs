using Contracts_;
using LoggerService_;
using Microsoft.Extensions.Options;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using Microsoft.OpenApi.Models;

namespace CountriesApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => 
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => { });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpClient("CountriesClient", config =>
            {
                config.Timeout = new TimeSpan(0, 0, 30);
                config.DefaultRequestHeaders.Clear();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Countries Api", Version = "v1" });
            });
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICollectionService, CollectionService>();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => 
            services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            services.AddRateLimiter(opt =>
            {
                opt.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                    RateLimitPartition.GetFixedWindowLimiter("GlobalLimiter",
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 2,
                        QueueLimit = 2,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromMinutes(1)
                    }));

                opt.AddPolicy("CountriesPolicy", context =>
                    RateLimitPartition.GetFixedWindowLimiter("SpecificLimiter",
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 3,
                        Window = TimeSpan.FromSeconds(10)
                    }));

                opt.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;

                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                        await context.HttpContext.Response
                            .WriteAsync($"Too many requests. Please try again after {retryAfter.TotalSeconds} second(s).", token);
                    else
                        await context.HttpContext.Response
                            .WriteAsync("Too many requests. Please try again later.", token);
                };
            });
        }
    }
}
