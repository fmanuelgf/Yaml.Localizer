
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

```yml
- Id: "{message-id}}"
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
public class ExampleClass
{
    private readonly YamlLocalizer localizer;

    public ExampleClass(YamlLocalizer localizer)
    {
        this.localizer = localizer;
    }

    public string GetTranslatedMessage(string msgId)
    {
        return this.localizer[msgId];
    }
}
```
