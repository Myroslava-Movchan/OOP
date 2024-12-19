using Online_Store_Management.Interfaces;
using System.Text.Json;
namespace Online_Store_Management.Services
{
    public class Serializer 
    {
        public static string SerializeDateTime(DateTime value)
        {
            return JsonSerializer.Serialize(value);
        }

        public static DateTime DeserializeDateTime(string? value)
        {
            return JsonSerializer.Deserialize<DateTime>(value);
        }

        public static string SerializeFloat(float value)
        {
            return JsonSerializer.Serialize(value);
        }

        public static float DeserializeFloat(string? value)
        {
            return JsonSerializer.Deserialize<float>(value);
        }
    }
}
