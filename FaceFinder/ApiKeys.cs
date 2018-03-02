using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace FaceFinder
{
    public static class ApiKeys
    {
#error You need to set up your API keys.
        // To get a FaceAPI key, sign up at https://aka.ms/K5qesz
        // Once signed up you will see the Azure region for your service
        public const string FaceApiKey = "<Key goes here>";
        public const AzureRegions FaceApiRegion = AzureRegions.Westcentralus; 
    }
}
