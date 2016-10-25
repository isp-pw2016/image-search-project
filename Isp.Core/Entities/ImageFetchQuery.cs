namespace Isp.Core.Entities
{
    public class ImageFetchQuery
    {
        public string Query { get; set; }

        public long Skip { get; set; }

        public long Take { get; set; }
    }
}