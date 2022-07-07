using StoreApp.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp
{
    public partial class App : Application
    {
        public static Panier panier;
        public static DbContext dbContext;
        public App()
        {
            InitializeComponent();
            panier = new Panier();
            MainPage = new AppShell();
            dbContext = new DbContext("StoreApp.db3");
            
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
