using AngleSharp;
using AngleSharp.Html.Dom;
using CodeHollow.FeedReader;
using UMan.Core;

namespace UMan.Services.Helpers
{
    public class HabrParserHelper
    {
        public const string HabrUrl = "https://habr.com/ru/rss/all/all/";

        private IConfiguration _config = Configuration.Default.WithDefaultLoader();

        private IDictionary<string, string> _selectorsConfig;

        public HabrParserHelper(IDictionary<string, string> selectorsConfig)
        {
            _selectorsConfig = selectorsConfig;
        }

        public async Task<IEnumerable<string>> GetSiteFeedUrls()
        {
            Feed feed = await FeedReader.ReadAsync(HabrUrl);

            var postsUrls = new List<string>();

            feed.Items.ToList().ForEach(i =>
            {
                postsUrls.Add(i.Link);
            });

            return postsUrls.AsEnumerable();
        }

        private async Task<Author> GetAuthor(string paperUrl)
        {
            var context = BrowsingContext.New(_config);

            using var document = await context.OpenAsync(paperUrl);

            var authorData = document.QuerySelector(PaperMetaDataAuthor) as IHtmlAnchorElement;

            if (authorData is not null)
            {
                return new Author()
                {
                    Name = authorData?.TextContent?.Trim(),
                    Email = "undefined@gmail.com"
                    //Papers = GetPapers()
                };
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        //TODO Rewrite to IConfiguration or IDictionary        
        public const string PaperMetadaRoot = "main.tm-layout__container div.tm-page__wrapper div.tm-page__main div.tm-article-presenter__header div.tm-article-presenter__snippet div.tm-article-snippet__meta";

        public const string PaperMetaDataAuthor = PaperMetadaRoot + "span.tm-article-snippet__author a.tm-user-info__username";
    }
}
