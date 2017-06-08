# .IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model.Warning
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**WarningId** | **string** | The identifier of the warning message, one of `WORD_COUNT_MESSAGE`, `JSON_AS_TEXT`, `CONTENT_TRUNCATED`, or `PARTIAL_TEXT_USED`. | 
**Message** | **string** | The message associated with the `warning_id`. For `WORD_COUNT_MESSAGE`, "There were {number} words in the input. We need a minimum of 600, preferably 1,200 or more, to compute statistically significant estimates."; for `JSON_AS_TEXT`, "Request input was processed as text/plain as indicated, however detected a JSON input. Did you mean application/json?"; for `CONTENT_TRUNCATED`, "For maximum accuracy while also optimizing processing time, only the first 250KB of input text (excluding markup) was analyzed. Accuracy levels off at approximately 3,000 words so this did not affect the accuracy of the profile."; and for `PARTIAL_TEXT_USED`, "The text provided to compute the profile was trimmed for performance reasons. This action does not affect the accuracy of the output, as not all of the input text was required." The `PARTIAL_TEXT_USED` warning applies only when Arabic input text exceeds a threshold at which additional words do not contribute to the accuracy of the profile. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

