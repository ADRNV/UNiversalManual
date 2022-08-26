using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
