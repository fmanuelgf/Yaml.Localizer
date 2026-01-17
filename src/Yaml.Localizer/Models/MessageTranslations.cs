namespace Yaml.Localizer.Models
{
    using System.Globalization;

    public class MessageTranslations
    {
        public string Id { get; set; } = string.Empty;

        public Dictionary<CultureInfo, string> Messages { get; set; } = [];
    }
}