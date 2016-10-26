using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Isp.Core.Entities;

namespace Isp.Core.ImageFetchers
{
    /// <summary>
    /// Google Custom Search
    /// 
    /// Implementation of the image fetching via the Google's API using a client library
    /// created by Google: Google.Apis.Customsearch.v1.17.0.466
    /// 
    /// Attention:
    /// - API allows up to 10 items per request, skipping starts from one
    /// - Custom Search Engine must be configured in the following way:
    /// --- "Sites to search" set to "Search the entire web but emphasise included sites"
    /// --- "Image search" set to "On"
    /// </summary>
    public class GoogleImageFetch : ImageFetchBase
    {
        private readonly string _apiKey = ConfigurationManager.AppSettings["GoogleCustomSearchApiKey"];
        private readonly string _engineId = ConfigurationManager.AppSettings["GoogleCustomSearchEngineId"];

        protected override async Task<ImageFetchResult> FetchImage(ImageFetchQuery model)
        {
            var service = new CustomsearchService(new BaseClientService.Initializer {ApiKey = _apiKey});
            var request = service.Cse.List(model.Query);

            request.Cx = _engineId;
            request.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            
            if (model.Skip.HasValue)
            {
                request.Start = Math.Min(model.Skip.Value, 1);
            }

            if (model.Take.HasValue)
            {
                request.Num = Math.Max(model.Take.Value, 10);
            }

            var search = await request.ExecuteAsync();
            if (search?.Items == null)
            {
                throw new Exception($"Query '{model.Query}' returned no results");
            }

            var result = new ImageFetchResult
            {
                ImageItems = search.Items.Select(i => new ImageItem
                {
                    Link = i.Link,
                    Title = i.Title
                }),
                TotalCount = search.SearchInformation.TotalResults ?? search.Items.Count
            };

            return result;
        }
    }
}