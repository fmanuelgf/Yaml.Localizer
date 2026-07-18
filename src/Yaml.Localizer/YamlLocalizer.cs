namespace Yaml.Localizer
{
    using System.Globalization;
    using Yaml.Localizer.Models;
    using YamlDotNet.Serialization;

    /// <summary>
    /// Provides localization functionality using YAML files.
    /// </summary>
    public class YamlLocalizer
    {
        private static CultureInfo culture = CultureInfo.CurrentCulture;
        private readonly CultureInfo defaultCulture = CultureInfo.CurrentCulture;
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
            this.defaultCulture = this.translationMappings.FirstOrDefault()?.Messages.Keys.FirstOrDefault()
                ?? CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// Gets or sets the current culture for localization.
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get => culture;
            set => culture = value;
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

            var result = data.Messages.FirstOrDefault(c =>
                c.Key.TextInfo.CultureName == culture.TextInfo.CultureName
            ).Value ?? data.Messages.FirstOrDefault(c =>
                c.Key.TwoLetterISOLanguageName == culture.TwoLetterISOLanguageName
            ).Value ?? data.Messages.FirstOrDefault().Value;
            
            return result ?? throw new KeyNotFoundException(
                $"Translation for language '{culture}' or default culture '{this.defaultCulture}' not found.");
        }
    }
} 