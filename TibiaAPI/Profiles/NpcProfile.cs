namespace TibiaAPI.Profiles
{
    public class NpcProfile : AutoMapper.Profile
    {
        public NpcProfile()
        {
            CreateMap<TibiaModels.BL.Npc, Models.NpcWithItemsDto>();
            CreateMap<TibiaModels.BL.Npc, Models.NpcDto>();
        }
    }
}
