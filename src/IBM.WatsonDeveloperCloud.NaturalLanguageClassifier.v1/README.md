[![NuGet](https://img.shields.io/badge/nuget-v2.14.0-green.svg?style=flat)](https://www.nuget.org/packages/IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1/)

### Natural Language Classifier
IBM Watson™ [Natural Language Classifier][natural_language_classifier] uses machine learning algorithms to return the top matching predefined classes for short text input. You create and train a classifier to connect predefined classes to example texts so that the service can apply those classes to new inputs.

### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1

```
#### .csproj
```xml

<ItemGroup>
    <PackageReference Include="IBM.WatsonDeveloperCloud.NaturalLangaugeClassifier.v1" Version="2.14.0" />
</ItemGroup>

```
### Usage
IBM Watson™ Natural Language Classifier can help your application understand the language of short texts and make predictions about how to handle them. A classifier learns from your example data and then can return information for texts that it is not trained on.

You can create and train a classifier in less than 15 minutes.

#### Analyze
Analyze features of natural language content.
```cs
ClassifyInput classifyInput = new ClassifyInput
    {
        Text = _textToClassify
    };

classifyResult = Classify(classifierId, classifyInput);
```

#### List Models
List available [custom models][custom_models].
```cs
var listClassifiersResult = ListClassifiers();
```

[natural_language_classifier]: https://console.bluemix.net/docs/services/natural-language-classifier/getting-started.html
