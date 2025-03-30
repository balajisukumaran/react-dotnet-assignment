using MyApi.BackingServices;
using MyApi.ApplicationServices;
using AspNetCoreRateLimit;

namespace MyApi
{
    /// <summary>
    /// Entry point for the MyApi application.
    /// Configures services, middleware, and application settings.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that sets up and runs the web application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient<IJsonFeed, BackingServices.JsonFeed>();
            builder.Services.AddScoped<IJokeService, JokeService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Get the XML comments file path.
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Configure logging based on environment.
            builder.Logging.ClearProviders();

            if (builder.Environment.IsDevelopment())
            {
                builder.Logging.AddConsole();
                builder.Logging.AddDebug();
                builder.Logging.SetMinimumLevel(LogLevel.Debug);

                // Enable CORS to allow react app to access the API
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                });
            }
            else
            {
                builder.Logging.AddConsole();
                builder.Logging.SetMinimumLevel(LogLevel.Warning);

                // Read allowed CORS origins from configuration
                var corsOrigins = builder.Configuration["CorsOrigins"]
                                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(origin => origin.Trim())
                                    .ToArray();

                // Add a CORS policy for production using the allowed origins
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("ProductionCors", policy =>
                    {
                        policy.WithOrigins(corsOrigins)
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
                });

                // Enable rate limiting only in production.
                builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
                builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
                builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
                builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
                builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
                builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
                builder.Services.AddMemoryCache();
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable CORS to allow react app to access the API
                app.UseCors("AllowAll");
            }

            // Enable rate limiting only in production.
            if (app.Environment.IsProduction())
            {
                app.UseCors("ProductionCors");
                app.UseIpRateLimiting();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}