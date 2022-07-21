using Newtonsoft.Json;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    internal class FactureViewModel
    {
        ObservableCollection<Invoice> invoices;
        public ObservableCollection<Invoice> Invoices { get; set; }
        public ObservableCollection<PersonInfo> Persons { get; set; }
        public ObservableCollection<SmartDevice> smartDevices { get; set; }
        public FactureViewModel()
        {
            this.Invoices = new ObservableCollection<Invoice>();
        }

        private async void GetInvoices()
        {
            try
            {
                this.Invoices.Clear();
                var items = await App.dbContext.GetAllInvoicesAsync();
                foreach (var item in items)
                {
                    var buyerInfo = JsonConvert.DeserializeObject<PersonInfo>(item.BuyerInfo);
                    var cartContent = JsonConvert.DeserializeObject<List<SmartDevice>>(item.CartContent);
                    var invoiceTotal = item.InvoiceTotal;
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
    }
}
