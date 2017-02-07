### Speech to Text

The IBM® [Speech to Text][speech-to-text] service provides an API that enables you to add IBM's speech recognition capabilities to your applications. The service transcribes speech from various languages and audio formats to text with low latency. For most languages, the service supports two sampling rates, broadband and narrowband.

### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.SpeechToText -Pre

```
#### Project.json
```JSON

"dependencies": {
   "IBM.WatsonDeveloperCloud.SpeechToText": "0.1.0-alpha"
}

```
### Usage
The Speech to Text API consists of the following groups of related calls:

* Models includes calls that return information about the models (languages and sampling rates) available for transcription.

* WebSockets includes a single call that establishes a persistent connection with the service over the WebSocket protocol.

* Sessionless includes HTTP calls that provide a simple means of transcribing audio without the overhead of establishing and maintaining a session.

* Sessions provides a collection of HTTP calls that provide a mechanism for a client to maintain a long, multi-turn exchange, or session, with the service or to establish multiple parallel conversations with a particular instance of the service.

* Asynchronous provides a non-blocking HTTP interface for transcribing audio. You can register a callback URL to be notified of job status and, optionally, results, or you can poll the service to learn job status and retrieve results manually.

* Custom models provides an HTTP interface for creating custom language models. The interface lets you expand the vocabulary of a base language model with domain-specific terminology.

* Custom corpora provides an HTTP interface for managing the corpora associated with a custom language model. You add a corpus to a custom model to extract words from the corpus into the model's vocabulary.

* Custom words provides an HTTP interface for managing individual words in a custom language model. You can add, list, and delete words from a custom model.

#### Instantiating and authenticating the service
Before you can send requests to the service it must be instantiated and credentials must be set.
```cs
//  create a Speech to Text Service instance
SpeechToTextService _speechToText = new SpeechToTextService();

//  set the credentials
_speechToText.SetCredential("<username>", "<password>");
```

#### Get models
Retrieves a list of all models available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz, among other things.
```cs
//  get a list of available speech models
var results = _speechToText.GetModels();
```

#### Get a model
Retrieves information about a single specified model that is available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz, among other things.
```cs
//  get details of a particular speech model
var results = _speechToText.GetModel("<model-name>");
```

<!-- #### Recognize audio via websockets
Sends audio and returns transcription results for recognition requests over a WebSocket connection. Requests and responses are enabled over a single TCP connection that abstracts much of the complexity of the request to offer efficient implementation, low latency, high throughput, and an asynchronous response. By default, only final results are returned for any request; to enable interim results, set the interim_results parameter to true.

The service imposes a data size limit of 100 MB per connection. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
```cs
``` -->

#### Recognize audio via file
Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results.
```cs
//  open and read an audio file
using (FileStream fs = File.OpenRead("<path-to-audio-file>"))
{
  //  get a transcript of the audio file
  var results = _speechToText.Recognize(fs);
}
```

<!-- #### Recognize audio via files
Sends audio and returns transcription results for a sessionless recognition request submitted as multipart form data. Returns only the final results.
```cs
``` -->

#### Create a session
Creates a session and locks recognition requests to that engine. You can use the session for multiple recognition requests so that each request is processed with the same engine. The session expires after 30 seconds of inactivity. Use the Get status method to prevent the session from expiring.
```cs
//  create a speech to text session
var results = _speechToText.CreateSession("<model-name>");
```

#### Get session status
Checks whether a specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The returned state must be initialized to indicate that you can send another recognition request. You can also use this method to prevent the session from expiring after 30 seconds of inactivity. The request must pass the cookie that was returned by the Create a session method.
```cs
//  get a session's status
var recognizeStatus = _speechToText.GetSessionStatus("<session-id>");
```

<!-- #### Observe session result
Requests results for a recognition task within a specified session. You can submit this method multiple times for the same recognition task. To see interim results, set the interim_results parameter to true. The request must pass the cookie that was returned by the Create a session method.
```cs
``` -->

<!-- #### Recognize session audio
Sends audio and returns transcription results for a session-based recognition request. By default, returns only the final transcription results for the request. To see interim results, set the parameter interim_results to true in a call to the Observe result method.
```cs
``` -->

<!-- #### Recognize multipart session audio
Sends audio and returns transcription results for a session-based recognition request submitted as multipart form data. By default, returns only the final transcription results for the request.
```cs
``` -->

#### Delete a session
Deletes an existing session and its engine. The request must pass the cookie that was returned by the Create a session method. You cannot send requests to a session after it is deleted. By default, a session expires after 30 seconds of inactivity if you do not delete it first.
```cs
//  deletes a speech to text session
_speechToText.DeleteSession("<session-id>");
```

<!-- #### Register a callback
Registers a callback URL with the service for use with subsequent asynchronous recognition requests. The service attempts to register, or white-list, the callback URL if it is not already registered by sending a GET request to the callback URL. The service passes a random alphanumeric challenge string via the challenge_string query parameter of the request.
```cs
``` -->

<!-- #### Create a job
Creates a job for a new asynchronous recognition request. The job is owned by the user whose service credentials are used to create it. How you learn the status and results of a job depends on the parameters you include with the job creation request:

* By callback notification: Include the callback_url query parameter to specify a URL to which the service is to send callback notifications when the status of the job changes. Optionally, you can also include the events and user_token query parameters to subscribe to specific events and to specify a string that is to be included with each notification for the job.

* By polling the service: Omit the callback_url, events, and user_token query parameters. You must then use the Check jobs or Check a job method to check the status of the job, using the latter to retrieve the results when the job is complete.

```cs
``` -->

<!-- #### Check jobs
Returns the status and ID of all outstanding jobs associated with the service credentials with which it is called. The method also returns the creation and update times of each job, and, if a job was created with a callback URL and a user token, the user token for the job. To obtain the results for a job whose status is completed, use the Check a job method. A job and its results remain available until you delete them with the Delete a job method or until the job's time to live expires, whichever comes first.
```cs
``` -->

<!-- #### Check a job
Returns information about a specified job. The response always includes the status of the job and its creation and update times. If the status is completed, the response also includes the results of the recognition request. You must submit the request with the service credentials of the user who created the job.
```cs
``` -->

<!-- #### Delete a job
Deletes the specified job. You cannot delete a job that the service is actively processing. Once you delete a job, its results are no longer available. The service automatically deletes a job and its results when the time to live for the results expires. You must submit the request with the service credentials of the user who created the job.
```cs
``` -->

<!-- #### Create a custom model
Creates a new custom language model for a specified base language model. The custom language model can be used only with the base language model for which it is created. The new model is owned by the individual whose service credentials are used to create it.
```cs
``` -->

<!-- #### List custom models
Lists information about all custom language models that are owned by the calling user. Use the language query parameter to see all custom models for the specified language; omit the parameter to see all custom models for all languages.
```cs
``` -->

<!-- #### List a custom model
Lists information about a custom language model. Only the owner of a custom model can use this method to query information about the model.
```cs
``` -->

<!-- #### Train a custom model
Initiates the training of a custom language model with new corpora, words, or both. After adding training data to the custom model with the corpora or words methods, use this method to begin the actual training of the model on the new data. You can specify whether the custom model is to be trained with all words from its words resources or only with words that were added or modified by the user. Only the owner of a custom model can use this method to train the model.
```cs
``` -->

<!-- #### Reset a custom model
Resets a custom language model by removing all corpora and words from the model. Resetting a custom model initializes the model to its state when it was first created. Metadata such as the name and language of the model are preserved. Only the owner of a custom model can use this method to reset the model.
```cs
``` -->

<!-- #### Upgrade a custom model
Upgrades a custom language model to the latest release level of the Speech to Text service. The method bases the upgrade on the latest trained data stored for the custom model. If the corpora or words for the model have changed since the model was last trained, you must use the Train a custom model method to train the model on the new data. Only the owner of a custom model can use this method to upgrade the model.

Note: This method is not currently implemented. It will be added for a future release of the API.
```cs
``` -->

<!-- #### Delete a custom model
Deletes an existing custom language model. The custom model cannot be deleted if another request, such as adding a corpus to the model, is currently being processed. Only the owner of a custom model can use this method to delete the model.
```cs
``` -->

<!-- #### Add a corpus
Adds a single corpus text file of new training data to the custom language model. Use multiple requests to submit multiple corpus text files. Only the owner of a custom model can use this method to add a corpus to the model. Note that adding a corpus does not affect the custom model until you train the model for the new data by using the Train a custom model method.
```cs
``` -->

<!-- #### List corpora
Lists information about all corpora that have been added to the specified custom language model. The information includes the total number of words and out-of-vocabulary (OOV) words, name, and status of each corpus. Only the owner of a custom model can use this method to list the model's corpora.
```cs
``` -->

<!-- #### List a corpus
Lists information about a single specified corpus. The information includes the total number of words and out-of-vocabulary (OOV) words, name, and status of the corpus. Only the owner of a custom model can use this method to list information about a corpus from the model.
```cs
``` -->

<!-- #### Delete a corpus
Deletes an existing corpus from a custom language model. The service removes any out-of-vocabulary (OOV) words associated with the corpus from the custom model's words resource unless they were also added by another corpus or they have been modified in some way with the Add custom words or Add a custom word method. Removing a corpus does not affect the custom model until you train the model with the Train a custom model method. Only the owner of a custom model can use this method to delete a corpus from the model.
```cs
``` -->

<!-- #### Add custom words
Adds one or more custom words to a custom language model. The service populates the words resource for a custom model with out-of-vocabulary (OOV) words found in each corpus added to the model. You can use this method to add additional words or to modify existing words in the words resource. Only the owner of a custom model can use this method to add or modify custom words associated with the model. Adding or modifying custom words does not affect the custom model until you train the model for the new data by using the Train a custom model method.
```cs
``` -->

<!-- #### Add a custom word
Adds a custom word to a custom language model. The service populates the words resource for a custom model with out-of-vocabulary (OOV) words found in each corpus added to the model. You can use this method to add additional words or to modify existing words in the words resource. Only the owner of a custom model can use this method to add or modify a custom word for the model. Adding or modifying a custom word does not affect the custom model until you train the model for the new data by using the Train a custom model method.
```cs
``` -->

<!-- #### List custom words
Lists information about all custom words from a custom language model. You can list all words from the custom model's words resource, only custom words that were added or modified by the user, or only OOV words that were extracted from corpora. Only the owner of a custom model can use this method to query the words from the model.
```cs
``` -->

<!-- #### List a custom word
Lists information about a custom word from a custom language model. Only the owner of a custom model can use this method to query a word from the model.
```cs
``` -->

<!-- #### Delete a custom word
Deletes a custom word from a custom language model. You can remove any word that you added to the custom model's words resource via any means. However, if the word also exists in the service's base vocabulary, the service removes only the custom pronunciation for the word; the word remains in the base vocabulary.

Removing a custom word does not affect the custom model until you train the model with the Train a custom model method. Only the owner of a custom model can use this method to delete a word from the model.
```cs
``` -->

[speech-to-text]: http://www.ibm.com/watson/developercloud/doc/speech-to-text/
