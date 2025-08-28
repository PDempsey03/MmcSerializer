using System.Collections;

namespace MmcSerializer.Extensions
{
    public static class TypeExtensions
    {
        public static TypeCategory GetTypeCategory(this Type type)
        {
            var possibleNullableUnderlyingType = Nullable.GetUnderlyingType(type);
            if (possibleNullableUnderlyingType != null)
            {
                if (possibleNullableUnderlyingType.IsEnum) return TypeCategory.NullableEnum;
                if (possibleNullableUnderlyingType.IsPrimitive) return TypeCategory.NullablePrimitive;
                if (possibleNullableUnderlyingType.IsValueType) return TypeCategory.NullableStruct;
            }

            if (type == typeof(string))
                return TypeCategory.String;

            if (type.IsArray)
                return TypeCategory.Array;

            if (typeof(IEnumerable).IsAssignableFrom(type))
                return TypeCategory.Enumerable;

            if (type.IsEnum)
                return TypeCategory.Enum;

            if (type.IsPrimitive) return TypeCategory.Primitive;

            if (type.IsValueType) return TypeCategory.Struct;

            else return TypeCategory.Class;
        }
    }

    public enum TypeCategory
    {
        Primitive,
        NullablePrimitive,
        Enum,
        NullableEnum,
        Struct,
        NullableStruct,
        Class,
        String,
        Array,
        Enumerable,
        Unknown
    }
}
