namespace MmcSerializer.Tests.Models.Strings
{
    public class StringsOnly
    {
        public string _stringField;

        public string PropertyString;

        private string _privateStringField;

        private string PrivateStringProperty;

        public override bool Equals(object? obj)
        {
            return ClassEquals.AreClassesEqualFromFieldsAndProperties(this, obj);
        }
    }
}
