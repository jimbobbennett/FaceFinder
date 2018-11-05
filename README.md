# Face Finder

This is a sample Xamarin app showing how to use the Azure Cognitive Services FaceAPIs to detect and analyse faces in a photo.

Read more on how this works and how to use it from [this blog post](https://www.jimbobbennett.io/face-identification-with-azure-faceapi/).

#### Getting started with FaceAPI

From the [Azure Portal](https://portal.azure.com/?WT.mc_id=azureportal-blog-jabenn), select __Create a resource__, search for _"Face"_, and select __Face__ from the __AI + Machine Learning__ category. The click __Create__.

![Selecting the Face API](https://www.jimbobbennett.io/content/images/2018/11/2018-11-05_11-20-49.png)

Enter a name for this resource, select your subscription and the location nearest to you. For the pricing tier, there is a free tier called __F0__ that gives you 30,000 calls per month at a rate of no more than 20 per minute, and you can have one face resource per subscription with this tier. After this there is a paid tier limited to 10 calls per second and you pay per 1,000 calls - at the time of writing this is US$0.25 per 1,000 calls.

![The pricing matrix for Face calls](https://www.jimbobbennett.io/content/images/2018/11/2018-11-05_11-24-47.png)

Choose a resource group, or create a new one and click __Create__. Once it has been created, head to it and grab an API key from _Resource Management->Keys_, and the endpoint from the _Overview_ blade.

#### Building and running the app

The Face Finder app is pretty complete, all you need to do is update the `ApiKeys.cs` file with your API key and endpoint. For the `FaceApiKey`, copy yours and paste it in. For the `FaceApiEndpoint`, paste the value for the endpoint, removing everything past `microsoft.com`. For example, for me the endpoint shown in the Azure portal is _https://westeurope.api.cognitive.microsoft.com/face/v1.0_, so I would set the endpoint to `https://westeurope.api.cognitive.microsoft.com`.

> If you get a _Not Found_ exception, then check your endpoint - this exception is thrown if you don't remove everything past `microsoft.com`.

Once you have done this, build and run the app. When it loads, tap the __Take photo__ button and take a picture of one or more faces. The app will then show a list of all the faces detected, describing them using the detected age and gender. Tap on a face in the list to see more details, including if that person is smiling, if they are wearing glasses, what hair, facial hair and makeup they have, and their emotion.
