using AutoMapper;
using FeedParser.Core;
using FeedParser.Core.Models;
using UMan.Core;
using UMan.Core.Repositories;

namespace UMan.DataAccess.Parsing.Handlers
{
    public class UpdateHandler : IUpdateHandler<IEnumerable<FeedParser.Core.Models.Article>>
    {
        private readonly IRepository<Core.Paper> _papersRepository;

        private readonly IMapper _mapper;

        public UpdateHandler(IRepository<Core.Paper> papersRepository, IMapper mapper)
        {
            _papersRepository = papersRepository;

            _mapper = mapper;
        }

        public Task OnUpdate(IEnumerable<FeedParser.Core.Models.Article> update)
        {
            throw new NotImplementedException();
        }
    }
}
