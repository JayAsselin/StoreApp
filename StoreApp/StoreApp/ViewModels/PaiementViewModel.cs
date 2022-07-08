using StoreApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    internal class PaiementViewModel:INotifyPropertyChanged
    {
        private string nom;
        private string prenom;
        private string adresse;
        private string telephone;
        private string courriel;
        private string carteCredit;
        public Command PaiementCanceled { get; }
        public Command PaiementConfirmed { get; }
        public string Nom { get => nom; set { nom = value; OnPropertyChanged(); } }
        public string Prenom { get => prenom; set { prenom = value; OnPropertyChanged(); } }
        public string Adresse { get => adresse; set { adresse = value; OnPropertyChanged(); } }
        public string Telephone { get => telephone; set { telephone = value; OnPropertyChanged(); } }
        public string Courriel { get => courriel; set { courriel = value; OnPropertyChanged(); } }
        public string CarteCredit { get => carteCredit; set { carteCredit = value; OnPropertyChanged(); } }

        public PaiementViewModel()
        {
            this.PaiementCanceled = new Command(ReturnToHomePage);
            this.PaiementConfirmed = new Command(ReturnToCart, ValidateFields);
            PropertyChanged += (_, __) => PaiementConfirmed.ChangeCanExecute();
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
            var question = await Shell.Current.DisplayAlert("Attention", "Etes-vous sur de vouloir le paiement?", "Oui", "Non");
            if (question)
                await Shell.Current.GoToAsync("..");
                
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Prenom) && !string.IsNullOrWhiteSpace(Courriel)
                && !string.IsNullOrWhiteSpace(Adresse) && !string.IsNullOrWhiteSpace(Telephone) && !string.IsNullOrWhiteSpace(CarteCredit);
        }

        private async void ReturnToCart()
        {
            await Shell.Current.DisplayAlert("Merci", "Merci d'avoir fait affaire avec nous!", "Ok");
            App.panier.ClearPanier();
            await Shell.Current.GoToAsync("..");
        }
    }
}
