namespace Yaml.Localizer
{
    using Yaml.Localizer.Models;
    using YamlDotNet.Serialization;

    /// <summary>
    /// Provides localization functionality using YAML files.
    /// </summary>
    public class YamlLocalizer
    {
        private readonly List<MessageTranslations> translationMappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlLocalizer"/> class.
        /// </summary>
        /// <param name="yamlFilePath">Path to the YAML file containing the translations.</param>
        public YamlLocalizer(string yamlFilePath)
        {
            var reader = File.OpenText(yamlFilePath);
            var input = reader.ReadToEnd();
            reader.Close();

            var deserializer = new DeserializerBuilder().Build();
            this.translationMappings = deserializer.Deserialize<List<MessageTranslations>>(input);
        }

        /// <summary>
        /// Gets the localized string for the specified message ID.
        /// </summary>
        /// <param name="id">The message ID.</param>
        /// <returns>The localized string.</returns>
        public string this[string id] => this.GetTranslation(id);

        private string GetTranslation(string id)
        {   
            var data = this.translationMappings.FirstOrDefault(t => t.Id == id)
                ?? throw new KeyNotFoundException($"Translation ID '{id}' not found.");

            var currentCulture = Thread.CurrentThread.CurrentCulture;
            
            var result = data.Messages.FirstOrDefault(c =>
                c.Key.TextInfo.EBCDICCodePage == currentCulture.TextInfo.EBCDICCodePage
            ).Value ?? data.Messages.FirstOrDefault(c =>
                c.Key.TwoLetterISOLanguageName == currentCulture.TwoLetterISOLanguageName
            ).Value;
            
            return result ?? throw new KeyNotFoundException(
                $"Translation for language '{Thread.CurrentThread.CurrentCulture}' not found.");
        }
    }
} 