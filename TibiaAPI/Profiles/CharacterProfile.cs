namespace TibiaAPI.Profiles
{
    public class CharacterProfile : AutoMapper.Profile
    {
        public CharacterProfile()
        {
            CreateMap<TibiaModels.BL.Character, Models.CharacterWithoutEquipmentDto>();
            CreateMap<TibiaModels.BL.Character, Models.CharacterDto>();
            CreateMap<Models.CharacterForCreationDto, TibiaModels.BL.Character>();
        }
    }
}
