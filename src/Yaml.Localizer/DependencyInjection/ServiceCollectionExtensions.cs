namespace Yaml.Localizer.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void RegisterYmlLocalizer(this IServiceCollection services, string yamlFilePath)
        {
            services.AddSingleton(sp => new YamlLocalizer(yamlFilePath));
        }
    }
}
