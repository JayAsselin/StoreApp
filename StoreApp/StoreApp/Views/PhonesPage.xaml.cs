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
    public partial class PhonesPage : ContentPage
    {
        PhonesViewModel viewModel;
        public PhonesPage()
        {
            InitializeComponent();
            viewModel = new PhonesViewModel();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.RefreshList();
            BindingContext = null;
            BindingContext = this.viewModel;
        }
    }
}