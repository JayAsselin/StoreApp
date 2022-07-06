using StoreApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace StoreApp.ViewModels
{
    internal class PanierViewModel:INotifyPropertyChanged
    {
        private Panier panier;
        public ObservableCollection<Panier> Panier { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public PanierViewModel()
        {
            Panier = new ObservableCollection<Panier>();
            panier.GetContent();
        }

        //public void GetPanier()
        //{
        //    panier.GetContent();
        //}
    }
}
