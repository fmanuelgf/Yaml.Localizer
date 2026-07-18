
# Yaml.Localizer

A tool for managing localized texts via a YAML definition file.

## Usage

First, register `YamlLocalizer` in the services collection.

```csharp
using Yaml.Localizer.DependencyInjection;

...

services.RegisterYamlLocalizer("{path-to-the-yaml-file}.yaml");
```

Ensure the YAML file has the correct format.
NOTE: The default culture will be the first `iso-code` of the first `message-id`.

```yml
- Id: "{message-id}"
  Messages:
    {iso-code1}: "{text1}"
    {iso-code2}: "{text2}"
    ...
```

>*Example*

```yaml
- Id: "MSG_GREETING"
  Messages:
    es: "Hola"
    en: "Hello"

- Id: "MSG_GOODBYE"
  Messages:
    es: "Adiós"
    en: "Goodbye"

- Id: "MSG_MULTILINE_TEXT"
  Messages:
    es: |
      Esto es un mensaje con
      múltiples líneas
    en: |
      This is a
      multiline message
```

Then, in order to get a translated text for your app's current culture, you can do as in this example:

>*Example*

```csharp
[TestCase("es", "MSG_GREETING", "Hola")]
[TestCase("en", "MSG_GREETING", "Hello")]
[TestCase("en-GB", "MSG_GOODBYE", "Goodbye")]
public void CanTranslateExistingMessage(string culture, string msgId, string expected)
{
    // Arrange
    this.Localizer.CurrentCulture = new CultureInfo(culture);
    
    // Act
    var result = this.Localizer[msgId];

    // Assert
    Assert.That(result, Is.EqualTo(expected));
}
```
