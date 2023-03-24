using FinalXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalXamarin.ViewModels
{
    public class RegisterPersonaViewModel
    {
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
        public ICommand Save => new Command(SaveRegister);
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public async void SaveRegister()
        {
            if (Nombre != null)
            {
                if (Telefono != null)
                {
                    if (Direccion != null)
                    {
                        var Persona = new PersonaModel()
                    {
                        Nombre = Nombre,
                        Telefono = Telefono,
                        Direccion = Direccion,
                    };
                    var Sav = await App.Database.SavePersonasAsync(Persona);
                    if (Sav != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "Esta persona ahora esta en tu lista de amigos", "ok");
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                        var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                        foreach (var page in existingPages)
                        {
                            if (page.GetType() == typeof(MainPage))
                                continue;
                            App.Current.MainPage.Navigation.RemovePage(page);
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "Esta persona no fue agregada a tu lista de amigos", "ok");
                    
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "Falta agregar el lugar de residencia de esta persona", "ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "Falta agregar el telefono de esta persona", "ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "se debe agregar el nombre de una persona", "ok");
            }

        }
    }
}
