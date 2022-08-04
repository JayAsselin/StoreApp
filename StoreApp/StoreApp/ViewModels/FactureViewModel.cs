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
    //Jerome Asselin 2195077
    internal class FactureViewModel
    {
        public ObservableCollection<Invoice> Invoices { get; set; }
        public ObservableCollection<SmartDevice> List { get; set; }
        public Command ViewInvoiceCommand { get; private set; }
        public FactureViewModel()
        {
            // Instanciation de la liste et commande
            this.Invoices = new ObservableCollection<Invoice>();
            this.ViewInvoiceCommand = new Command<object>(ViewInvoice);
        }

        /// <summary>
        /// Methode priver pour populer la liste des factures a partir du Data Provider
        /// </summary>
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

        /// <summary>
        /// Methode public pour rafraichir la liste quand on navigue sur la page Facture
        /// </summary>
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

        /// <summary>
        /// Methode qui recherche la facture selectionner par son Id et l'envoi dans la page InvoicePopup
        /// </summary>
        /// <param name="item"></param>
        private async void ViewInvoice(object item)
        {
            try
            {
                if (item == null)
                    return;

                Invoice invoice = new Invoice();
                invoice = await App.dataProvider.GetInvoiceByIdAsync((int)item);
                await Shell.Current.ShowPopupAsync(new InvoicePopup(invoice));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }
    }
}
