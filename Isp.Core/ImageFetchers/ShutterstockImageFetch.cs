using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Isp.Core.Configs;
using Isp.Core.Entities;
using Isp.Core.Entities.Jsons.Shutterstock;
using Isp.Core.Exceptions;
using Isp.Core.Helpers;

namespace Isp.Core.ImageFetchers
{
    /// <summary>
    /// Shutterstock Image API v2
    /// https://developers.shutterstock.com/api/v2/images/search
    /// 
    /// Implementation of the image fetching via the Shutterstock's API using web requests
    /// 
    /// Free but when used commercially a footnote about using the API is required
    /// There is no explicit transactions limit, but there are mentions of limiting
    /// requests per second (no definite number given)
    /// 
    /// Accessing the image API does not require any OAuth tokens (others do)
    /// 
    /// Attention:
    /// - API allows up to 500 items per request, skipping (page number) starts from one
    /// - API requires User-Agent defined in headers
    /// </summary>
    public class ShutterstockImageFetch : ImageFetchBase
    {
        private const string _name = "Shutterstock Image Search";

        protected override async Task<ImageFetchResult> FetchImage(ImageFetchQuery model)
        {
            var requestParams = HttpUtility.ParseQueryString(string.Empty);
            requestParams["query"] = model.Query;
            requestParams["sort"] = "relevance";
            requestParams["license[0]"] = "commercial";
            requestParams["license[1]"] = "editorial";
            requestParams["license[2]"] = "enhanced";
            requestParams["license[3]"] = "sensitive";
            requestParams["license[4]"] = "NOT enhanced";
            requestParams["license[5]"] = "NOT sensitive";

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
                var credentials = AppSetting.ShutterstockCredentials;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                client.DefaultRequestHeaders.Add("User-Agent", "ISP");

                var task = await client.GetByteArrayAsync($"{AppSetting.ShutterstockApiUrl}?{requestParams}");
                if (task == null)
                {
                    throw new CustomException("No response from the API", _name);
                }

                jsonString = Encoding.UTF8.GetString(task);
            }

            var search = JsonHelpers.Deserialize<ShutterstockJson>(jsonString, _name);
            var result = new ImageFetchResult
            {
                ImageItems = search?.Data?.Select(i => new ImageItem
                {
                    Link = i.Assets?.Preview?.Url,
                    Title = i.Description
                }),
                TotalCount = search?.TotalCount
            };

            return result;
        }
    }
}