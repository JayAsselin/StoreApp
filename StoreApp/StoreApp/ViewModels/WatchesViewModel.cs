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
    internal class WatchesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<SmartDevice> Watches { get; set; }
        public int GetCount { get; set; }
        public Command AddToCart { get; private set; }
        public Command RemoveAllItemsFromCart { get; private set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void GetList()
        {
            try
            {
                this.Watches.Clear();
                var items = await App.dbContext.GetByTypeAsync("Smart Watch");
                foreach (var item in items)
                {
                    this.Watches.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void RefreshList()
        {
            GetList();
            GetCount = App.panier.CountPanier();
            OnPropertyChanged(nameof(GetCount));
        }

        public WatchesViewModel()
        {
            this.Watches = new ObservableCollection<SmartDevice>();
            this.AddToCart = new Command<SmartDevice>(OnTapped);
            this.RemoveAllItemsFromCart = new Command(EmptyCart);
        }

        private void OnTapped(SmartDevice item)
        {
            if (item == null)
                return;

            App.panier.AddProduct(item);
            GetCount = App.panier.CountPanier();
            OnPropertyChanged(nameof(GetCount));
        }
        public async void EmptyCart() 
        {
            if (App.panier.CountPanier() == 0)
                await Shell.Current.DisplayAlert("Info", "Le panier est vide", "Ok");
            else
            {
                var question = await Shell.Current.DisplayAlert("Attention", "Voulez vous vraiment vider le panier?", "Oui", "Non");
                if(question)
                {
                    App.panier.ClearPanier();
                    GetCount = App.panier.CountPanier();
                    OnPropertyChanged(nameof(GetCount));
                } 
            }
            
        }
    }
}
