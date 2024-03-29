﻿using AutoMapper;

namespace UMan.DataAccess
{
    public class AuthorMapperProfile : Profile
    {
        public AuthorMapperProfile()
        {
            CreateMap<Entities.Author, Core.Author>().ForMember(a => a.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Core.Author, Entities.Author>().ForMember(a => a.Id, opt => opt.Ignore());
        }
    }
}
