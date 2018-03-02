using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Plugin.Media.Abstractions;

namespace FaceFinder
{
    public class FacesViewModel : ViewModelBase
    {
        public FacesViewModel(MediaFile photo, IEnumerable<DetectedFace> faces)
        {
            Faces = faces.Select(f => new FaceViewModel(photo, f));
            SelectedFace = Faces.First();
        }

        public IEnumerable<FaceViewModel> Faces { get; }

        FaceViewModel _selectedFace;
        public FaceViewModel SelectedFace
        {
            get => _selectedFace;
            set => Set(ref _selectedFace, value);
        }
    }
}

