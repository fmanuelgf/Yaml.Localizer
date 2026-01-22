namespace Yaml.Localizer.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the YamlLocalizer as a singleton service.
        /// </summary>
        public static void RegisterYamlLocalizer(this IServiceCollection services, string yamlFilePath)
        {
            services.AddSingleton(sp => new YamlLocalizer(yamlFilePath));
        }
    }
}
