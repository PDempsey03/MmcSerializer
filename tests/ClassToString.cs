using MmcSerializer.Extensions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace MmcSerializer.Tests
{
    public static class ClassToString
    {
        private const string NullString = "null";
        private const string ArrayElementStart = "{";
        private const string ArrayElementEnd = "},";
        private const string FinalArrayElementEnd = "}";
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
                    PrintRecursive(tempArray, stringBuilder, new int[tempArray.Rank], 0, newIndentation);
                }
                else
                {
                    stringBuilder.AppendLine(value.ToString());
                }
            }
        }

        private static void PrintRecursive(Array array, StringBuilder stringBuilder, int[] indices, int dimension, string indentation)
        {
            int length = array.GetLength(dimension);

            stringBuilder.Append(indentation);
            stringBuilder.AppendLine("[");

            string newIndentation = indentation + IndentationChar;

            for (int i = 0; i < length; i++)
            {
                indices[dimension] = i;

                if (dimension < array.Rank - 1)
                {
                    PrintRecursive(array, stringBuilder, indices, dimension + 1, newIndentation);
                }
                else
                {
                    stringBuilder.AppendLine(newIndentation + ArrayElementStart);
                    ConvertObjectToStringFromFieldsAndProperties(array.GetValue(indices), stringBuilder, newIndentation);
                    stringBuilder.AppendLine(newIndentation + (i == length - 1 ? FinalArrayElementEnd : ArrayElementEnd));
                }
            }
            stringBuilder.AppendLine(indentation + "]");
        }
    }
}
