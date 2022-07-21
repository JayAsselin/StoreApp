using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Models
{
    public class Invoice
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BuyerInfo { get; set; }
        public string CartContent { get; set; }
        public double InvoiceTotal { get; set; }
    }
}
