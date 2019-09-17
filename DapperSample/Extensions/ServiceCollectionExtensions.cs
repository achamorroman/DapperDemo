using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DapperDemo.Extensions
{
    namespace Microsoft.Extensions.DependencyInjection
    {
        public static class ServiceCollectionExtensions
        {
            public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
            {
                services.AddSwaggerGen(swagger =>
                {
                    var contact = new Contact() { Name = SwaggerConfiguration.ContactName, Url = SwaggerConfiguration.ContactUrl };
                    swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                        new Info
                        {
                            Title = SwaggerConfiguration.DocInfoTitle,
                            Version = SwaggerConfiguration.DocInfoVersion,
                            Description = SwaggerConfiguration.DocInfoDescription,
                            Contact = contact
                        }
                    );
                });

                return services;
            }
        }
    }
}
