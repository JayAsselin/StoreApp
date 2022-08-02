using Newtonsoft.Json;
using StoreApp.Models;
using StoreApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    internal class FactureViewModel
    {
        public ObservableCollection<Invoice> Invoices { get; set; }
        public Command ViewInvoiceCommand { get; private set; }
        public FactureViewModel()
        {
            this.Invoices = new ObservableCollection<Invoice>();
            this.ViewInvoiceCommand = new Command<Invoice>(ViewInvoice);
        }

        private async void GetInvoices()
        {
            try
            {
                this.Invoices.Clear();
                var items = await App.dataProvider.GetAllInvoicesAsync();
                foreach (var item in items)
                {
                    this.Invoices.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }

        public void RefreshList()
        {
            try
            {
                GetInvoices();
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }

        }

        private async void ViewInvoice(Invoice item)
        {
            try
            {
                if (item == null)
                    return;

                await Shell.Current.ShowPopupAsync(new InvoicePopup(item));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }
    }
}
