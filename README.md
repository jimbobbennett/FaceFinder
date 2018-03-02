# Face Finder

This is a sample Xamarin app showing how to use the Azure Cognitive Services FaceAPIs to detect and analyse faces in a photo.

#### To build this app
Head to [the try cognitive services](https://aka.ms/Xjga43) page, click the bit __Get API Key__ button next to FaceApi, then sign in with your identitry provider of choice (Microsoft, Facebook, Github etc.).

Copy one of your two API keys, then open `ApiKeys.cs` and update the `FaceApiKey` field to contain your key. You will also need to update the `FaceApiRegion` field to match the __Endpoint__ value shown above your key. In code this is an `enum` so find the relevant vale that matches the URL shown in the endpoint.

For example - if your endpoint is _https://westcentralus.api.cognitive.microsoft.com/face/v1.0_ then set the region to 'AzureRegions.Westcentralus'.

Finally build and run the app on an iOS or Android device.

#### Using the app

Run the app, tap the __Take photo__ button and take a photo of some faces. You will then see a list of all the faces found in the image. Tap on each one to see more details.
