using Newtonsoft.Json;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    //Jerome Asselin 2195077

    [QueryProperty(nameof(JsonList), "panier")]
    internal class PaiementViewModel:BaseViewModel
    {
        string jsonList;
        /// <summary>
        /// Recois la liste des smart device en format Json
        /// </summary>
        public string JsonList
        {
            get => jsonList;
            set
            {
                jsonList = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                GetJson(jsonList);
            }
        }

        /// <summary>
        /// Deserialise la liste de smart device et popule la liste accessible par la Vue
        /// </summary>
        /// <param name="list"></param>
        public void GetJson(string list)
        {
            try
            {
                JsonDevices = JsonConvert.DeserializeObject<List<SmartDevice>>(list);
                GetPrice = App.panier.GetTotal();
                OnPropertyChanged(nameof(GetPrice));
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }
        List<SmartDevice> jsonDevices;
        public List<SmartDevice> JsonDevices { get => jsonDevices; set { SetProperty(ref jsonDevices, value); } }

        public double GetPrice { get; set; }
        public Command PaiementCanceled { get; }
        public Command PaiementConfirmed { get; }
        public Invoice invoiceInfo;
        string nom;
        string prenom;
        string adresse;
        string telephone;
        string courriel;
        string carteCredit;
        public string Nom { get => nom; set { SetProperty(ref nom, value); } }
        public string Prenom { get => prenom; set { SetProperty(ref prenom, value); } }
        public string Adresse { get => adresse; set { SetProperty(ref adresse, value); } }
        public string Telephone { get => telephone; set { SetProperty(ref telephone, value); } }
        public string Courriel { get => courriel; set { SetProperty(ref courriel, value); } }
        public string CarteCredit { get => carteCredit; set { SetProperty(ref carteCredit, value); } }
        public PaiementViewModel()
        {
            try
            {
                // Instancie la liste et les commandes
                this.PaiementCanceled = new Command(ReturnToHomePage);
                this.PaiementConfirmed = new Command(ReturnToCart, ValidateFields);
                PropertyChanged += (_, __) => PaiementConfirmed.ChangeCanExecute();
                this.JsonDevices = new List<SmartDevice>();
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }

        /// <summary>
        /// Renvoie a la page d'Accueil si le bouton annuler est clicker
        /// </summary>
        private async void ReturnToHomePage()
        {
            try
            {
                var question = await Shell.Current.DisplayAlert("Attention", "Etes-vous sur de vouloir quitter la page?", "Oui", "Non");
                if (question)
                    await Shell.Current.GoToAsync("//accueil");
                    
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
                
        }

        /// <summary>
        /// Valide que les champs ne sont pas vide ainsi qu'un entrer specifique pour l'Adresse
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Prenom) 
                && !string.IsNullOrWhiteSpace(Courriel) && Regex.IsMatch(Adresse, @"^\d{1,}\s\w+\s?\-?\w+$") 
                && !string.IsNullOrWhiteSpace(Telephone) && !string.IsNullOrWhiteSpace(CarteCredit);
        }

        /// <summary>
        /// Affiche un popup pour dire que le paiement est bon et creer un objet Invoice qui est envoyer au DataProvider qui l'envoie a l'API qui l'insere a la base de donnee
        /// </summary>
        private async void ReturnToCart()
        {
            try
            {
                var result = await Shell.Current.ShowPopupAsync(new PaymentPopup());
                string listAchat = JsonConvert.SerializeObject(JsonDevices);

                if ((bool)result)
                {
                    invoiceInfo = new Invoice
                    {
                        Nom = Nom,
                        Prenom = Prenom,
                        Adresse = Adresse,
                        Telephone = Telephone,
                        Courriel = Courriel,
                        NumCarte = CarteCredit,
                        Montant = GetPrice,
                        CartList = listAchat
                    };
                    await App.dataProvider.AddInvoiceAsync(invoiceInfo);
                    App.panier.ClearPanier();
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }

    }
}
