using FinalXamarin.Models;
using FinalXamarin.Google;
using FinalXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace FinalXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UbicacionPage : ContentPage
    {
        public UbicacionPage()
        {
            InitializeComponent();
        }
        public async void Button_Clicked(object sender, EventArgs e)
        {
            var main = (UbicacionViewModel)BindingContext;
            if (!string.IsNullOrEmpty(main.Address))
            {
                map.Pins.Clear();
                var location = await googleApi.GetLocation(main.Address);
                var coordinates = location.results.FirstOrDefault().geometry.location;
                var pin = new Pin
                {
                    Position = new Position(coordinates.lat, coordinates.lng),
                    Label = location.results.FirstOrDefault().formatted_address,
                    Type = PinType.SearchResult
                };
                map.Pins.Add(pin);
            }

        }
    }
}