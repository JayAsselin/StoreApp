using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StoreApp.Models
{
    public class BuyerInfo
    {
        private string lName;
        private string fName;
        private string adress;
        private string phone;
        private string email;
        private string creditCard;

        public string LName { get => lName; set => lName = value; }
        public string FName { get => fName; set => fName = value; }
        public string Adress { get => adress; set => adress = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string CreditCard { get => creditCard; set => creditCard = value; }
    }
}
