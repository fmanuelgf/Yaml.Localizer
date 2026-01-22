namespace Yaml.Localizer.Models
{
    using System.Globalization;

    /// <summary>
    /// Represents translations for a specific message ID.
    /// </summary>
    public class MessageTranslations
    {
        /// <summary>
        /// Gets or sets the translation ID.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the dictionary of messages with their corresponding cultures.
        /// </summary>
        public Dictionary<CultureInfo, string> Messages { get; set; } = [];
    }
}