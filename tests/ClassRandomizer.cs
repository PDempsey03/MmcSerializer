using System.Reflection;

namespace MmcSerializer.Tests
{
    public static class ClassRandomizer
    {
        public static void RandomizeClassFieldAndProperties<T>(T obj) where T : class
        {
            Random rng = new Random();

            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                object? value = field.FieldType switch
                {
                    Type t when t == typeof(bool) => rng.Next(2) == 0,
                    Type t when t == typeof(byte) => (byte)rng.Next(byte.MinValue, byte.MaxValue + 1),
                    Type t when t == typeof(sbyte) => (sbyte)rng.Next(sbyte.MinValue, sbyte.MaxValue + 1),
                    Type t when t == typeof(char) => (char)rng.Next(char.MinValue + 0xA, 0xD700), // avoid unicode surrogate issues
                    Type t when t == typeof(double) => rng.NextDouble() * 1000 - 500,
                    Type t when t == typeof(float) => (float)(rng.NextDouble() * 1000 - 500),
                    Type t when t == typeof(int) => rng.Next(int.MinValue, int.MaxValue),
                    Type t when t == typeof(uint) => (uint)rng.Next(int.MinValue, int.MaxValue),
                    Type t when t == typeof(nint) => (nint)rng.Next(int.MinValue, int.MaxValue),
                    Type t when t == typeof(nuint) => (nuint)rng.Next(int.MinValue, int.MaxValue),
                    Type t when t == typeof(long) => (long)(rng.NextDouble() * long.MaxValue),
                    Type t when t == typeof(ulong) => (ulong)(rng.NextDouble() * ulong.MaxValue),
                    Type t when t == typeof(short) => (short)rng.Next(short.MinValue, short.MaxValue + 1),
                    Type t when t == typeof(ushort) => (ushort)rng.Next(ushort.MinValue, ushort.MaxValue + 1),
                    Type t when t == typeof(string) => new string([.. Enumerable.Range(0, rng.Next(5, 20)).Select(_ => (char)rng.Next('a', 'z' + 1))]),
                    Type t when t == typeof(Enum) => Enum.GetValues(t).GetValue(rng.Next(Enum.GetValues(t).Length)),
                    _ => field.GetValue(obj)! // if not primitive type, leave untouched
                };

                field.SetValue(obj, value);
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in properties)
            {
                object? value = property.PropertyType switch
                {
                    Type t when t == typeof(bool) => rng.Next(2) == 0,
                    Type t when t == typeof(byte) => (byte)rng.Next(byte.MinValue, byte.MaxValue + 1),
                    Type t when t == typeof(sbyte) => (sbyte)rng.Next(sbyte.MinValue, sbyte.MaxValue + 1),
                    Type t when t == typeof(char) => (char)rng.Next(char.MinValue + 0xA, 0xD700), // avoid unicode surrogate issues
                    Type t when t == typeof(double) => rng.NextDouble() * 1000 - 500,
                    Type t when t == typeof(float) => (float)(rng.NextDouble() * 1000 - 500),
                    Type t when t == typeof(int) => rng.Next(int.MinValue, int.MaxValue),
                    Type t when t == typeof(uint) => (uint)rng.Next(int.MinValue, int.MaxValue),
                    Type t when t == typeof(nint) => (nint)rng.Next(int.MinValue, int.MaxValue),
                    Type t when t == typeof(nuint) => (nuint)rng.Next(int.MinValue, int.MaxValue),
                    Type t when t == typeof(long) => (long)(rng.NextDouble() * long.MaxValue),
                    Type t when t == typeof(ulong) => (ulong)(rng.NextDouble() * ulong.MaxValue),
                    Type t when t == typeof(short) => (short)rng.Next(short.MinValue, short.MaxValue + 1),
                    Type t when t == typeof(ushort) => (ushort)rng.Next(ushort.MinValue, ushort.MaxValue + 1),
                    Type t when t == typeof(string) => new string([.. Enumerable.Range(0, rng.Next(5, 20)).Select(_ => (char)rng.Next('a', 'z' + 1))]),
                    Type t when t == typeof(Enum) => Enum.GetValues(t).GetValue(rng.Next(Enum.GetValues(t).Length)),
                    _ => property.GetValue(obj)! // if not primitive type, leave untouched
                };

                property.SetValue(obj, value);
            }
        }
    }
}
