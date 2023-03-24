using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalXamarin.ViewModels
{
    public class UbicacionViewModel
    {
        public string Address { get; set; }

        public ICommand Exit => new Command(ButtonsPage);

        public async void ButtonsPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
         
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(MainPage))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }

        public UbicacionViewModel()
        {
            FillPage();
        }
        public async void FillPage()
        {
            var myDirec = (App.Current.Properties["Direc"].ToString());

            Address = myDirec;

        }
    }
}
