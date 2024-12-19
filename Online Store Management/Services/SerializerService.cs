using System;
using System.Globalization;

namespace Online_Store_Management.Services
{
    public class SerializerService
    {
        public void Serialization()
        {
            string logFilePath = "serialization-log.txt";
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-CA");

                DateTime currentDate = DateTime.Now;
                float floatValue = 1998.50F;

                string serializedDateTimeX = Serializer.SerializeDateTime(currentDate);
                string serializedFloatX = Serializer.SerializeFloat(floatValue);

                writer.WriteLine("Culture: fr-CA");
                writer.WriteLine($"Serialized DateTime: {serializedDateTimeX}");
                writer.WriteLine($"Serialized int: {serializedFloatX}");

                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

                DateTime deserializedDateTimeY = Serializer.DeserializeDateTime(serializedDateTimeX);
                float deserializedFloatY = Serializer.DeserializeFloat(serializedFloatX);

                writer.WriteLine("\nCulture: de-DE");
                writer.WriteLine($"Deserialized DateTime: {deserializedDateTimeY}");
                writer.WriteLine($"Deserialized int: {deserializedFloatY}");

                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("es-ES");
                string serializedDateTimeZ = Serializer.SerializeDateTime(currentDate);
                string serializedFloatZ = Serializer.SerializeFloat(floatValue);
                writer.WriteLine("\nFinal results logged:");
                writer.WriteLine($"DateTime (es-ES): {serializedDateTimeZ}");
                writer.WriteLine($"Int (es-ES): {serializedFloatZ}");
            }
        }
    }
}
