using Newtonsoft.Json;

namespace TestApp.Domain.Interfaces
{
    /// <summary>
    /// Interface of Serializer
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        T Deserialize<T>(string json);

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="json"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        T Deserialize<T>(string json, JsonSerializerSettings settings);

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Serialize(object data);

        /// <summary>
        /// Try Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        T TryDeserialize<T>(string content, JsonSerializerSettings? jsonSettings = null);
    }
}