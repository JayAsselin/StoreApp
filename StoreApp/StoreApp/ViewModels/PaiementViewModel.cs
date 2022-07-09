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
    internal class PaiementViewModel:INotifyPropertyChanged
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
        public List<SmartDevice> JsonDevices { get => jsonDevices; set { jsonDevices = value; OnPropertyChanged(); } }

        public double GetPrice { get; set; }
        public Command PaiementCanceled { get; }
        public Command PaiementConfirmed { get; }
        //public Invoice invoice;
        //public PersonInfo personInfo;
        //public Dictionary<PersonInfo, SmartDevice> InvoiceList = new Dictionary<PersonInfo, SmartDevice>();
        string nom;
        string prenom;
        string adresse;
        string telephone;
        string courriel;
        string carteCredit;
        public string Nom { get => nom; set { nom = value; OnPropertyChanged(); } }
        public string Prenom { get => prenom; set { prenom = value; OnPropertyChanged(); } }
        public string Adresse { get => adresse; set { adresse = value; OnPropertyChanged(); } }
        public string Telephone { get => telephone; set { telephone = value; OnPropertyChanged(); } }
        public string Courriel { get => courriel; set { courriel = value; OnPropertyChanged(); } }
        public string CarteCredit { get => carteCredit; set { carteCredit = value; OnPropertyChanged(); } }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private async void ReturnToHomePage()
        {
            try
            {
                var question = await Shell.Current.DisplayAlert("Attention", "Etes-vous sur de vouloir le paiement?", "Oui", "Non");
                if (question)
                    await Shell.Current.GoToAsync("..");
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
                App.panier.ClearPanier();
                //personInfo = new PersonInfo
                //{
                //    LName = Nom,
                //    FName = Prenom,
                //    Adress = Adresse,
                //    Phone = Telephone,
                //    Email = Courriel,
                //    CreditCard = CarteCredit
                //};

                //InvoiceList.Add(personInfo, panier.ListPanier);
                //invoice = new Invoice
                //{
                //    InvoiceContent = 
                //}
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
            
        }

    }
}
