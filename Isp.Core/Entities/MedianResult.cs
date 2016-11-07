using System;
using Newtonsoft.Json;

namespace Isp.Core.Entities
{
    public class MedianResult
    {
        [JsonIgnore]
        public double Median { get; set; }

        [JsonProperty("Median")]
        public double MedianFormatted => Math.Round(Median*1000, 3);
    }
}