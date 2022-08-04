using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    //Jerome Asselin 2195077
    public class ItemsListViewModel : BaseViewModel
    {
        public ObservableCollection<SmartDevice> Phones { get; set; }
        public ObservableCollection<SmartDevice> Tablets { get; set; }
        public ObservableCollection<SmartDevice> Watches { get; set; }
        public ICommand ExpandCommand { get; private set; }
        public bool IsExpanded { get; set; }
        public int GetCount { get; set; }
        public Command AddToCart { get; private set; }
        public Command RemoveAllItemsFromCart { get; private set; }

        public ItemsListViewModel()
        {
            try
            {
                // Instanciation des listes et commandes
                this.Phones = new ObservableCollection<SmartDevice>();
                this.Tablets = new ObservableCollection<SmartDevice>();
                this.Watches = new ObservableCollection<SmartDevice>();
                this.AddToCart = new Command<object>(OnTapped);
                this.RemoveAllItemsFromCart = new Command(EmptyCart);

                // Indique au expander qu'il est fermer par defaut
                this.IsExpanded = false;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }

        /// <summary>
        /// Popule la liste en appelant le dataProvider qui appele lui meme l'api en specifiant le type
        /// </summary>
        private async void GetPhonesList()
        {
            try
            {
                this.Phones.Clear();
                var items = await App.dataProvider.GetAllDevicesAsync();
                foreach (var item in items)
                {
                    if (item.Type == "Smart phone")
                        this.Phones.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }

        /// <summary>
        /// Popule la liste en appelant le dataProvider qui appele lui meme l'api en specifiant le type
        /// </summary>
        private async void GetTabletsList()
        {
            try
            {
                this.Tablets.Clear();
                var items = await App.dataProvider.GetAllDevicesAsync();
                foreach (var item in items)
                {
                    if (item.Type == "Tablet")
                        this.Tablets.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }

        /// <summary>
        /// Popule la liste en appelant le dataProvider qui appele lui meme l'api en specifiant le type
        /// </summary>
        private async void GetWatchesList()
        {
            try
            {
                this.Watches.Clear();
                var items = await App.dataProvider.GetAllDevicesAsync();
                foreach (var item in items)
                {
                    if (item.Type == "Smart watch")
                        this.Watches.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }

        /// <summary>
        /// Rafraichit les listes dans la vue ainsi que la quantite d'item au panier
        /// </summary>
        public void RefreshList()
        {
            try
            {
                GetPhonesList();
                GetTabletsList();
                GetWatchesList();
                GetCount = App.panier.CountPanier();
                OnPropertyChanged(nameof(GetCount));
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }

        }

        /// <summary>
        /// Envoi le smart device par sont Id au panier
        /// </summary>
        /// <param name="item"></param>
        private async void OnTapped(object item)
        {
            try
            {
                if (item == null)
                    return;

                SmartDevice device = new SmartDevice();
                device = await App.dataProvider.GetDeviceByIdAsync((int)item);
                App.panier.AddProduct(device);
                GetCount = App.panier.CountPanier();
                OnPropertyChanged(nameof(GetCount));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }

        }

        /// <summary>
        /// Vide le panier si il contient des items sinon affiche un message
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
