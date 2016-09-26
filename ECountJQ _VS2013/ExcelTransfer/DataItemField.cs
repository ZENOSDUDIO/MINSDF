namespace ECount.Infrustructure.Data.Integration
{
    public class DataItemField : DataSchemaField
    {
        private bool _hasChanged;
        private object _value;

        public DataItemField(DataSchemaField field)
        {
            Name = field.Name;
            Source = field.Source;
            Destination = field.Destination;
            Index = field.Index;
            Type = field.Type;
            Skip = field.Skip;
            AnyAttr = field.AnyAttr;
            _value = null;
        }

        public object Value
        {
            get { return _value; }
            set
            {
                if (value != _value)
                {
                    if (_value != default(object))
                        _hasChanged = true;
                    _value = value;
                }
            }
        }

        public bool HasChanged
        {
            get { return _hasChanged; }
        }

        public void DoValidate(ValidationResults validationResults)
        {
            // have not execute validation
            if (!DoValidate(_value, validationResults))
                // does it necessary here? alex hu@20080429
                if (Type != typeof(string).ToString() && _value.ToString().Length == 0)
                    _value = null;
        }
    }
}