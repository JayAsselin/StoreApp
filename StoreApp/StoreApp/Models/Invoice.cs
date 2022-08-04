using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Models
{
    //Jerome Asselin 2195077
    public class Invoice
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public string NumCarte { get; set; }
        public double Montant { get; set; }
        public string CartList { get; set; }
    }
}
