namespace Yaml.Localizer.Tests.UnitTests.Base
{
    using System.Globalization;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Yaml.Localizer.DependencyInjection;

    public abstract class UnitTests
    {
        protected YamlLocalizer Localizer { get; private set; }

        public void UseYaml(string yamlFilePath)
        {
            var services = new ServiceCollection();
            services.RegisterYamlLocalizer(yamlFilePath);
            var provider = services.BuildServiceProvider();
            this.Localizer = provider.GetRequiredService<YamlLocalizer>();
        }

        [TestCase("es", "foo")]
        [TestCase("zz", "GOODBYE")]
        public void CannotTranslateNonExistingMessage(string lang, string msgId)
        {
            // Arrange
            this.Localizer.CurrentCulture = new CultureInfo(lang);
            
            // Act
            // Assert
            Assert.Throws<KeyNotFoundException>(() =>
            {
                _ = this.Localizer[msgId];
            });
        }
    }
}