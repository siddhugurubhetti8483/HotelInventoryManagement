namespace Hotel_Mang.DTOs
{
    public class VendorCreateRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public List<string> ItemsSupplied { get; set; } = new List<string>();
    }
}
