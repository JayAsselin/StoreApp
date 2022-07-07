using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace StoreApp.Data
{
    public class Panier:INotifyPropertyChanged
    {
        private List<SmartDevice> content;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Panier()
        {
            content = new List<SmartDevice>();
        }
        public List<SmartDevice> GetContent() { return content; }
        public SmartDevice GetProductById(int id)
        {
            return content.Find(p => p.Id == id);
        }
        public void AddProduct(SmartDevice product)
        {
            content.Add(product);
        }
        public void RemoveProduct(int id)
        {
            SmartDevice product = content.Find(p => p.Id == id);
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
