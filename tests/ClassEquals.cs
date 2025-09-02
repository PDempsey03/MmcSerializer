using System.Reflection;

namespace MmcSerializer.Tests
{
    public static class ClassEquals
    {
        public static bool AreClassesEqualFromFieldsAndProperties(object? obj1, object? obj2)
        {
            if (obj1 == null || obj2 == null) return false;

            var obj1Type = obj1.GetType();
            var obj2Type = obj2.GetType();

            if (obj1Type != obj2Type) return false;

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var fields = obj1Type.GetFields(bindingFlags);
            foreach (var field in fields)
            {
                var thisValue = field.GetValue(obj1);
                var otherValue = field.GetValue(obj2);

                if (!Equals(thisValue, otherValue))
                    return false;
            }

            var properties = obj2Type.GetProperties(bindingFlags);
            foreach (var prop in properties)
            {
                if (!prop.CanRead) continue;

                var thisValue = prop.GetValue(obj1);
                var otherValue = prop.GetValue(obj2);

                if (!Equals(thisValue, otherValue))
                    return false;
            }

            return true;
        }
    }
}
