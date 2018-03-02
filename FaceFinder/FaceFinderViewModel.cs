using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Azure.CognitiveServices.Vision;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace FaceFinder
{
    public class FaceFinderViewModel : ViewModelBase
    {
        FaceAPI _FaceFinder;

        public FaceFinderViewModel()
        {
            TakePhotoCommand = new Command(async () => await TakePhoto());

            _FaceFinder = new FaceAPI(new ApiKeyServiceClientCredentials(ApiKeys.FaceApiKey))
            {
                AzureRegion = ApiKeys.FaceApiRegion
            };
        }

        MediaFile _photo;
        StreamImageSource _photoSource;
        public StreamImageSource PhotoSource
        {
            get => _photoSource;
            set => Set(ref _photoSource, value);
        }

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public ICommand TakePhotoCommand { get; }
        public ICommand PostCommand { get; }

        async Task TakePhoto()
        {
            try
            {
                IsBusy = true;

                _photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
                PhotoSource = (StreamImageSource)ImageSource.FromStream(() => _photo.GetStream());

                using (var s = _photo.GetStreamWithImageRotatedForExternalStorage())
                {
                    var fo = new FaceOperations(_FaceFinder);
                    var fa = Enum.GetValues(typeof(FaceAttributeTypes))
                                 .OfType<FaceAttributeTypes>()
                                 .ToList();
                    var faces = await fo.DetectInStreamAsync(s, true, true, fa);

                    if (faces.Any())
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new FaceListPage { BindingContext = new FacesViewModel(_photo, faces) });
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Where are you?", "No faces found, please try again.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("An error occured", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
