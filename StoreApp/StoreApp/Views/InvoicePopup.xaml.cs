using StoreApp.Models;
using StoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoicePopup : Popup
    {
        public Invoice invoice { get; set; }
        public InvoicePopup(Invoice item)
        {
            InitializeComponent();
            invoice = item;
            this.BindingContext = invoice;
            LoadInvoiceById(item.Id);
        }

        private async void LoadInvoiceById(int id)
        {
            try
            {
                var item = await App.dataProvider.GetInvoiceByIdAsync(id);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }

        private void Button_Clicked(object sender, EventArgs e) => Dismiss(null);
    }
}