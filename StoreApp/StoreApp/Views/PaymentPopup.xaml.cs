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
    public partial class PaymentPopup : Popup
    {
        public PaymentPopup()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e) => Dismiss(true);
    }
}