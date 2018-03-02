using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FaceFinder
{
    public class FaceMasterDetailPage : MasterDetailPage
    {
        public FaceMasterDetailPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            Master = new FaceListPage();

            Detail = new FaceDetailsPage();
            Detail.SetBinding(BindingContextProperty, "SelectedFace");
        }
    }
}

