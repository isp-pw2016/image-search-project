using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Isp.Core.Entities;
using Isp.Core.Entities.Jsons.Instagram;
using Isp.Core.Exceptions;
using Newtonsoft.Json;

namespace Isp.Core.ImageFetchers
{
    /// <summary>
    /// Instagram Tag API v1
    /// https://instagram.com/developer/
    /// 
    /// Implementation of the image fetching by tags via the Instagram's API
    /// using web requests
    /// 
    /// The API requires an OAuth access token, should be obtained and stored manually
    /// using the client id during the authorization on Instagram
    /// 
    /// Generated the token from a live user
    /// http://services.chrisriversdesign.com/instagram-token/
    /// 
    /// OAuth token may expire if not used
    /// 
    /// Attention:
    /// - API allows only a single tag to be queried
    /// - API supports strange pagination: take count is available, but skipping
    ///   is done by assign min_ and max_tag_id which are known from the request
    /// - API does not provide the total number of images one can get via a specific tag
    /// </summary>
    public class InstagramImageFetch : ImageFetchBase
    {
        private const string _name = "Instagram Tag";
        private readonly string _apiUrl = ConfigurationManager.AppSettings["InstagramApiUrl"];
        private readonly string _accessToken = ConfigurationManager.AppSettings["InstagramAccessToken"];

        protected override async Task<ImageFetchResult> FetchImage(ImageFetchQuery model)
        {
            var singleTag = model.Query
                .Trim()
                .Split(' ')
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(singleTag))
            {
                throw new ImageFetchException("API requires a single tag", _name);
            }

            var client = new HttpClient();

            var requestParams = HttpUtility.ParseQueryString(string.Empty);
            requestParams["access_token"] = _accessToken;

            if (model.Take.HasValue)
            {
                requestParams["count"] = model.Take.Value.ToString();
            }

            var apiUrlWithTag = string.Format(_apiUrl, singleTag);
            var task = await client.GetAsync($"{apiUrlWithTag}?{requestParams}");
            if (task == null)
            {
                throw new ImageFetchException("No response from the API", _name);
            }

            var jsonString = await task.Content.ReadAsStringAsync();
            if (jsonString == null)
            {
                throw new ImageFetchException("Error when reading the response from the API", _name);
            }

            InstagramJson search;
            try
            {
                search = JsonConvert.DeserializeObject<InstagramJson>(jsonString);
            }
            catch (Exception)
            {
                throw new ImageFetchException("Error when deserializing the response from the API", _name);
            }

            var result = new ImageFetchResult
            {
                ImageItems = search.Data?.Select(i => new ImageItem
                {
                    Link = i.Images?.StandardResolution?.Url,
                    Title = i.Caption?.Text
                })
            };

            return result;
        }
    }
}