using Newtonsoft.Json;
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
    //Jerome Asselin 2195077
    internal class PanierViewModel:BaseViewModel
    {
        private ObservableCollection<SmartDevice> _listPanier;
        public ObservableCollection<SmartDevice> ListPanier { get => _listPanier; set { SetProperty(ref _listPanier, value); } }

        public int GetCount { get; set; }
        public double GetPrice { get; set; }
        public Command RemoveAllItemsFromCart { get; private set; }
        public Command GoToPaiement { get; private set; }
        public Command RemoveFromCart { get; private set; }

        public PanierViewModel()
        {
            try
            {
                // Instancie la liste et les commandes
                ListPanier = new ObservableCollection<SmartDevice>();
                this.RemoveAllItemsFromCart = new Command(EmptyCart);
                this.GoToPaiement = new Command(PaiementPage);
                this.RemoveFromCart = new Command(RemoveItem);
                PropertyChanged += (_, __) => RemoveFromCart.ChangeCanExecute();
                PropertyChanged += (_, __) => GoToPaiement.ChangeCanExecute();
                PropertyChanged += (_, __) => RemoveAllItemsFromCart.ChangeCanExecute();
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }

        /// <summary>
        /// Ajoute les items a la liste pour les afficher et affiche le montant du panier et le nombre d'item
        /// </summary>
        public void GetPanier()
        {
            try
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
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }

        /// <summary>
        /// Rafraichit la liste
        /// </summary>
        public void RefreshList()
        {
            GetPanier();
        }

        /// <summary>
        /// Vide le panier au complet si il n'est pas vide sinon affiche un message
        /// </summary>
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
                        GetPanier();
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            

        }

        /// <summary>
        /// Permet d'enlever juste un des items de la liste en clickant 2 fois dessus
        /// </summary>
        /// <param name="device"></param>
        public async void RemoveItem(object device)
        {
            try
            {
                var question = await Shell.Current.DisplayAlert("Attention", "Voulez vous vraiment enlever cette item du panier?", "Oui", "Non");
                if (question)
                {
                    App.panier.RemoveProduct((device as SmartDevice).Id);
                    GetPanier();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }

        }

        /// <summary>
        /// Serialise la liste du panier et l'envoie as la page de Paiement et l'affiche
        /// </summary>
        public async void PaiementPage()
        {
            try
            {
                if (App.panier.CountPanier() > 0)
                {
                    string jsonPanier = JsonConvert.SerializeObject(ListPanier);
                    await Shell.Current.GoToAsync($"{nameof(PaiementPage)}?panier={jsonPanier}");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }

        }
        
    }
}
