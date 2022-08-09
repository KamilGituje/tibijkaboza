namespace TibiaAPI.Profiles
{
    public class ItemProfile : AutoMapper.Profile
    {
        public ItemProfile()
        {
            CreateMap<TibiaModels.BL.Item, Models.ItemDto>();
            CreateMap<TibiaModels.BL.Item, Models.ItemWithQuantityDto>();
        }
    }
}
