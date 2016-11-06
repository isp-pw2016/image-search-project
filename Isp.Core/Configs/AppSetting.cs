using System;
using System.Configuration;
using System.Text;

namespace Isp.Core.Configs
{
    public static class AppSetting
    {
        public static readonly string GoogleApiUrl = ConfigurationManager.AppSettings["GoogleApiUrl"];
        public static readonly string GoogleApiKey = ConfigurationManager.AppSettings["GoogleApiKey"];
        public static readonly string GoogleEngineId = ConfigurationManager.AppSettings["GoogleEngineId"];

        public static readonly string BingApiUrl = ConfigurationManager.AppSettings["BingApiUrl"];
        public static readonly string BingApiKey = ConfigurationManager.AppSettings["BingApiKey"];

        public static readonly string InstagramApiUrl = ConfigurationManager.AppSettings["InstagramApiUrl"];
        public static readonly string InstagramAccessToken = ConfigurationManager.AppSettings["InstagramAccessToken"];

        public static readonly string FlickrApiUrl = ConfigurationManager.AppSettings["FlickrApiUrl"];
        public static readonly string FlickrApiKey = ConfigurationManager.AppSettings["FlickrApiKey"];
        public static readonly string FlickrPhotoUrl = ConfigurationManager.AppSettings["FlickrPhotoUrl"];

        public static readonly string ShutterstockApiUrl = ConfigurationManager.AppSettings["ShutterstockApiUrl"];
        public static readonly string ShutterstockClientId = ConfigurationManager.AppSettings["ShutterstockClientId"];
        public static readonly string ShutterstockClientSecret = ConfigurationManager.AppSettings["ShutterstockClientSecret"];

        public static readonly string ShutterstockCredentials
            = Convert.ToBase64String(Encoding.ASCII.GetBytes(
                $"{ShutterstockClientId}:{ShutterstockClientSecret}"));
    }
}