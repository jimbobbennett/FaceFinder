using System;
using Xamarin.Forms;

namespace FaceFinder
{
    public partial class FaceListPage : ContentPage
    {
        public FaceListPage()
        {
            InitializeComponent();
        }

        private void HandleItemTapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new FaceDetailsPage { BindingContext = ((FacesViewModel)BindingContext).SelectedFace });
        }
    }
}
