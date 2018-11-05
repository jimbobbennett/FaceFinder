using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace FaceFinder
{
    public class FaceViewModel : ViewModelBase
    {
        public StreamImageSource Photo { get; }
        public string Description { get; }
        public string Details { get; }

        public FaceViewModel(MediaFile photo, DetectedFace face)
        {
            Photo = (StreamImageSource)ImageSource.FromStream(() => photo.GetStreamWithImageRotatedForExternalStorage());

            var sb = new StringBuilder();
            sb.AppendLine($"Glasses: {face.FaceAttributes.Glasses}");
            sb.AppendLine($"Smile: {face.FaceAttributes.Smile:P0}");
            sb.AppendLine($"Makeup: {GetMakeup(face)}");
            sb.AppendLine($"Hair: {GetHair(face)}");
            sb.AppendLine($"Facial Hair: {GetFacialHair(face)}");
            sb.AppendLine($"Emotion: {GetEmotion(face)}");

            Details = sb.ToString();
            Description = $"{face.FaceAttributes.Age:N0} year old {face.FaceAttributes.Gender}";
        }

        private static string GetMakeup(DetectedFace face)

        {
            var makeup = (new[]
            {
                face.FaceAttributes.Makeup.EyeMakeup ? "Eyes" : "",
                face.FaceAttributes.Makeup.LipMakeup ? "Lips" : "",
            }).Where(m => !string.IsNullOrEmpty(m));

            var makeups = string.Join(", ", makeup);
            return string.IsNullOrEmpty(makeups) ? "None" : makeups;
        }

        private string GetHair(DetectedFace face)
        {
            if (face.FaceAttributes.Hair.Invisible)
                return "Hidden";

            if (face.FaceAttributes.Hair.Bald > 0.75)
                return "Bald";

            var color = face.FaceAttributes.Hair.HairColor.OrderByDescending(h => h.Confidence).FirstOrDefault();
            if (color == null)
                return "Unknown";

            return $"{color.Color}";
        }

        private string GetFacialHair(DetectedFace face)
        {
            if (face.FaceAttributes.FacialHair.Beard < 0.1 &&
                face.FaceAttributes.FacialHair.Moustache < 0.1 &&
                face.FaceAttributes.FacialHair.Sideburns < 0.1)
                return "None";

            return $"Beard ({face.FaceAttributes.FacialHair.Beard:P0}), " +
                $"Moustache ({face.FaceAttributes.FacialHair.Moustache:P0}), " +
                $"Sideburns ({face.FaceAttributes.FacialHair.Sideburns:P0}), ";
        }

        private string GetEmotion(DetectedFace face)
        {
            var emotions = new Dictionary<string, double>
            {
                {nameof(face.FaceAttributes.Emotion.Anger), face.FaceAttributes.Emotion.Anger},
                {nameof(face.FaceAttributes.Emotion.Contempt), face.FaceAttributes.Emotion.Contempt},
                {nameof(face.FaceAttributes.Emotion.Disgust), face.FaceAttributes.Emotion.Disgust},
                {nameof(face.FaceAttributes.Emotion.Fear), face.FaceAttributes.Emotion.Fear},
                {nameof(face.FaceAttributes.Emotion.Happiness), face.FaceAttributes.Emotion.Happiness},
                {nameof(face.FaceAttributes.Emotion.Neutral), face.FaceAttributes.Emotion.Neutral},
                {nameof(face.FaceAttributes.Emotion.Sadness), face.FaceAttributes.Emotion.Sadness},
                {nameof(face.FaceAttributes.Emotion.Surprise), face.FaceAttributes.Emotion.Surprise},
            };

            return emotions.OrderByDescending(e => e.Value).First().Key;
        }
    }
}

