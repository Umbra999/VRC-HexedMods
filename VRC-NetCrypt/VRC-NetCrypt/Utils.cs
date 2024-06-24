using System.Runtime.Serialization.Formatters.Binary;

namespace VRC_NetCrypt
{
    internal class Utils
    {
        public static void EncryptOrDecrypt(byte[] data, int startIndex = 0, int endIndex = int.MaxValue)
        {
            if (endIndex > data.Length) endIndex = data.Length;

            sbyte key = (sbyte)('A' - 'Z');
            for (int i = startIndex; i < endIndex; i++)
            {
                data[i] ^= (byte)key;
            }
        }

        private static byte[] IL2CPPObjectToByteArray(Il2CppSystem.Object obj)
        {
            if (obj == null) return null;
            var bf = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var ms = new Il2CppSystem.IO.MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        private static byte[] ManagedObjectToByteArray(object obj)
        {
            if (obj == null) return null;
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        private static object ManagedObjectFromArray(byte[] data)
        {
            if (data == null) return default;
            BinaryFormatter bf = new();
            using MemoryStream ms = new(data);
            object obj = bf.Deserialize(ms);
            return obj;
        }

        private static Il2CppSystem.Object IL2CPPObjectFromArray(byte[] data)
        {
            if (data == null) return default;
            var bf = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var ms = new Il2CppSystem.IO.MemoryStream(data);
            object obj = bf.Deserialize(ms);
            return (Il2CppSystem.Object)obj;
        }

        public static object Serialize(Il2CppSystem.Object obj)
        {
            return ManagedObjectFromArray(IL2CPPObjectToByteArray(obj));
        }

        public static Il2CppSystem.Object Serialize(object obj)
        {
            return IL2CPPObjectFromArray(ManagedObjectToByteArray(obj));
        }
    }
}
