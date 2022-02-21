using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using CurrencyRates.CzBank.Grpc.Rss;
using CurrencyRates.CzBank.V2.Connector.Constants;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Configuration;

namespace CurrencyRates.CzBank.V2.Connector.Services
{
    public class ClientNewsConnectorService : Grpc.Rss.RssService.RssServiceBase
    {
        private readonly HttpClient _httpClient;

        public ClientNewsConnectorService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient(GeneralConstants.BaseCzBankUri);
        }

        public override async Task<GetInfoResponse> Download(GetInfoRequest request, ServerCallContext context)
        {
            using var streamResponse = await _httpClient.GetStreamAsync(GeneralConstants.AdditionalUrlNews, context.CancellationToken);
            
            using var reader = XmlReader.Create(streamResponse);
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

            return response;
        }



        //    public override Task<GetInfoResponse> Download(GetInfoRequest request, ServerCallContext context)
        //    {
        //        using var reader = XmlReader.Create(_newsAddress);
        //        var feed = SyndicationFeed.Load(reader);
        //        reader.Close();

        //        var news = feed.Items.Select(x => new RssData()
        //        {
        //            Title = x.Title.Text,
        //            LinkForFullDescription = x.Id,
        //            PublishDate = Timestamp.FromDateTimeOffset(x.PublishDate),
        //            ShortDescription = x.Summary.Text
        //        });

        //        var response = new GetInfoResponse();
        //        response.Items.AddRange(news);

        //        return Task.FromResult(response);
        //    }
        //}
    }
}