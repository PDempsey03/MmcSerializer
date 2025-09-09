namespace MmcSerializer.Tests.Models.ClassBased.Primitive
{
    public class MultiPrimitive
    {
        public bool _boolField;

        public byte _byteField;

        public sbyte _sbyteField;

        public char _charField;

        public double _doubleField;

        public float _floatField;

        public int _intField;

        public uint _uintField;

        public nint _nintField;

        public nuint _nuintField;

        public long _longField;

        public ulong _ulongField;

        public short _shortField;

        public ushort _ushortField;

        private bool _privateBoolField;

        private byte _privateByteField;

        private sbyte _privateSbyteField;

        private char _privateCharField;

        private double _privateDoubleField;

        private float _privateFloatField;

        private int _privateIntField;

        private uint _privateUintField;

        private nint _privateNintField;

        private nuint _privateNuintField;

        private long _privateLongField;

        private ulong _privateUlongField;

        private short _privateShortField;

        private ushort _privateUshortField;

        public bool BoolProperty { get; set; }

        public byte ByteProperty { get; set; }

        public sbyte SbyteProperty { get; set; }

        public char CharProperty { get; set; }

        public double DoubleProperty { get; set; }

        public float FloatProperty { get; set; }

        public int IntProperty { get; set; }

        public uint UintProperty { get; set; }

        public nint NintProperty { get; set; }

        public nuint NuintProperty { get; set; }

        public long LongProperty { get; set; }

        public ulong UlongProperty { get; set; }

        public short ShortProperty { get; set; }

        public ushort UshortProperty { get; set; }

        private bool PrivateBoolProperty { get; set; }

        private byte PrivateByteProperty { get; set; }

        private sbyte PrivateSbyteProperty { get; set; }

        private char PrivateCharProperty { get; set; }

        private double PrivateDoubleProperty { get; set; }

        private float PrivateFloatProperty { get; set; }

        private int PrivateIntProperty { get; set; }

        private uint PrivateUintProperty { get; set; }

        private nint PrivateNintProperty { get; set; }

        private nuint PrivateNuintProperty { get; set; }

        private long PrivateLongProperty { get; set; }

        private ulong PrivateUlongProperty { get; set; }

        private short PrivateShortProperty { get; set; }

        private ushort PrivateUshortProperty { get; set; }

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
