namespace TibiaAPI.Models
{
    public class ItemWithQuantityDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
    }
}
