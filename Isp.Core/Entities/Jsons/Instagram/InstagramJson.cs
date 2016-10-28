using System.Collections.Generic;
using Newtonsoft.Json;

namespace Isp.Core.Entities.Jsons.Instagram
{
    public class InstagramJson
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public IList<Datum> Data { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("next_max_tag_id")]
        public string NextMaxTagId { get; set; }

        [JsonProperty("deprecation_warning")]
        public string DeprecationWarning { get; set; }

        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }

        [JsonProperty("next_min_id")]
        public string NextMinId { get; set; }

        [JsonProperty("min_tag_id")]
        public string MinTagId { get; set; }

        [JsonProperty("next_url")]
        public string NextUrl { get; set; }
    }

    public class Meta
    {
        [JsonProperty("code")]
        public int Code { get; set; }
    }

    public class LowResolution
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class StandardResolution
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class LowBandwidth
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class Videos
    {
        [JsonProperty("low_resolution")]
        public LowResolution LowResolution { get; set; }

        [JsonProperty("standard_resolution")]
        public StandardResolution StandardResolution { get; set; }

        [JsonProperty("low_bandwidth")]
        public LowBandwidth LowBandwidth { get; set; }
    }

    public class Location
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }

    public class Comments
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class Likes
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class Images
    {
        [JsonProperty("low_resolution")]
        public LowResolution LowResolution { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("standard_resolution")]
        public StandardResolution StandardResolution { get; set; }
    }

    public class Position
    {
        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }
    }

    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }

    public class UsersInPhoto
    {
        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class From
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }

    public class Caption
    {
        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Datum
    {
        [JsonProperty("attribution")]
        public object Attribution { get; set; }

        [JsonProperty("videos")]
        public Videos Videos { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("comments")]
        public Comments Comments { get; set; }

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("likes")]
        public Likes Likes { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("users_in_photo")]
        public IList<UsersInPhoto> UsersInPhoto { get; set; }

        [JsonProperty("caption")]
        public Caption Caption { get; set; }

        [JsonProperty("user_has_liked")]
        public bool UserHasLiked { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}