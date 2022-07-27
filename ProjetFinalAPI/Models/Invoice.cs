namespace ProjetFinalAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string BuyerInfo { get; set; }
        public string CartContent { get; set; }
        public double InvoiceTotal { get; set; }
    }
}
