using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
          services.AddSwaggerGen(c =>
           {
             c.SwaggerDoc("v1",new OpenApiInfo {
                 Version = "v1",
                 Title = "API"
             });
            }
           );
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {

            app.UseSwagger();  
            app.UseSwaggerUI(options =>options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"));  
            return app;
        }
    }
}