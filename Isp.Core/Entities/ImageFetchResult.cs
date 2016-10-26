using System.Collections.Generic;

namespace Isp.Core.Entities
{
    public class ImageFetchResult
    {
        public IEnumerable<ImageItem> ImageItems { get; set; }

        public long TotalCount { get; set; }
    }
}