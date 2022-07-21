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
    public partial class FacturePage : ContentPage
    {
        FactureViewModel viewModel;
        public FacturePage()
        {
            InitializeComponent();
            this.viewModel = new FactureViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = null;
            this.BindingContext = viewModel;
            viewModel.RefreshList();
        }
    }
}