namespace TibiaAPI.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<Models.UserForLoginAndCreationDto, TibiaModels.BL.User>();
            CreateMap<TibiaModels.BL.User, Models.UserForLoginAndCreationDto>();
        }
    }
}
