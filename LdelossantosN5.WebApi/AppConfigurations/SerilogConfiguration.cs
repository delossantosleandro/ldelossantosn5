using Serilog;
using Serilog.Events;

namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class SerilogConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console() //Just for the sake of this POC
                .CreateLogger();

            builder.Host.UseSerilog();
        }

        public static void ConfigureMiddleware(WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                Log.Information("API Call: {Method} {Path}", context.Request.Method, context.Request.Path);
                await next();
                Log.Information("API Response: {Method} {Path}", context.Request.Method, context.Request.Path);
            });
        }
    }
}