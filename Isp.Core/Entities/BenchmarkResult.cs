using System;
using Isp.Core.Extensions;
using Newtonsoft.Json;

namespace Isp.Core.Entities
{
    public class BenchmarkResult
    {
        public ImageFetchResult ImageFetch { get; set; }

        [JsonIgnore]
        public double Stopwatch { get; set; }

        public double Time => Math.Round(Stopwatch, 9);

        public string TimeString => Time.ToTimeString();
    }
}