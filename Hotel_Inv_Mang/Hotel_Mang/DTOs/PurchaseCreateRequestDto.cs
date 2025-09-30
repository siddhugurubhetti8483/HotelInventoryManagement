namespace Hotel_Mang.DTOs
{
    public class PurchaseCreateRequestDto
    {
        public DateTime Date { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string VendorId { get; set; } = string.Empty;
    }
}
