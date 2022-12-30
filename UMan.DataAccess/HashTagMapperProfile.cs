using AutoMapper;

namespace UMan.DataAccess
{
    public class HashTagMapperProfile : Profile
    {
        public HashTagMapperProfile()
        {
            CreateMap<Core.HashTag, Entities.HashTag>()
                .ForMember(h => h.Papers, opt => opt.Ignore());
        }
    }
}
