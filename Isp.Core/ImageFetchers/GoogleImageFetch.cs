using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using Isp.Core.Configs;
using Isp.Core.Entities;
using Isp.Core.Exceptions;

namespace Isp.Core.ImageFetchers
{
    /// <summary>
    /// Google Custom Search
    /// https://developers.google.com/custom-search/docs/overview
    /// 
    /// Implementation of the image fetching via the Google's API using a client library
    /// created by Google: Google.Apis.Customsearch.v1.17.0.466
    /// 
    /// Free but with a daily limit of 100 transactions
    /// 
    /// Attention:
    /// - API allows up to 10 items per request, skipping starts from one
    /// - API allows filtering of the result object via Fields property
    /// - API returns its own benchmark
    /// - Custom Search Engine must be configured in the following way:
    /// --- "Sites to search" set to "Search the entire web but emphasise included sites"
    /// --- "Image search" set to "On"
    /// </summary>
    public class GoogleImageFetch : ImageFetchBase
    {
        private const string _name = "Google Custom Search";

        protected override async Task<ImageFetchResult> FetchImage(ImageFetchQuery model)
        {
            var service = new CustomsearchService(new BaseClientService.Initializer {ApiKey = AppSetting.GoogleApiKey});
            var request = service.Cse.List(model.Query);

            request.Cx = AppSetting.GoogleEngineId;
            request.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            request.Fields = "items(link,title),searchInformation";

            if (model.Skip.HasValue)
            {
                request.Start = Math.Max(model.Skip.Value, 1);
            }

            if (model.Take.HasValue)
            {
                request.Num = Math.Min(model.Take.Value, 10);
            }

            Search search;
            try
            {
                search = await request.ExecuteAsync();
            }
            catch (Exception ex)
            {
                throw new ImageFetchException($"Error when reading the response from the API: {ex.Message}", _name);
            }

            var result = new ImageFetchResult
            {
                ImageItems = search?.Items?.Select(i => new ImageItem
                {
                    Link = i.Link,
                    Title = i.Title
                }),
                TotalCount = search?.SearchInformation?.TotalResults ?? search?.Items?.Count ?? 0,
                Time = search?.SearchInformation?.SearchTime ?? default(double)
            };

            return result;
        }
    }
}