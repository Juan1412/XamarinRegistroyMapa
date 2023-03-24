using FinalXamarin.Models;
using FinalXamarin.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalXamarin.ViewModels
{
    public class ListPersonaViewModel
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

        public ICommand Delete => new Command(DeletePersona);
        public ICommand Ubicacion => new Command(UbicacionPersona);
        public ICommand Update => new Command(UpdatePersona);
        public PersonaModel SelectPersona { get; set; }
        public ObservableCollection<PersonaModel> ListPersonas { get; set; }

        public async void DeletePersona()
        {
            if (SelectPersona != null)
            {
                var res = await App.Current.MainPage.DisplayAlert("Alerta", "¿Quieres eliminar a eesta persona de tu lista de amigos?", "si", "no");
                if (res)
                {
                    var del = App.Database.DeletePersonasAsync(SelectPersona);
                    await App.Current.MainPage.DisplayAlert("Alerta", "Esta persona ha sido eliminada de tu lista de amigos", "ok");
                    await App.Current.MainPage.Navigation.PushAsync(new PersonaListPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "Esta persona no pudo ser elimindad de tu lista de amigos", "ok");
                    await App.Current.MainPage.Navigation.PushAsync(new PersonaListPage());
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Selecciona la persona que quieras eliminar de tu lista de amigos", "ok");
            }
        }

        public async void UbicacionPersona()
        {
            if (SelectPersona != null)
            {
                App.Current.Properties["id"] = SelectPersona.ID;
                App.Current.Properties["nom"] = SelectPersona.Nombre;
                App.Current.Properties["Tel"] = SelectPersona.Telefono;
                App.Current.Properties["Direc"] = SelectPersona.Direccion;
                await App.Current.MainPage.Navigation.PushAsync(new UbicacionPage());
                //nos elimina la pagina anterior del estack de paginas
                var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    if (page.GetType() == typeof(UbicacionPage))
                        continue;
                    App.Current.MainPage.Navigation.RemovePage(page);
                }


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione el lugar de residencia de tu amigo-" +
                    "se mostrara en el mapa", "ok");
            }
            
        }
        //Método para editar o actualizar un registro de la lista
        public async void UpdatePersona()
        {
            if (SelectPersona != null)
            {
                App.Current.Properties["id"] = SelectPersona.ID;
                App.Current.Properties["nom"] = SelectPersona.Nombre;
                App.Current.Properties["Tel"] = SelectPersona.Telefono;
                App.Current.Properties["Direc"] = SelectPersona.Direccion;
                await App.Current.MainPage.Navigation.PushAsync(new UpdPersonaPage());


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Selecciona el " +
                    "amigo que desea editar", "ok");
            }
        }

        public ListPersonaViewModel()
        {
            FillList();
        }

        private async void FillList()
        {
            ListPersonas = new ObservableCollection<PersonaModel>();

            var MyList = await App.Database.GetPersonasAsync();
            foreach (var item in MyList)
            {
                ListPersonas.Add(new PersonaModel
                {
                    ID = item.ID,
                    Nombre = item.Nombre,
                    Telefono = item.Telefono,
                    Direccion = item.Direccion,
                });
            }
        }
    }
}
