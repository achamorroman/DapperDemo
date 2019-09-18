using Microsoft.Extensions.DependencyInjection;

namespace DapperDemo.Extensions
{
    namespace Microsoft.Extensions.DependencyInjection
    {
        public static class ServiceCollectionExtensions
        {
            public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
            {
                services.AddSwaggerDocument(config =>
                {
                    config.PostProcess = document =>
                    {
                        document.Info.Version = SwaggerConfiguration.DocInfoVersion;
                        document.Info.Title = SwaggerConfiguration.DocInfoTitle;
                        document.Info.Description = SwaggerConfiguration.DocInfoDescription;
                        document.Info.TermsOfService = "None";
                        document.Info.Contact = new NSwag.OpenApiContact
                        {
                            Name = SwaggerConfiguration.ContactName,
                            Email = SwaggerConfiguration.ContactEmail,
                            Url = SwaggerConfiguration.ContactUrl
                        };
                        document.Info.License = new NSwag.OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = "https://example.com/license"
                        };
                    };
                });

                return services;
            }
        }
    }
}
