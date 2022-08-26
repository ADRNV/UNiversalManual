using AutoMapper;

namespace UMan.DataAccess
{
    public class ArticleMapperProfile : Profile
    {
        public ArticleMapperProfile()
        {
            CreateMap<Entities.Article, Core.Article>()
                .ReverseMap();
        }
    }
}
