[![NuGet](https://img.shields.io/badge/nuget-v2.17.0-green.svg?style=flat)](https://www.nuget.org/packages/IBM.WatsonDeveloperCloud.CompareComply.v1/)

### Compare Comply
IBM Watson™ [Compare and Comply]() analyzes governing documents to provide details about critical aspects of the documents.

### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.ComnpareComply.v1

```
#### .csproj
```xml

<ItemGroup>
    <PackageReference Include="IBM.WatsonDeveloperCloud.CompareComply.v1" Version="2.17.0" />
</ItemGroup>

```

### Usage
About
IBM Watson™ Compare and Comply is a collection of advanced APIs that enable better and faster document understanding. The APIs are pre-trained to handle document conversion, table understanding, Natural Language Processing, and comparison for contracts. JSON output adds real power to end-user applications, for a wide variety of use cases. The machine learning feedback interface is available to collect your feedback which is then incorporated into regular improvements to the core NLP model; the more you use it, the better the system performs.

Compare and Comply is designed to provide:

- Natural language understanding of contracts and invoices
- The ability to convert programmatic or scanned PDF documents, Microsoft Word files, image files, and text files to annotated JSON or to HTML
- Identification of legal entities and categories that align with subject matter expertise
- The ability to extract table information from an input document
- The ability to compare two contracts
- Compare and Comply brings together a functionally rich set of integrated, automated Watson APIs to input a file to identify sections, lists (numbered and bulleted), footnotes, and tables, converting these items into a structured HTML format. Furthermore, classification of this structured format is annotated and output as JSON with labeled elements, types, categories, and other information.

#### HTML Conversion
Uploads a file. The response includes an HTML version of the document.
```cs
using (FileStream fs = File.OpenRead(<file-path>))
{
    var htmlConversionResult = service.ConvertToHtml(fs);
}
```






#### Classify Elements
Uploads a file. The response includes an analysis of the document’s structural and semantic elements.
```cs
using (FileStream fs = File.OpenRead(<file-path>))
{
    var elementClassificationResult = service.ClassifyElements(fs);
}
```






#### Extract Tables
Uploads a file. The response includes an analysis of the document’s tables.
```cs
using (FileStream fs = File.OpenRead(<file-path>))
{
    var extractTablesResult = service.ExtractTables(fs);
}
```







#### Compare Documents
Uploads two PDF or JSON files. The response includes JSON comparing the two documents. Uploaded files must be in the same file format.
```cs
 using (FileStream fs0 = File.OpenRead(<file-a-path>))
{
    using (FileStream fs1 = File.OpenRead(<file-b-path>))
    {
        var comparisonResults = service.CompareDocuments(fs0, fs1);
    }
}
```






#### List Feedback
Gets the list of batch-processing jobs submitted by users.
```cs
    var ListFeedbackResult = service.ListFeedback(
        feedbackType: "element_classification",
        before: <before>,
        after: <after>,
        documentTitle: "doc title",
        modelId: "contracts",
        modelVersion: "11.00",
        categoryRemoved: "categoryRemoved",
        categoryAdded: "categoryAdded",
        categoryNotChanged: "categoryUnchanged",
        typeRemoved: "nature:party",
        typeAdded: "nature:party",
        typeNotChanged: "nature:party",
        pageLimit: 3,
        sort: "sort"
        );
```






#### Add Feedback
Adds feedback in the form of labels from a subject-matter expert (SME) to a governing document.
Important: Feedback is not immediately incorporated into the training model, nor is it guaranteed to be incorporated at a later date. Instead, submitted feedback is used to suggest future updates to the training model.
```cs
var addFeedbackResult = service.AddFeedback(<feedbackData>);
```






#### Get Feedback
List a specified feedback entry
```cs
//  temporary fix for a bug requiring `x-watson-metadata` header
Dictionary<string, object> customData = new Dictionary<string, object>();
Dictionary<string, string> customHeaders = new Dictionary<string, string>();
customHeaders.Add("x-watson-metadata", "customer_id=sdk-test-customer-id");
customData.Add(Constants.CUSTOM_REQUEST_HEADERS, customHeaders);

var getFeedbackResult = service.GetFeedback(<feedback-id>, customData:customData);
```






#### Delete Feedback
Deletes a specified feedback entry
```cs
var deleteFeedbackResult = service.DeleteFeedback(<feedback-id>);
```






#### List Batches
Gets the list of submitted batch-processing jobs
```cs
var getBatchesResult = service.ListBatches();
```






#### Create Batch Request
Run Compare and Comply methods over a collection of input documents.
Important: Batch processing requires the use of the IBM Cloud Object Storage service. The use of IBM Cloud Object Storage with Compare and Comply is discussed at Using batch processing.
```cs
using (FileStream fsInput = File.OpenRead(<input-credentials-file-path>))
{
    using (FileStream fsOutput = File.OpenRead(<output-credentials-file-path>))
    {
        var createBatchResult = service.CreateBatch(
            <action>,
            fsInput,
            <input-storage-location>,
            <input-bucket-name>,
            fsOutput,
            <output-storage-location>,
            <output-bucket-name>
            );
        batchId = createBatchResult.BatchId;
    }
}
```






#### Get Batch
Gets information about a specific batch-processing request
```cs
var getBatchResult = service.GetBatch(<batch-id>);
```






#### Update Batch
Updates a pending or active batch-processing request. You can rescan the input bucket to check for new documents or cancel a request.
```cs
var updateBatchResult = service.UpdateBatch(<batch-id>, <action>);
```

[compare-comply]: https://cloud.ibm.com/docs/services/compare-comply/index.html
