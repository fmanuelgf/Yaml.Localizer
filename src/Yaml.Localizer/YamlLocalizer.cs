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
        private readonly CultureInfo defaultCulture;
        private readonly List<MessageTranslations> translationMappings;
        private CultureInfo currentCulture;

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

            this.currentCulture = defaultCulture;
        }

        /// <summary>
        /// Gets the localized string for the specified message ID.
        /// </summary>
        /// <param name="id">The message ID.</param>
        /// <returns>The localized string.</returns>
        public string this[string id] => this.GetTranslation(id);

        /// <summary>
        /// Gets or sets the current culture for localization.
        /// </summary>
        [Obsolete("This propery will be removed in future releases. Please use the 'UseCultureOrDefault' method instead.")]
        public CultureInfo CurrentCulture
        {
            get
            {
                return this.currentCulture;
            }
            set
            {
                this.currentCulture = value;
            }
        }

        /// <summary>
        /// Sets the specified valid locale; otherwise, the default value will be used.
        /// </summary>
        /// <param name="lang">The ISO code for the locale.</param>
        public void UseCultureOrDefault(string? lang)
        {
            try
            {
                this.currentCulture = string.IsNullOrEmpty(lang)
                    ? this.defaultCulture
                    : new CultureInfo(lang);
            }
            catch
            {
                this.currentCulture = this.defaultCulture;
            }
        }

        private string GetTranslation(string id)
        {   
            var data = this.translationMappings.FirstOrDefault(t => t.Id == id)
                ?? throw new KeyNotFoundException($"Translation ID '{id}' not found.");

            var result = data.Messages.FirstOrDefault(c =>
                c.Key.TextInfo.CultureName == this.currentCulture.TextInfo.CultureName
            ).Value ?? data.Messages.FirstOrDefault(c =>
                c.Key.TwoLetterISOLanguageName == this.currentCulture.TwoLetterISOLanguageName
            ).Value ?? data.Messages.FirstOrDefault(c => c.Key == this.defaultCulture).Value;
            
            return result ?? throw new KeyNotFoundException(
                $"Translation for language '{this.currentCulture}' or default culture '{this.defaultCulture}' not found.");
        }
    }
} 