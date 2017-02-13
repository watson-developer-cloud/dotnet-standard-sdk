### Visual Recognition
The IBM Watson™ [Visual Recognition][visual-recognition] service will be abstracted into the .NET Standard SDK in the future.
<!-- The IBM Watson™ [Visual Recognition][visual-recognition] service uses deep learning algorithms to identify scenes, objects, and celebrity faces in images you upload to the service. You can create and train a custom classifier to identify subjects that suit your needs. -->

<!-- ### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.VisualRecognition -Pre

```
#### Project.json
```JSON

"dependencies": {
   "IBM.WatsonDeveloperCloud.VisualRecognition": "0.1.1-alpha"
}

``` -->
<!-- ### Usage
The IBM Watson™ [Visual Recognition][visual-recognition] service uses deep learning algorithms to identify scenes, objects, and faces in images you upload to the service. You can create and train a custom classifier to identify subjects that suit your needs. You can create and add images to a collection and then search that collection with your own image to find similar images. A valid API Key from Bluemix is required for all calls. -->

<!-- #### Classify an image
Upload images or URLs to identify classes by default. To identify custom classifiers, include the classifier_ids or owners parameters. Images must be in .jpeg, or .png format.

For each image, the response includes a score for each class within each selected classifier. Scores range from 0 - 1 with a higher score indicating greater likelihood of the class being depicted in the image. The default threshold for reporting scores from a classifier is 0.5. We recommend an image that is a minimum of 224 x 224 pixels for best quality results.
```cs
``` -->

<!-- #### Detect faces
Analyze faces in images and get data about them, such as estimated age, gender, plus names of celebrities. Images must be in .jpeg, or .png format. This functionality is not trainable, and does not support general biometric facial recognition.

For each image, the response includes face location, a minimum and maximum estimated age, a gender, and confidence scores. Scores range from 0 - 1 with a higher score indicating greater correlation.
```cs
``` -->

<!-- #### Create a classifier
Train a new multi-faceted classifier on the uploaded image data. A new custom classifier can be trained by several compressed (.zip) files, including files containing positive or negative images (.jpg, or .png). You must supply at least two compressed files, either two positive example files or one positive and one negative example file.

Compressed files containing positive examples are used to create “classes” that define what the new classifier is. The prefix that you specify for each positive example parameter is used as the class name within the new classifier. The `_positive_examples` suffix is required. There is no limit on the number of positive example files you can upload in a single call.

The compressed file containing negative examples is not used to create a class within the created classifier, but does define what the new classifier is not. Negative example files should contain images that do not depict the subject of any of the positive examples. You can only specify one negative example file in a single call. For more information, see [Structure of the training data][structure-of-the-training-data], and [Guidelines for good training][guidelines-for-good-training].
```cs
``` -->

<!-- #### Retrieve a list of custom classifiers
Retrieve a list of user-created classifiers.
```cs
``` -->

<!-- #### Retrieve classifier details
Retrieve information about a specific classifier.
```cs
``` -->

<!-- #### Update a classifier
Update an existing classifier by adding new classes, or by adding new images to existing classes.You cannot update a custom classifier with a free API Key.

To update the existing classifier, use several compressed (.zip) files, including files containing positive or negative images (.jpg, or .png). You must supply at least one compressed file, with additional positive or negative examples.

Compressed files containing positive examples are used to create and update “classes” to impact all of the classes in that classifier. The prefix that you specify for each positive example parameter is used as the class name within the new classifier. The `_positive_examples` suffix is required. There is no limit on the number of positive example files you can upload in a single call.

The compressed file containing negative examples is not used to create a class within the created classifier, but does define what the updated classifier is not. Negative example files should contain images that do not depict the subject of any of the positive examples. You can only specify one negative example file in a single call. For more information, see [Updating custom classifiers][updating-custom-classifiers].
```cs
``` -->

<!-- #### Delete a classifier
Delete a custom classifier with the specified classifier ID.
```cs
``` -->

<!-- #### Create a collection
Create a new collection of images to search. You can create a maximum of 5 collections.
```cs
``` -->

<!-- #### List collections
List all custom collections.
```cs
``` -->

<!-- #### Retrieve collection details
Retrieve information about a specific collection.
```cs
``` -->

<!-- #### Delete a collection
Delete a user created collection.
```cs
``` -->

<!-- #### Add images to a collection
Add images to a collection. Each collection can contain 1000000 images. It takes 1 second to upload 1 images, so uploading 1000000 images takes 11 days.
```cs
``` -->

<!-- #### List images in a collection
List 100 images in a collection. This returns an arbitrary selection of 100 images. Each collection can contain 1000000 images.
```cs
``` -->

<!-- #### List image details
List details about a specific image in a collection.
```cs
``` -->

<!-- #### Delete an imnage
Delete an image from a collection.
```cs
``` -->

<!-- #### Add or update metadata
Add metadata to a specific image in a collection. Use metadata for your own reference to identify images. You cannot filter the find_similar method by metadata.
```cs
``` -->

<!-- #### List metadata
View the metadata for a specific image in a collection.
```cs
``` -->

<!-- #### Delete metadata
Delete all metadata associated with an image.
```cs
``` -->

<!-- #### Find similar images
Upload an image to find similar images in your custom collection.
```cs
``` -->

[visual-recognition]: http://www.ibm.com/watson/developercloud/visual-recognition/api/v3/
[structure-of-the-training-data]: http://www.ibm.com/watson/developercloud/doc/visual-recognition/customizing.shtml#structure
[guidelines-for-good-training]: http://www.ibm.com/watson/developercloud/doc/visual-recognition/customizing.shtml#goodtraining
[updating-custom-classifiers]: http://www.ibm.com/watson/developercloud/doc/visual-recognition/customizing.shtml#retrain
