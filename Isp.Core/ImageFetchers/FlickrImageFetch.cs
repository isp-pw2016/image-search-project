using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Isp.Core.Entities;
using Isp.Core.Entities.Jsons.Flickr;
using Isp.Core.Exceptions;
using Isp.Core.Extensions;

namespace Isp.Core.ImageFetchers
{
    /// <summary>
    /// Flickr Photos Search API v1
    /// https://flickr.com/services/api/flickr.photos.search.html
    /// 
    /// Implementation of the image fetching by tags via the Flickr's API
    /// using web requests
    /// 
    /// Free but with a hourly limit of 3600 transactions
    /// Keys generated for non-commercial access
    /// 
    /// API does not return the image URL, but gives data necessary to construct one
    /// https://www.flickr.com/services/api/misc.urls.html
    /// 
    /// Attention:
    /// - API allows up to 20 tags to be queried
    /// - API returns up to 4000 per a request
    /// - API allows up to 500 items per request, skipping (page number) starts from one
    /// </summary>
    public class FlickrImageFetch : ImageFetchBase
    {
        private const string _name = "Flickr Photos Search";
        private readonly string _apiUrl = ConfigurationManager.AppSettings["FlickrApiUrl"];
        private readonly string _apiKey = ConfigurationManager.AppSettings["FlickrApiKey"];
        private readonly string _photoUrl = ConfigurationManager.AppSettings["FlickrPhotoUrl"];

        protected override async Task<ImageFetchResult> FetchImage(ImageFetchQuery model)
        {
            var tags = model.Query
                .Trim()
                .Split(new[] {' '}, 20);

            var tagsParam = string.Join(",", tags);

            var requestParams = HttpUtility.ParseQueryString(string.Empty);
            requestParams["method"] = "flickr.photos.search";
            requestParams["api_key"] = _apiKey;
            requestParams["format"] = "json";
            requestParams["nojsoncallback"] = "1";
            requestParams["tags"] = tagsParam;
            requestParams["sort"] = "relevance";

            if (model.Take.HasValue)
            {
                requestParams["per_page"] = Math.Min(model.Take.Value, 500).ToString();
            }

            if (model.Skip.HasValue)
            {
                requestParams["page"] = Math.Max(model.Skip.Value, 1).ToString();
            }

            string jsonString;
            using (var client = new HttpClient())
            {
                var task = await client.GetAsync($"{_apiUrl}?{requestParams}");
                if (task?.Content == null)
                {
                    throw new ImageFetchException("No response from the API", _name);
                }

                jsonString = await task.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    throw new ImageFetchException("Error when reading the response from the API", _name);
                }
            }

            var search = JsonDeserialize<FlickrJson>(jsonString, _name);
            var result = new ImageFetchResult
            {
                ImageItems = search?.Photos?.Photo?.Select(i => new ImageItem
                {
                    Link = string.Format(_photoUrl, i.Farm, i.Server, i.Id, i.Secret),
                    Title = i.Title
                }),
                TotalCount = search?.Photos?.Total.TryToInt64()
            };

            return result;
        }
    }
}