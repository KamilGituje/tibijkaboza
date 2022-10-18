namespace TibiaAPI.Profiles
{
    public class MonsterProfile : AutoMapper.Profile
    {
        public MonsterProfile()
        {
            CreateMap<TibiaModels.BL.Monster, Models.MonsterDto>();
        }
    }
}
