namespace SamINV.Models
{
    public class Invoice
    {
        public long Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public string Contact { get; set; } = "";
        public decimal? Amount { get; set; }
        public bool Draft { get; set; }
    }
}