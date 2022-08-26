using AutoMapper;

namespace UMan.DataAccess
{
    public class PaperMapperProfile : Profile
    {
        public PaperMapperProfile()
        {
            CreateMap<Entities.Paper, Core.Paper>()
                .ReverseMap();
        }

    }
}
