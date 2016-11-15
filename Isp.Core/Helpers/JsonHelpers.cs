using System;
using Isp.Core.Exceptions;
using Newtonsoft.Json;

namespace Isp.Core.Helpers
{
    public static class JsonHelpers
    {
        public static T Deserialize<T>(string json, string name) where T : class
        {
            T model;
            try
            {
                model = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new CustomException(
                    $"Error when deserializing the object ({ex.Message})",
                    name);
            }

            return model;
        }
    }
}