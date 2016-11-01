using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Isp.Core.Entities;
using Isp.Core.Entities.Jsons.Instagram;
using Isp.Core.Exceptions;

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
    /// - API seems to receive up to 33 images
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

            var requestParams = HttpUtility.ParseQueryString(string.Empty);
            requestParams["access_token"] = _accessToken;

            if (model.Take.HasValue)
            {
                requestParams["count"] = model.Take.Value.ToString();
            }

            var apiUrlWithTag = string.Format(_apiUrl, singleTag);

            string jsonString;
            using (var client = new HttpClient())
            {
                var task = await client.GetAsync($"{apiUrlWithTag}?{requestParams}");
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

            var search = JsonDeserialize<InstagramJson>(jsonString, _name);
            var result = new ImageFetchResult
            {
                ImageItems = search?.Data?.Select(i => new ImageItem
                {
                    Link = i.Images?.StandardResolution?.Url,
                    Title = i.Caption?.Text
                })
            };

            return result;
        }
    }
}