using System.Collections.Generic;
using Newtonsoft.Json;

namespace Isp.Core.Entities
{
    public class ReverseImageFetchResult
    {
        public IEnumerable<ImageItem> ImageItems { get; set; }

        [JsonIgnore]
        public long? TotalCount { get; set; }

        [JsonProperty("TotalCount")]
        public string TotalCountFormatted => TotalCount?.ToString() ?? "?";
    }
}