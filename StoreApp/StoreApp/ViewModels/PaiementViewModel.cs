using Newtonsoft.Json;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    [QueryProperty(nameof(JsonList), "panier")]
    internal class PaiementViewModel:BaseViewModel
    {
        string jsonList;
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
        public PersonInfo personInfo;
        //public List<string> Invoice = new List<string>();
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

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Prenom) 
                && !string.IsNullOrWhiteSpace(Courriel) && !string.IsNullOrWhiteSpace(Adresse) 
                && !string.IsNullOrWhiteSpace(Telephone) && !string.IsNullOrWhiteSpace(CarteCredit);
        }

        private async void ReturnToCart()
        {
            try
            {
                await Shell.Current.DisplayAlert("Merci", "Merci d'avoir fait affaire avec nous!", "Ok");
                personInfo = new PersonInfo
                {
                    LName = Nom,
                    FName = Prenom,
                    Adress = Adresse,
                    Phone = Telephone,
                    Email = Courriel,
                    CreditCard = CarteCredit
                };
                string buyerInfo = JsonConvert.SerializeObject(personInfo, Formatting.Indented);
                string listAchat = JsonConvert.SerializeObject(JsonList);
                //Invoice.Add(buyerInfo);
                //Invoice.Add(listAchat);
                //string newInvoice = JsonConvert.SerializeObject(Invoice);
                invoiceInfo = new Invoice
                {
                    BuyerInfo = buyerInfo,
                    CartContent = listAchat,
                    InvoiceTotal = GetPrice
                };
                await App.dbContext.InsertInvoiceAsync(invoiceInfo);
                App.panier.ClearPanier();
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }

    }
}
