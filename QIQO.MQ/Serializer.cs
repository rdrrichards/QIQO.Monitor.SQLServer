using Newtonsoft.Json;
using System;
using System.Text;

namespace QIQO.MQ
{
    public static class Serializer
    {
        public static byte[] Serialize(this object obj)
        {
            if (obj == null) return null;
            return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public static object DeSerialize(this byte[] arrBytes, Type type) => JsonConvert.DeserializeObject(Encoding.Default.GetString(arrBytes), type);

        public static string DeSerializeText(this byte[] arrBytes) => Encoding.Default.GetString(arrBytes);
    }
}
