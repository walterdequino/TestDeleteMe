using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test.Api.Helpers
{
    /// <summary>
    /// Null to Default Json Converter
    /// </summary>
    public class NullToDefaultJsonConverter : JsonConverter
    {
        /// <summary>
        /// Keep an internal reference to the StringType for performance!
        /// </summary>
        private static readonly Type StringType = typeof(string);

        /// <summary>
        /// Can Type Be Assigned Null
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool CanTypeBeAssignedNull(Type type)
            => !type.IsValueType || (Nullable.GetUnderlyingType(type) != null);

        /// <summary>
        /// ONLY Handle Fields that would fail a Null assignment and requires resolving of a non-null existing/default value for the Type!
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
            => !CanTypeBeAssignedNull(objectType);

        /// <summary>
        /// Read Json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingOrDefaultValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingOrDefaultValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            try
            {
                return token.Type switch
                {
                    //If the Json Token is Null then we should always use the Default Value (e.g. int=0, bool=false, etc.);
                    //  also works as expected for Nullable Values (e.g. int?=null, bool?=null, etc.)
                    JTokenType.Null => existingOrDefaultValue,

                    //For performance we try skip exceptions for some cases:
                    //  1) Cases where Empty Strings are passed (incorrectly) but previously were coerced to Default values exceptions!
                    JTokenType.String when objectType != StringType && string.IsNullOrEmpty(token.Value<string>()) => existingOrDefaultValue,

                    //Finally we allow the Token to manage the conversion of the actual value, of which exceptions
                    //  will be handled in a way compatible with legacy JsonMediaTypeFormatter...

                    _ => token.ToObject(objectType)
                };
            }
            catch (Exception)
            {
                return existingOrDefaultValue;
            }
        }

        /// <summary>
        /// Return false; we want normal Json.NET behavior when serializing...
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// WriteJson
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
