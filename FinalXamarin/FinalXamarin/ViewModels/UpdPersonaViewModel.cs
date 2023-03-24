using FinalXamarin.View;
using FinalXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalXamarin.ViewModels
{
    public class UpdPersonaViewModel
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
        public UpdPersonaViewModel()
        {
            FillPage();
        }

        public string UpdNombre { get; set; }
        public string UpdTelefono { get; set; }
        public string UpdDireccion { get; set; }
        public ICommand UpdPersona => new Command(UpdatePersona);

        
        public async void FillPage()
        {
            var myNom = (App.Current.Properties["nom"].ToString());
            var myTel = (App.Current.Properties["Tel"].ToString());
            var myDirec = (App.Current.Properties["Direc"].ToString());

            UpdNombre = myNom;
            UpdTelefono = myTel;
            UpdDireccion = myDirec;
        }

        
        public async void UpdatePersona()
        {
            var myId = (App.Current.Properties["id"].ToString());
            if (UpdNombre != null)
            {
                if (UpdTelefono != null)
                {
                    if (UpdDireccion != null)
                    {
                        var good = new PersonaModel()
                    {
                        ID = int.Parse(myId),
                        Nombre = UpdNombre,
                        Telefono = UpdTelefono,
                        Direccion = UpdDireccion,
                    };
                    var save = await App.Database.SavePersonasAsync(good);
                    if (save != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "El registro ha sido modificado con exito", "ok");
                        await App.Current.MainPage.Navigation.PushAsync(new PersonaListPage());
                        
                        var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                        foreach (var page in existingPages)
                        {
                            if (page.GetType() == typeof(PersonaListPage))
                                continue;
                            App.Current.MainPage.Navigation.RemovePage(page);

                            }
                    }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "El campo del Direccion esta vacio, por favor ingreselo", "ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "El campo del Telefono esta vacio, por favor ingreselo", "ok");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "El campo de Nombre esta vacio, por favor ingrese descripcion", "ok");
            }
        }
    }
}
