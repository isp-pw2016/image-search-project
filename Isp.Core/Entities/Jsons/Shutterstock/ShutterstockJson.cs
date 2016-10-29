using System.Collections.Generic;
using Newtonsoft.Json;

namespace Isp.Core.Entities.Jsons.Shutterstock
{
    public class ShutterstockJson
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("search_id")]
        public string SearchId { get; set; }

        [JsonProperty("data")]
        public IList<Datum> Data { get; set; }
    }

    public class SmallJpg
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("dpi")]
        public long Dpi { get; set; }

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("is_licensable")]
        public bool IsLicensable { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public class MediumJpg
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("dpi")]
        public long Dpi { get; set; }

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("is_licensable")]
        public bool IsLicensable { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public class HugeJpg
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("dpi")]
        public long Dpi { get; set; }

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("is_licensable")]
        public bool IsLicensable { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public class HugeTiff
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("dpi")]
        public long Dpi { get; set; }

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("is_licensable")]
        public bool IsLicensable { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public class Preview
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public class SmallThumb
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public class LargeThumb
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public class Assets
    {
        [JsonProperty("small_jpg")]
        public SmallJpg SmallJpg { get; set; }

        [JsonProperty("medium_jpg")]
        public MediumJpg MediumJpg { get; set; }

        [JsonProperty("huge_jpg")]
        public HugeJpg HugeJpg { get; set; }

        [JsonProperty("huge_tiff")]
        public HugeTiff HugeTiff { get; set; }

        [JsonProperty("preview")]
        public Preview Preview { get; set; }

        [JsonProperty("small_thumb")]
        public SmallThumb SmallThumb { get; set; }

        [JsonProperty("large_thumb")]
        public LargeThumb LargeThumb { get; set; }
    }

    public class Category
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Contributor
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Datum
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("added_date")]
        public string AddedDate { get; set; }

        [JsonProperty("aspect")]
        public string Aspect { get; set; }

        [JsonProperty("assets")]
        public Assets Assets { get; set; }

        [JsonProperty("categories")]
        public IList<Category> Categories { get; set; }

        [JsonProperty("contributor")]
        public Contributor Contributor { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_type")]
        public string ImageType { get; set; }

        [JsonProperty("is_adult")]
        public bool IsAdult { get; set; }

        [JsonProperty("is_illustration")]
        public bool IsIllustration { get; set; }

        [JsonProperty("keywords")]
        public IList<string> Keywords { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }
    }
}