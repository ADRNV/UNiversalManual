using AutoMapper;
using FeedParser.Core;
using FeedParser.Core.Models;
using FeedParser.Parsers.Updates.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UMan.Core;
using UMan.Core.Repositories;
using CoreArticle = UMan.Core.Article;

namespace UMan.DataAccess.Parsing.Handlers
{
    public class UpdateHandler : UpdateHandlerBase<IEnumerable<FeedParser.Core.Models.Article>>
    {
        private readonly IPapersRepository _papersRepository;

        private readonly ILogger _logger;

        public UpdateHandler(IPapersRepository papersRepository, ILogger<UpdateHandler> logger)
        {
            _papersRepository = papersRepository;

            _logger = logger;
        }

        public override async Task OnUpdate(IEnumerable<FeedParser.Core.Models.Article> update)
        {
            var paper = new Paper() { Author = new Author() { Name = "System", Email = "System@gmail.com" } };

            foreach(var article in update)
            {
                var papaerPart = new CoreArticle() 
                { 
                    Title = article.Header,
                    Content = String.Join(" ", article.Content.ToArray())
                };
                _logger.LogWarning($"{article.Header} from {article.Link}");
                paper.Articles.Add(papaerPart);
            }

            Task.WaitAll(_papersRepository.Add(paper));

            await _papersRepository.Save();
        }
    }
}
