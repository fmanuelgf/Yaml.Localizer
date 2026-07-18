namespace Yaml.Localizer.Tests.UnitTests
{
    using System.Globalization;
    using NUnit.Framework;
    using Yaml.Localizer.Tests.UnitTests.Base;

    public class YamlLocalizerUsingLongIsoCodeTests : UnitTests
    {
        [SetUp]
        public void Setup()
        {
            base.UseYaml("TestFiles/long-iso-codes.yaml");
        }

        [TestCase("es", "MSG_GREETING", "Hola desde ES")]
        [TestCase("en", "MSG_GREETING", "Hello from UK")]
        [TestCase("es", "MSG_GOODBYE", "Adiós desde ES")]
        [TestCase("en", "MSG_GOODBYE", "Goodbye from UK")]
        [TestCase("es-ES", "MSG_GREETING", "Hola desde ES")]
        [TestCase("en-US", "MSG_GREETING", "Hello from US")]
        [TestCase("en-GB", "MSG_GREETING", "Hello from UK")]
        [TestCase("es-ES", "MSG_GOODBYE", "Adiós desde ES")]
        [TestCase("en-US", "MSG_GOODBYE", "Goodbye from US")]
        [TestCase("en-GB", "MSG_GOODBYE", "Goodbye from UK")]
        public void CanTranslateExistingMessage(string culture, string msgId, string expected)
        {
            // Arrange
            this.Localizer.CurrentCulture = new CultureInfo(culture);
            
            // Act
            var result = this.Localizer[msgId];

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("fr-FR", "MSG_GREETING", "Hola desde ES")]
        [TestCase("ch-CH", "MSG_GOODBYE", "Adiós desde ES")]
        public void CanTranslateExistingMessageToDefaultCulture(string culture, string msgId, string expected)
        {
            // Arrange
            this.Localizer.CurrentCulture = new CultureInfo(culture);
            
            // Act
            var result = this.Localizer[msgId];

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("es", "MSG_MULTILINE_TEXT", "Esto es un mensaje desde ES con\nmúltiples líneas\n")]
        [TestCase("en", "MSG_MULTILINE_TEXT", "This is a\nmultiline UK message\n")]
        [TestCase("es-ES", "MSG_MULTILINE_TEXT", "Esto es un mensaje desde ES con\nmúltiples líneas\n")]
        [TestCase("en-US", "MSG_MULTILINE_TEXT", "This is a\nmultiline US message\n")]
        [TestCase("en-GB", "MSG_MULTILINE_TEXT", "This is a\nmultiline UK message\n")]
        public void CanTranslateExistingMultilineMessage(string culture, string msgId, string expected)
        {
            // Arrange
            this.Localizer.CurrentCulture = new CultureInfo(culture);
            
            // Act
            var result = this.Localizer[msgId];

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}