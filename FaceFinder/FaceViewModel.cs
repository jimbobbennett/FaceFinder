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
                face.FaceAttributes.Makeup.EyeMakeup.GetValueOrDefault(false) ? "Eyes" : "",
                face.FaceAttributes.Makeup.LipMakeup.GetValueOrDefault(false) ? "Lips" : "",
            }).Where(m => !string.IsNullOrEmpty(m));

            var makeups = string.Join(", ", makeup);
            return string.IsNullOrEmpty(makeups) ? "None" : makeups;
        }

        private string GetHair(DetectedFace face)
        {
            if (face.FaceAttributes.Hair.Invisible.GetValueOrDefault(false))
                return "Hidden";

            if (face.FaceAttributes.Hair.Bald.GetValueOrDefault(0) > 0.75)
                return "Bald";

            var color = face.FaceAttributes.Hair.HairColor.OrderByDescending(h => h.Confidence).FirstOrDefault();
            if (color == null)
                return "Unknown";

            return $"{color.Color}";
        }

        private string GetFacialHair(DetectedFace face)
        {
            if (face.FaceAttributes.FacialHair.Beard.GetValueOrDefault() < 0.1 &&
                face.FaceAttributes.FacialHair.Moustache.GetValueOrDefault() < 0.1 &&
                face.FaceAttributes.FacialHair.Sideburns.GetValueOrDefault() < 0.1)
                return "None";

            return $"Beard ({face.FaceAttributes.FacialHair.Beard.GetValueOrDefault():P0}), " +
                $"Moustache ({face.FaceAttributes.FacialHair.Moustache.GetValueOrDefault():P0}), " +
                $"Sideburns ({face.FaceAttributes.FacialHair.Sideburns.GetValueOrDefault():P0}), ";
        }

        private string GetEmotion(DetectedFace face)
        {
            var emotions = new Dictionary<string, double>
            {
                {nameof(face.FaceAttributes.Emotion.Anger), face.FaceAttributes.Emotion.Anger.GetValueOrDefault()},
                {nameof(face.FaceAttributes.Emotion.Contempt), face.FaceAttributes.Emotion.Contempt.GetValueOrDefault()},
                {nameof(face.FaceAttributes.Emotion.Disgust), face.FaceAttributes.Emotion.Disgust.GetValueOrDefault()},
                {nameof(face.FaceAttributes.Emotion.Fear), face.FaceAttributes.Emotion.Fear.GetValueOrDefault()},
                {nameof(face.FaceAttributes.Emotion.Happiness), face.FaceAttributes.Emotion.Happiness.GetValueOrDefault()},
                {nameof(face.FaceAttributes.Emotion.Neutral), face.FaceAttributes.Emotion.Neutral.GetValueOrDefault()},
                {nameof(face.FaceAttributes.Emotion.Sadness), face.FaceAttributes.Emotion.Sadness.GetValueOrDefault()},
                {nameof(face.FaceAttributes.Emotion.Surprise), face.FaceAttributes.Emotion.Surprise.GetValueOrDefault()},
            };

            return emotions.OrderByDescending(e => e.Value).First().Key;
        }
    }
}

