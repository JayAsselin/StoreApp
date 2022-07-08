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
    public partial class PaiementPage : ContentPage
    {
        PaiementViewModel viewModel;
        public PaiementPage()
        {
            InitializeComponent();
            viewModel = new PaiementViewModel();
            BindingContext = viewModel;
        }
    }
}