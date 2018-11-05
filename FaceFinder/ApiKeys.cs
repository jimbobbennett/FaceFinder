namespace FaceFinder
{
    public static class ApiKeys
    {
#error You need to set up your API key
        // To get a FaceAPI key, create a Face resource on the Azure portal
        // Once signed up you will see the APi key and Azure endpoint for your service
        public const string FaceApiKey = "<Key goes here>";

#error You need to set up your Endpoint
        // Remember to strip off everything after microsoft.com if you copied the endpoint directly
        // from the portal, otherwise you will get a Not Found exception
        public const string FaceApiEndpoint = "https://<endpoint>.api.cognitive.microsoft.com"; 
    }
}
