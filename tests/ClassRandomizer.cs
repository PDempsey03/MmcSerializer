using System.Reflection;

namespace MmcSerializer.Tests
{
    public static class ClassRandomizer
    {
        public static void RandomizeClassFieldAndProperties<T>(T obj)
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                object? value = RandomizeFromType(field.FieldType) ?? field.GetValue(obj);

                field.SetValue(obj, value);
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (!property.CanWrite) continue;

                object? value = RandomizeFromType(property.PropertyType) ?? property.GetValue(obj);

                property.SetValue(obj, value);
            }
        }

        public static void RandomizeStructFieldAndProperties<T>(ref T obj)
        {
            if (obj == null) return;

            var boxedCopy = (object)obj;

            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                object? value = RandomizeFromType(field.FieldType) ?? field.GetValue(boxedCopy);

                field.SetValue(boxedCopy, value);
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (!property.CanWrite) continue;

                object? value = RandomizeFromType(property.PropertyType) ?? property.GetValue(boxedCopy);

                property.SetValue(boxedCopy, value);
            }

            obj = (T)boxedCopy;
        }

        private static object? RandomizeFromType(Type type)
        {
            Random rng = Random.Shared;

            return type switch
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
                Type t when t.IsEnum => Enum.GetValues(t).GetValue(rng.Next(Enum.GetValues(t).Length)),
                Type t when t.IsValueType && !t.IsPrimitive && !t.IsEnum => HandleRandomizeStructFromType(t),
                // Type t when t.IsArray => 
                Type t when t.IsClass && !t.IsAbstract => HandleRandomizeClassFromType(t), // TODO: could allow abstract, get classes assignable from, and then pick a random one
                _ => null
            };
        }

        private static object? HandleRandomizeClassFromType(Type type)
        {
            var instance = Activator.CreateInstance(type);

            if (instance == null) return null;

            var method = typeof(ClassRandomizer).GetMethod(nameof(RandomizeStructFieldAndProperties), BindingFlags.Public | BindingFlags.Static);

            var genericMethod = method!.MakeGenericMethod(type);

            object[] parameters = [instance];

            genericMethod.Invoke(null, parameters);

            return parameters[0];
        }

        private static object? HandleRandomizeStructFromType(Type type)
        {
            var instance = Activator.CreateInstance(type);

            if (instance == null) return null;

            var method = typeof(ClassRandomizer).GetMethod(nameof(RandomizeStructFieldAndProperties), BindingFlags.Public | BindingFlags.Static);

            var genericMethod = method!.MakeGenericMethod(type);

            object[] parameters = [instance];

            genericMethod.Invoke(null, parameters);

            return parameters[0];
        }
    }
}