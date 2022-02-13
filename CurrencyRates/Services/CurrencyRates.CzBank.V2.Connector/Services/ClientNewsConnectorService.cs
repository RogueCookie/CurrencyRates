using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Xml;
using CurrencyRates.CzBank.V2.Connector.Models;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.CzBank.V2.Connector.Services
{
    public class ClientNewsConnectorService 
    {
        private readonly ILogger<ClientNewsConnectorService> _logger;

        public ClientNewsConnectorService(ILogger<ClientNewsConnectorService> logger)
        {
            _logger = logger;
        }

        public void DownloadRssNewsAsync(CancellationToken cancellationToken)
        {
            string url = "https://www.cnb.cz/en/.content/rss-feed/rss-feed_tz.xml";//TODO url binding
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();

            var news = feed.Items.Select(x => new RssModel()
            {
                Title = x.Title.Text,
                LinkForFullDescription = x.Id,
                PublishDate = x.PublishDate,
                ShortDescription = x.Summary.Text
            }).ToList();
        }
    }
}