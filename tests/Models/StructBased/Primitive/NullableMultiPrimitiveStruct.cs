namespace MmcSerializer.Tests.Models.StructBased.Primitive
{
    public struct NullableMultiPrimitiveStruct
    {
        public bool? _boolField = null;

        public byte? _byteField = null;

        public sbyte? _sbyteField = null;

        public char? _charField = null;

        public double? _doubleField = null;

        public float? _floatField = null;

        public int? _intField = null;

        public uint? _uintField = null;

        public nint? _nintField = null;

        public nuint? _nuintField = null;

        public long? _longField = null;

        public ulong? _ulongField = null;

        public short? _shortField = null;

        public ushort? _ushortField = null;

        private bool? _privateBoolField = null;

        private byte? _privateByteField = null;

        private sbyte? _privateSbyteField = null;

        private char? _privateCharField = null;

        private double? _privateDoubleField = null;

        private float? _privateFloatField = null;

        private int? _privateIntField = null;

        private uint? _privateUintField = null;

        private nint? _privateNintField = null;

        private nuint? _privateNuintField = null;

        private long? _privateLongField = null;

        private ulong? _privateUlongField = null;

        private short? _privateShortField = null;

        private ushort? _privateUshortField = null;

        public bool? BoolProperty { get; set; } = null;

        public byte? ByteProperty { get; set; } = null;

        public sbyte? SbyteProperty { get; set; } = null;

        public char? CharProperty { get; set; } = null;

        public double? DoubleProperty { get; set; } = null;

        public float? FloatProperty { get; set; } = null;

        public int? IntProperty { get; set; } = null;

        public uint? UintProperty { get; set; } = null;

        public nint? NintProperty { get; set; } = null;

        public nuint? NuintProperty { get; set; } = null;

        public long? LongProperty { get; set; } = null;

        public ulong? UlongProperty { get; set; } = null;

        public short? ShortProperty { get; set; } = null;

        public ushort? UshortProperty { get; set; } = null;

        private bool? PrivateBoolProperty { get; set; } = null;

        private byte? PrivateByteProperty { get; set; } = null;

        private sbyte? PrivateSbyteProperty { get; set; } = null;

        private char? PrivateCharProperty { get; set; } = null;

        private double? PrivateDoubleProperty { get; set; } = null;

        private float? PrivateFloatProperty { get; set; } = null;

        private int? PrivateIntProperty { get; set; } = null;

        private uint? PrivateUintProperty { get; set; } = null;

        private nint? PrivateNintProperty { get; set; } = null;

        private nuint? PrivateNuintProperty { get; set; } = null;

        private long? PrivateLongProperty { get; set; } = null;

        private ulong? PrivateUlongProperty { get; set; } = null;

        private short? PrivateShortProperty { get; set; } = null;

        private ushort? PrivateUshortProperty { get; set; } = null;

        public NullableMultiPrimitiveStruct()
        {

        }

        public override bool Equals(object? obj)
        {
            return ClassEquals.AreClassesEqualFromFieldsAndProperties(this, obj);
        }

        public override string ToString()
        {
            return ClassToString.GetObjectToStringFromFieldAndProperties(this);
        }
    }
}
