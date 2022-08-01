using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public string NumCarte { get; set; }
        //public string CartContent { get; set; }
        public double InvoiceTotal { get; set; }
    }
}
