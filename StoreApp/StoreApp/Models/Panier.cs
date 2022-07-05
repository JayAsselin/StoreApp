using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Models
{
    public class Panier
    {
        private List<SmartDevice> content;
        public Panier()
        {
            this.content = new List<SmartDevice>();
        }
        public List<SmartDevice> GetContent() { return content; }
        public SmartDevice GetProductById(int id)
        {
            return this.content.Find(p => p.Id == id);
        }
        public void AddProduct(SmartDevice product)
        {
            this.content.Add(product);
        }
        public void RemoveProduct(int id)
        {
            SmartDevice product = this.content.Find(p => p.Id == id);
            this.content.Remove(product);
        }
        public void ClearPanier()
        {
            this.content.Clear();
        }
        public int CountPanier()
        {
            return this.content.Count;
        }
        public double GetTotal()
        {
            return this.content.Sum(p => p.Prix);
        }

    }
}
