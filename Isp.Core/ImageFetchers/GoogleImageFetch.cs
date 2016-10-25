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
            request.Start = model.Skip;
            request.Num = model.Take;

            var search = await request.ExecuteAsync();

            if (search?.Items == null)
            {
                throw new Exception($"Query '{model.Query}' returned no results");
            }

            var result = new ImageFetchResult
            {

                TotalCount = search.SearchInformation.TotalResults ?? search.Items.Count
            };

            return result;
        }
    }
}