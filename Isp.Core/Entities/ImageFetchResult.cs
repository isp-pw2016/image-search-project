using System.Collections.Generic;
using Isp.Core.Extensions;
using Newtonsoft.Json;

namespace Isp.Core.Entities
{
    public class ImageFetchResult
    {
        public IEnumerable<ImageItem> ImageItems { get; set; }

        [JsonIgnore]
        public long? TotalCount { get; set; }

        [JsonProperty("TotalCount")]
        public string TotalCountFormatted => TotalCount?.ToString() ?? "NULL";

        public double? Time { get; set; }

        public string TimeString => Time.ToTimeString();
    }
}