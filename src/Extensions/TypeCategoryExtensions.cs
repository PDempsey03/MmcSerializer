namespace MmcSerializer.Extensions
{
    public static class TypeCategoryExtensions
    {
        public static TypeCategory ToNullable(this TypeCategory typeCategory)
        {
            return typeCategory switch
            {
                TypeCategory.Primitive => TypeCategory.NullablePrimitive,
                TypeCategory.Enum => TypeCategory.NullableEnum,
                TypeCategory.Struct => TypeCategory.NullableStruct,
                _ => typeCategory
            };
        }
    }
}
