namespace Yaml.Localizer.Tests.UnitTests
{
    using NUnit.Framework;
    using Yaml.Localizer.Tests.UnitTests.Base;

    public class YamlLocalizerUsingShortIsoCodeTests : UnitTests
    {
        [SetUp]
        public void Setup()
        {
            base.UseYaml("TestFiles/short-iso-codes.yaml");
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
            var result = this.Localizer[msgId];

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
            var result = this.Localizer[msgId];

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}