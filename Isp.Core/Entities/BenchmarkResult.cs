using Isp.Core.Extensions;

namespace Isp.Core.Entities
{
    public class BenchmarkResult
    {
        public ImageFetchResult ImageFetch { get; set; }

        public double Time { get; set; }

        public string TimeString => Time.ToTimeString();
    }
}