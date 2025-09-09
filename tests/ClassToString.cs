using MmcSerializer.Extensions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace MmcSerializer.Tests
{
    public static class ClassToString
    {
        private const string NullString = "null";
        private const char IndentationChar = '\t';

        public static string GetObjectToStringFromFieldAndProperties(object? obj)
        {
            StringBuilder stringBuilder = new StringBuilder();

            ConvertObjectToStringFromFieldsAndProperties(obj, stringBuilder);

            return stringBuilder.ToString();
        }

        private static void ConvertObjectToStringFromFieldsAndProperties(object? obj, StringBuilder stringBuilder, string indentation = "")
        {
            if (stringBuilder == null) return;

            if (obj == null)
            {
                stringBuilder.AppendLine(indentation + NullString);
                return;
            }

            var type = obj.GetType();
            var typeCategory = type.GetTypeCategory();

            // straight print certain base types
            if (typeCategory == TypeCategory.Enum || typeCategory == TypeCategory.NullableEnum ||
                typeCategory == TypeCategory.Primitive || typeCategory == TypeCategory.NullablePrimitive ||
                typeCategory == TypeCategory.String)
            {
                stringBuilder.AppendLine(indentation + obj.ToString());
                return;
            }

            // get list of field/property name and its associated value
            List<(string, object?)> objects = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                 .Where(f => !f.IsDefined(typeof(CompilerGeneratedAttribute)))
                 .Select(f => (f.Name, f.GetValue(obj)))
                 .Concat(
                     type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                         .Select(p => (p.Name, p.GetValue(obj)))
                 ).ToList();

            foreach (var tuple in objects)
            {
                string name = tuple.Item1;
                object? value = tuple.Item2;

                stringBuilder.Append(indentation + name + ": ");
                var newIndentation = indentation + IndentationChar;

                if (value == null)
                {
                    stringBuilder.AppendLine(NullString);
                    continue;
                }

                var valuesTypeCategory = value.GetType().GetTypeCategory();

                if (valuesTypeCategory == TypeCategory.Class ||
                    valuesTypeCategory == TypeCategory.Struct ||
                    valuesTypeCategory == TypeCategory.NullableStruct)
                {
                    stringBuilder.AppendLine();
                    ConvertObjectToStringFromFieldsAndProperties(value, stringBuilder, newIndentation);
                }
                else if (valuesTypeCategory == TypeCategory.Array)
                {
                    stringBuilder.AppendLine();
                    Array tempArray = (Array)value;
                    foreach (var item in tempArray)
                    {
                        ConvertObjectToStringFromFieldsAndProperties(item, stringBuilder, newIndentation);
                    }
                }
                else
                {
                    stringBuilder.AppendLine(value.ToString());
                }
            }
        }
    }
}
