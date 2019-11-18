namespace ProductShop
{
    using System.IO;
    using System.Text;
    using System.Runtime.Serialization.Json;

    /// <summary>
    /// Build-in Alternative for Newtonsoft.Json
    /// Less functional, slower... than Newtonsoft.Json
    /// </summary>

    public static class JsonHelper
    {
        public static string SerializeJson<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                var result = Encoding.UTF8.GetString(stream.ToArray());

                return result;
            }
        }

        public static T Deserialize<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            var jsonStringBytes = Encoding.UTF8.GetBytes(jsonString);

            using (var stream = new MemoryStream(jsonStringBytes))
            {
                var result = (T)serializer.ReadObject(stream);

                return result;
            }
        }
    }
}