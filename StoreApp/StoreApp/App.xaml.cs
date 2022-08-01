using StoreApp.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp
{
    public partial class App : Application
    {
        public static Panier panier;
        //public static DbContext dbContext;
        public static DataProviderService dataProvider;
        public App()
        {
            // License key for syncfusion badge package
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjcyMzg5QDMyMzAyZTMyMmUzMFdPcVFaMjB1dmozejJERXcyRS8ydE5wQ1h5ZXV2S2lyQm1scStGQmR1WlU9");

            InitializeComponent();
            panier = new Panier();
            MainPage = new AppShell();
            dataProvider = new DataProviderService();
            //dbContext = new DbContext("StoreApp.db3");
            
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
