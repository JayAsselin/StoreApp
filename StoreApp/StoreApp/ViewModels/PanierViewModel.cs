using StoreApp.Data;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    internal class PanierViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<SmartDevice> _listPanier;
        public ObservableCollection<SmartDevice> ListPanier { get => _listPanier; set { _listPanier = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public int GetCount { get; set; }
        public double GetPrice { get; set; }
        public Command RemoveAllItemsFromCart { get; private set; }
        public Command GoToPaiement { get; private set; }
        public Command RemoveFromCart { get; private set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public PanierViewModel()
        {
            ListPanier = new ObservableCollection<SmartDevice>();
            this.RemoveAllItemsFromCart = new Command(EmptyCart);
            this.GoToPaiement = new Command(PaiementPage);
            this.RemoveFromCart = new Command(RemoveItem);
            PropertyChanged += (_, __) => RemoveFromCart.ChangeCanExecute();
            PropertyChanged += (_, __) => GoToPaiement.ChangeCanExecute();
            PropertyChanged += (_, __) => RemoveAllItemsFromCart.ChangeCanExecute();
        }

        public void GetPanier()
        {
            this.ListPanier.Clear();
            var items = App.panier.GetContent();
            foreach (var item in items)
            {
                ListPanier.Add(item);
            }
            GetPrice = App.panier.GetTotal();
            OnPropertyChanged(nameof(GetPrice));
            GetCount = App.panier.CountPanier();
            OnPropertyChanged(nameof(GetCount));
        }

        public void RefreshList()
        {
            GetPanier();
        }
        public async void EmptyCart()
        {
            if (App.panier.CountPanier() == 0)
                await Shell.Current.DisplayAlert("Info", "Le panier est vide", "Ok");
            else
            {
                var question = await Shell.Current.DisplayAlert("Attention", "Voulez vous vraiment vider le panier?", "Oui", "Non");
                if (question)
                {
                    App.panier.ClearPanier();
                    GetPanier();
                }
            }

        }
        public async void RemoveItem(object device)
        {
            var question = await Shell.Current.DisplayAlert("Attention", "Voulez vous vraiment enlever cette item du panier?", "Oui", "Non");
            if (question)
            {
                App.panier.RemoveProduct((device as SmartDevice).Id);
                GetPanier();
            }
        }
        public async void PaiementPage()
        {
            if(App.panier.CountPanier() > 0)
                await Shell.Current.GoToAsync("PaiementPage");
        }
        
    }
}
