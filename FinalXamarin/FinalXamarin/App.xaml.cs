using FinalXamarin.Repository;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalXamarin
{
    public partial class App : Application
    {
        static ApiRepository database;
        //Conexion a la base de datos
        public static ApiRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new ApiRepository(Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "Negocio.db5"));

                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
