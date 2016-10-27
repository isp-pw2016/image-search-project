using System.Collections.Generic;
using Isp.Core.Extensions;

namespace Isp.Core.Entities
{
    public class ImageFetchResult
    {
        public IEnumerable<ImageItem> ImageItems { get; set; }

        public long TotalCount { get; set; }

        public double? Time { get; set; }

        public string TimeString => Time.ToTimeString();
    }
}