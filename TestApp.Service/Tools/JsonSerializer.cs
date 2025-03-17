using Newtonsoft.Json;

using TestApp.Domain.Filters;
using TestApp.Domain.Interfaces;

namespace TestApp.Domain.Tools
{
    /// <summary>
    /// Json Serializer
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json)!;
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="json"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(json, settings)!;
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Try Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        /// <exception cref="InternalValidationException"></exception>
        public T TryDeserialize<T>(string content, JsonSerializerSettings? jsonSettings = null)
        {
            T responseObjectData;

            try
            {
                responseObjectData = jsonSettings is not null ? Deserialize<T>(content, jsonSettings) : Deserialize<T>(content);
            }
            catch (Exception ex)
            {
                throw new InternalValidationException($"Object Result: {Serialize(content)} Exception: {Serialize(ex)}");
            }

            return responseObjectData;
        }
    }
}