using StoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanierPage : ContentPage
    {
        PanierViewModel viewModel;
        public PanierPage()
        {
            InitializeComponent();
            viewModel = new PanierViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = null;
            BindingContext = viewModel;
        }
    }
}