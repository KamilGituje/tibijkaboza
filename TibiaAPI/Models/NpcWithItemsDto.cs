using TibiaModels.BL;

namespace TibiaAPI.Models
{
    public class NpcWithItemsDto
    {
        public int NpcId { get; set; }
        public string Name { get; set; }
        public List<ItemWithPriceDto> Items { get; set; }
    }
}
