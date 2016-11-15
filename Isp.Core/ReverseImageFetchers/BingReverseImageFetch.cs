using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Isp.Core.Configs;
using Isp.Core.Entities;
using Isp.Core.Entities.Jsons.Bing;
using Isp.Core.Exceptions;
using Isp.Core.Helpers;

namespace Isp.Core.ReverseImageFetchers
{
    public class BingReverseImageFetch : ReverseImageFetchBase
    {
        private const string _name = "Bing Image Insights";

        protected override async Task<ReverseImageFetchResult> FetchReverseImage(ReverseImageFetchQuery model, string filePath)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AppSetting.BingApiKey);

                var imageBytes = File.ReadAllBytes(filePath);

                using (var content = new ByteArrayContent(imageBytes))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

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

                    var response = await client.PostAsync($"{AppSetting.BingApiUrl}?{requestParams}", content);
                    if (response == null)
                    {
                        throw new CustomException("No response from the API", _name);
                    }

                    var entity = await response.Content.ReadAsStreamAsync();
                    var jsonString = new StreamReader(entity).ReadToEnd();

                    var search = JsonHelpers.Deserialize<BingJson>(jsonString, _name);
                    var result = new ReverseImageFetchResult
                    {
                        ImageItems = search?.Value?.Select(i => new ImageItem
                        {
                            Link = i.ContentUrl,
                            Title = i.Name
                        }),
                        TotalCount = search?.TotalEstimatedMatches
                    };

                    return result;
                }
            }
        }
    }
}