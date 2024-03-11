using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;


namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class JsonSerializationConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }
    }
}
