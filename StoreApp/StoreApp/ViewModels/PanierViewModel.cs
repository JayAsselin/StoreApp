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
        private ObservableCollection<SmartDevice> _smartDevices;
        public ObservableCollection<SmartDevice> ListPanier { get => _smartDevices; set { _smartDevices = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public Command RemoveAllItemsFromCart { get; private set; }
        public Command GoToPaiement { get; private set; }
        private Command removeFromCart;
        public Command RemoveFromCart { get => removeFromCart; private set { removeFromCart = value; OnPropertyChanged(); } }

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
        }

        public void GetPanier()
        {
            this.ListPanier.Clear();
            var items = App.panier.GetContent();
            foreach (var item in items)
            {
                ListPanier.Add(item);
            }
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
                    App.panier.ClearPanier();
            }

        }
        public async void RemoveItem(object device)
        {
            var question = await Shell.Current.DisplayAlert("Attention", "Voulez vous vraiment enlever cette item du panier?", "Oui", "Non");
            if (question)
            {
                App.panier.RemoveProduct((device as SmartDevice).Id);
                GetPanier();
                OnPropertyChanged();
            }
        }
        public async void PaiementPage()
        {
            await Shell.Current.GoToAsync("PaiementPage");
        }
        private int getCount;
        public int GetCount { get => getCount; set { getCount = App.panier.CountPanier(); OnPropertyChanged(); } }
        public double GetPrice => App.panier.GetTotal();
    }
}
