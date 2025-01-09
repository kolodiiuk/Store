namespace Laundry.Domain.Reports
{
    public class ChequeReport
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Subtotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
        public List<ChequeReportItem> Items { get; set; }
    }

    public class ChequeReportItem
    {
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}