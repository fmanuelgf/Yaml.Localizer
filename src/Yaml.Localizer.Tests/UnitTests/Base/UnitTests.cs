namespace Yaml.Localizer.Tests.UnitTests.Base
{
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
        public void CannotTranslateNonExistingMessageOrCulture(string lang, string msgId)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
            
            // Act
            // Assert
            Assert.Throws<KeyNotFoundException>(() =>
            {
                _ = this.Localizer[msgId];
            });
        }
    }
}