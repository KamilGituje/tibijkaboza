namespace TibiaAPI.Profiles
{
    public class EquipmentProfile : AutoMapper.Profile
    {
        public EquipmentProfile()
        {
            CreateMap<TibiaModels.BL.Equipment, Models.EquipmentDto>();
        }
    }
}
