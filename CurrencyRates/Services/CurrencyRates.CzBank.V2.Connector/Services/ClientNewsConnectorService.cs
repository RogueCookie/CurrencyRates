using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using CurrencyRates.CzBank.Grpc.Rss;
using CurrencyRates.CzBank.V2.Connector.Models;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.CzBank.V2.Connector.Services
{
    public class ClientNewsConnectorService : Grpc.Rss.RssService.RssServiceBase
    {
        public ClientNewsConnectorService()
        {
                
        }

        public override Task<GetInfoResponse> Download(GetInfoRequest request, ServerCallContext context)
        {
            string url = "https://www.cnb.cz/en/.content/rss-feed/rss-feed_tz.xml";//TODO url binding
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();

            var news = feed.Items.Select(x => new RssData()
            {
                Title = x.Title.Text,
                LinkForFullDescription = x.Id,
                PublishDate = Timestamp.FromDateTimeOffset(x.PublishDate),
                ShortDescription = x.Summary.Text
            });

            var response = new GetInfoResponse();
            response.Items.AddRange(news);

            return Task.FromResult(response);
        }
    }
}