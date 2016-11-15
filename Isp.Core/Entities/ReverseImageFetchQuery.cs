namespace Isp.Core.Entities
{
    public class ReverseImageFetchQuery
    {
        public string Query { get; set; }

        public string FileName { get; set; }

        public long? Skip { get; set; }

        public long? Take { get; set; }
    }
}