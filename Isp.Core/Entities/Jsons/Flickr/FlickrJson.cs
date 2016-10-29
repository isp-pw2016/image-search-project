using System.Collections.Generic;
using Newtonsoft.Json;

namespace Isp.Core.Entities.Jsons.Flickr
{
    public class FlickrJson
    {
        [JsonProperty("photos")]
        public Photos Photos { get; set; }

        [JsonProperty("stat")]
        public string Stat { get; set; }
    }

    public class Photo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("farm")]
        public long Farm { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("ispublic")]
        public long Ispublic { get; set; }

        [JsonProperty("isfriend")]
        public long Isfriend { get; set; }

        [JsonProperty("isfamily")]
        public long Isfamily { get; set; }
    }

    public class Photos
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }

        [JsonProperty("perpage")]
        public long Perpage { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("photo")]
        public IList<Photo> Photo { get; set; }
    }
}