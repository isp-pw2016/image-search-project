using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Isp.Core.Entities;
using Isp.Core.Entities.Jsons.Bing;
using Isp.Core.Exceptions;
using Newtonsoft.Json;

namespace Isp.Core.ImageFetchers
{
    /// <summary>
    /// Bing Image Search v5
    /// https://microsoft.com/cognitive-services/en-us/bing-image-search-api
    /// 
    /// Implementation of the image fetching via the Bing's API using web requests
    /// 
    /// Free but with a monthly limit of 1000 transactions
    /// API keys may expire if not used for >= 90 days
    /// 
    /// Attention:
    /// - API allows up to 150 items per request, skipping starts from zero
    /// </summary>
    public class BingImageFetch : ImageFetchBase
    {
        private const string _name = "Bing Image Search";
        private readonly string _apiUrl = ConfigurationManager.AppSettings["BingSearchApiUrl"];
        private readonly string _apiKey = ConfigurationManager.AppSettings["BingSearchApiKey"];

        protected override async Task<ImageFetchResult> FetchImage(ImageFetchQuery model)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

            var requestParams = HttpUtility.ParseQueryString(string.Empty);
            requestParams["q"] = model.Query;

            if (model.Take.HasValue)
            {
                requestParams["count"] = Math.Min(model.Take.Value, 150).ToString();
            }

            if (model.Skip.HasValue)
            {
                requestParams["offset"] = Math.Max(model.Skip.Value, 0).ToString();
            }

            var task = await client.GetAsync($"{_apiUrl}?{requestParams}");
            if (task == null)
            {
                throw new ImageFetchException("No response from the API", _name);
            }

            var jsonString = await task.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                throw new ImageFetchException("Error when reading the response from the API", _name);
            }

            BingJson search;
            try
            {
                search = JsonConvert.DeserializeObject<BingJson>(jsonString);
            }
            catch (Exception ex)
            {
                throw new ImageFetchException(
                    $"Error when deserializing the response from the API ({ex.Message})",
                    _name);
            }

            var result = new ImageFetchResult
            {
                ImageItems = search.Value?.Select(i => new ImageItem
                {
                    Link = i.ContentUrl,
                    Title = i.Name
                }),
                TotalCount = search.TotalEstimatedMatches
            };

            return result;
        }
    }
}