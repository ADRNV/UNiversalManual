using AutoMapper;

namespace UMan.DataAccess
{
    public class PaperMapperProfile : Profile
    {
        public PaperMapperProfile()
        {
            CreateMap<Entities.Paper, Core.Paper>()
                .ForMember(p => p.Articles, opt => opt.MapFrom(src => src.Articles))
                .ForMember(p => p.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(p => p.HashTags, opt => opt.MapFrom(src => src.HashTags));

            CreateMap<Core.Paper, Entities.Paper>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Articles, opt => opt.MapFrom(src => src.Articles))
                .ForMember(p => p.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(p => p.HashTags, opt => opt.MapFrom(src => src.HashTags));
        }

    }
}
