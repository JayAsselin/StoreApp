using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    internal class TabletsViewModel:BaseViewModel
    {
        public ObservableCollection<SmartDevice> Tablets { get; set; }
        public int GetCount { get; set; }
        public Command AddToCart { get; private set; }
        public Command RemoveAllItemsFromCart { get; private set; }

        private async void GetList()
        {
            try
            {
                this.Tablets.Clear();
                var items = await App.dbContext.GetByTypeAsync("Tablet");
                foreach (var item in items)
                {
                    this.Tablets.Add(item);
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
                GetList();
                GetCount = App.panier.CountPanier();
                OnPropertyChanged(nameof(GetCount));
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }

        public TabletsViewModel()
        {
            try
            {
                this.Tablets = new ObservableCollection<SmartDevice>();
                this.AddToCart = new Command<SmartDevice>(OnTapped);
                this.RemoveAllItemsFromCart = new Command(EmptyCart);
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }

        private void OnTapped(SmartDevice item)
        {
            try
            {
                if (item == null)
                    return;

                App.panier.AddProduct(item);
                GetCount = App.panier.CountPanier();
                OnPropertyChanged(nameof(GetCount));
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }
        public async void EmptyCart()
        {
            try
            {
                if (App.panier.CountPanier() == 0)
                    await Shell.Current.DisplayAlert("Info", "Le panier est vide", "Ok");
                else
                {
                    var question = await Shell.Current.DisplayAlert("Attention", "Voulez vous vraiment vider le panier?", "Oui", "Non");
                    if (question)
                    {
                        App.panier.ClearPanier();
                        GetCount = App.panier.CountPanier();
                        OnPropertyChanged(nameof(GetCount));
                    }
                }
            }
            catch (Exception ex)
            {
               await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            

        }
    }
}
