using Newtonsoft.Json;

namespace JSONParser
{
    public class Parser<T>
    {
        public Parser()
        {
        }

        public List<T> DeserializeFromString(string? jsonText)
        {
            if (jsonText is null)
                throw new Exception("JSON metni bulunamadı");

            var returnObject = JsonConvert.DeserializeObject<List<T>>(jsonText);

            if (returnObject is null)
                throw new Exception("JSON Deserialize Hatası");

            return returnObject;
        }

        public string SerializeFromObject(T features)
        {
            if (features is null)
                throw new NullReferenceException("Öznitelikler bulunamadı");
            var serialized = JsonConvert.SerializeObject(features, Formatting.Indented);

            return serialized;
        }
    }
}