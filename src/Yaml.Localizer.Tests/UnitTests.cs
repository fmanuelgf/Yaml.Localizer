namespace Yaml.Localizer.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Yaml.Localizer;
    using Yaml.Localizer.DependencyInjection;

    public class UnitTests
    {
        private YamlLocalizer ymlLocalizer;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.RegisterYamlLocalizer("TestFiles/test.yaml");
            var provider = services.BuildServiceProvider();
            this.ymlLocalizer = provider.GetRequiredService<YamlLocalizer>();
        }

        [TestCase("es", "MSG_GREETING", "Hola")]
        [TestCase("en", "MSG_GREETING", "Hello")]
        [TestCase("es", "MSG_GOODBYE", "Adiós")]
        [TestCase("en", "MSG_GOODBYE", "Goodbye")]
        [TestCase("es-ES", "MSG_GREETING", "Hola")]
        [TestCase("en-US", "MSG_GREETING", "Hello")]
        [TestCase("en-GB", "MSG_GREETING", "Hello")]
        [TestCase("es-ES", "MSG_GOODBYE", "Adiós")]
        [TestCase("en-US", "MSG_GOODBYE", "Goodbye")]
        [TestCase("en-GB", "MSG_GOODBYE", "Goodbye")]
        public void CanTranslateExistingMessage(string culture, string msgId, string expected)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            
            // Act
            var result = this.ymlLocalizer[msgId];

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("es", "MSG_MULTILINE_TEXT", "Esto es un mensaje con\nmúltiples líneas\n")]
        [TestCase("en", "MSG_MULTILINE_TEXT", "This is a\nmultiline message\n")]
        [TestCase("es-ES", "MSG_MULTILINE_TEXT", "Esto es un mensaje con\nmúltiples líneas\n")]
        [TestCase("en-US", "MSG_MULTILINE_TEXT", "This is a\nmultiline message\n")]
        [TestCase("en-GB", "MSG_MULTILINE_TEXT", "This is a\nmultiline message\n")]
        public void CanTranslateExistingMultilineMessage(string culture, string msgId, string expected)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            
            // Act
            var result = this.ymlLocalizer[msgId];

            // Assert
            Assert.That(result, Is.EqualTo(expected));
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
                _ = this.ymlLocalizer[msgId];
            });
        }
    }
}