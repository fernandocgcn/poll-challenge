using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PollChallenge.Api.Extensions
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwaggerDoc(this IServiceCollection services, string description)
        {
            services.AddOpenApiDocument(config =>
            {
                config.Version = "v1";
                config.AllowReferencesWithProperties = true;
                config.Title = "API Documentation: " + Assembly.GetEntryAssembly().GetName().Name;
                config.Description = description;
            });
        }

        public static void ConfigureSwaggerUI(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(config => config.TransformToExternalPath = (internalUiRoute, request) =>
            {
                config.Path = "/swagger";
                return internalUiRoute.StartsWith("/") == true && internalUiRoute.StartsWith(request.PathBase) == false
                    ? request.PathBase + internalUiRoute
                    : internalUiRoute;
            });
        }
    }
}
