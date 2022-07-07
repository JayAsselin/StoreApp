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
        public ObservableCollection<SmartDevice> ListPanier;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand RemoveFromCart { get; private set; }
        public ICommand GoToPaiement { get; private set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public PanierViewModel()
        {
            ListPanier = new ObservableCollection<SmartDevice>();
        }

        public void GetPanier()
        {
            var items = App.panier.GetContent();
            foreach (var item in items)
            {
                ListPanier.Add(item);
            }
        }

        public void RefreshList()
        {
            GetPanier();
            this.RemoveFromCart = new Command(EmptyCart);
            this.GoToPaiement = new Command(PaiementPage);
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
        public async void PaiementPage()
        {
            await Shell.Current.GoToAsync("PaiementPage");
        }
        private int getCount;
        public int GetCount { get => getCount; set { _ = App.panier.CountPanier(); OnPropertyChanged(); } }
        public double GetPrice => App.panier.GetTotal();
    }
}
