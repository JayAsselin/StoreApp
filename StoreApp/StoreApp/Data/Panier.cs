using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace StoreApp.Data
{
    //Jerome Asselin 2195077
    public class Panier:INotifyPropertyChanged
    {
        private ObservableCollection<SmartDevice> content;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Panier()
        {
            content = new ObservableCollection<SmartDevice>();
        }
        public ObservableCollection<SmartDevice> GetContent() { return content; }
        public SmartDevice GetProductById(int id)
        {
            return content.FirstOrDefault(p => p.Id == id);
        }
        public void AddProduct(SmartDevice product)
        {
            content.Add(product);
        }
        public void RemoveProduct(int id)
        {
            SmartDevice product = content.FirstOrDefault(p => p.Id == id);
            content.Remove(product);
        }
        public void ClearPanier()
        {
            content.Clear();
        }
        public int CountPanier()
        {
            return content.Count;
        }
        public double GetTotal()
        {
            return content.Sum(p => p.Prix);
        }

    }
}
