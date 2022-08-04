using Newtonsoft.Json;
using StoreApp.Models;
using StoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoicePopup : Popup
    {
        public Invoice invoice { get; set; }
        public List<SmartDevice> ListItem { get; set; }
        public InvoicePopup(Invoice item)
        {
            InitializeComponent();
            // Assigne l'objet invoice au parametre recu par le constructeur
            invoice = item;
            // Deserialise la liste des achats dans une liste a afficher sur la facture
            this.ListItem = new List<SmartDevice>(JsonConvert.DeserializeObject<List<SmartDevice>>(invoice.CartList));
            this.BindingContext = this;
        }

        // Event pour fermer le popup quand on click sur le bouton
        private void Button_Clicked(object sender, EventArgs e) => Dismiss(null);
    }
}