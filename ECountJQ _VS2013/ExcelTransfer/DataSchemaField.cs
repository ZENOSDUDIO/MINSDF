using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ECount.Infrustructure.Data.Integration
{
    public class DataSchemaField
    {
        internal Type _type = typeof(string);
        internal List<string> _validators = new List<string>();

        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr;

        [XmlAttribute("destination")]
        public string Destination;

        [XmlAttribute("index")]
        public int Index;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("skip")]
        public bool Skip;

        [XmlAttribute("source")]
        public string Source;

        [XmlAttribute("type")]
        public string Type;

        public bool BuildValidators()
        {
            // type conversion validation
            if (!Type.Equals("System.String") && !_validators.Contains("DoTypeConversionValidate"))
            {
                // TODO: by alex hu@20080305 need refactoring
                if (Type.Equals(typeof(int).ToString()))
                    _type = typeof(int);
                else if (Type.Equals(typeof(decimal).ToString()))
                    _type = typeof(decimal);
                else if (Type.Equals(typeof(DateTime).ToString()))
                    _type = typeof(DateTime);

                _validators.Add("DoTypeConversionValidate");
            }

            if (AnyAttr == null)
                return false;
            foreach (XmlAttribute attr in AnyAttr)
            {
                switch (attr.Name)
                {
                    case "length":
                    case "minlength":
                    case "maxlength":
                        // string length validation
                        if (!_validators.Contains("DoStringLengthValidate"))
                            _validators.Add("DoStringLengthValidate");
                        break;
                    case "required":
                        // not null validation, make been the first one
                        if (bool.Parse(attr.Value) && !_validators.Contains("DoNotNullValidate"))
                            _validators.Insert(0, "DoNotNullValidate");
                        break;
                    case "min":
                    case "max":
                        // range validation
                        if (!_validators.Contains("DoRangeValidate"))
                            _validators.Add("DoRangeValidate");
                        break;
                    case "pattern":
                        // regular expression validation
                        if (!_validators.Contains("DoRegexValidate"))
                            _validators.Add("DoRegexValidate");
                        break;
                    case "domain":
                        // domain validation
                        if (!_validators.Contains("DoDomainValidate"))
                            _validators.Add("DoDomainValidate");
                        break;
                    case "notcontains":
                        // not contains character validation
                        if (!_validators.Contains("DoNotContainsCharactersValidate"))
                            _validators.Add("DoNotContainsCharactersValidate");
                        break;
                }
            }
            return _validators.Count != 0;
        }

        public bool DoValidate(object value, ValidationResults validationResults)
        {
            if (_validators.Count == 0 && !BuildValidators())
                return false;

            if (_validators.Contains("DoNotNullValidate"))
            {
                ValidationUtils.DoNotNullValidate(value, Name, validationResults);
                // to avoid exception raised below, return if invalid
                if (!validationResults.IsValid)
                    return false;
            }
            else if (value == null || value.ToString().Length == 0)
                return false; // null does not need other validations.

            if (_validators.Contains("DoTypeConversionValidate"))
            {
                // do type conversion validation
                ValidationUtils.DoTypeConversionValidate(value.ToString(), Name, _type, validationResults);

                // to avoid convertion exception raised below, return if invalid
                if (!validationResults.IsValid)
                    return false;
            }

            foreach (string validator in _validators)
            {
                // the requirevalidator is the first one matched.
                switch (validator)
                {
                    case "DoStringLengthValidate":
                        int minlength, maxlength;
                        if (!int.TryParse(GetAnyAttributeValue("minlength"), out minlength) &&
                            !int.TryParse(GetAnyAttributeValue("length"), out minlength))
                            minlength = 0;
                        if (!int.TryParse(GetAnyAttributeValue("maxlength"), out maxlength) &&
                            !int.TryParse(GetAnyAttributeValue("length"), out maxlength))
                            maxlength = int.MaxValue;
                        ValidationUtils.DoStringLengthValidate(value.ToString(), Name, minlength, maxlength, validationResults);
                        break;
                    case "DoRangeValidate":
                        // TODO: by alex hu@20080305 need more generic
                        // for int
                        if (_type == typeof(int))
                        {
                            int min, max;
                            if (!int.TryParse(GetAnyAttributeValue("min"), out min))
                                min = int.MinValue;
                            if (!int.TryParse(GetAnyAttributeValue("max"), out max))
                                max = int.MaxValue;

                            ValidationUtils.DoRangeValidate(Convert.ToInt32(value), Name, min, max, validationResults);
                        }
                        break;
                    case "DoRegexValidate":
                        ValidationUtils.DoRegexValidate(value.ToString(), Name, GetAnyAttributeValue("pattern"), validationResults);
                        break;
                    case "DoDomainValidate":
                        // TODO: by alex hu@20080305 need more generic
                        string[] domain = GetAnyAttributeValue("domain").Split('|');
                        ValidationUtils.DoDomainValidate(value.ToString(), Name, domain, validationResults);
                        break;
                    case "DoNotContainsCharactersValidate":
                        ValidationUtils.DoNotContainsCharactersValidate(value.ToString(), Name, GetAnyAttributeValue("notcontains"),
                                                                        validationResults);
                        break;
                }
            }
            return true;
        }

        public string GetAnyAttributeValue(string name)
        {
            if (string.IsNullOrEmpty(name) || AnyAttr == null)
                return null;
            return GetAnyAttributeValue(AnyAttr, name);
        }

        internal static string GetAnyAttributeValue(XmlAttribute[] attrs, string name)
        {
            if (string.IsNullOrEmpty(name) || attrs == null)
                return null;
            XmlAttribute attr = Array.Find(attrs, delegate(XmlAttribute a)
            {
                return
                    a.Name.Equals(name,
                                  StringComparison.
                                    InvariantCultureIgnoreCase);
            });
            if (attr == null)
                return null;
            return attr.Value;
        }

        public bool SetAnyAttributeValue(string name, string value)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return false;
            AnyAttr = SetAnyAttributeValue(AnyAttr, name, value);
            return true;
        }

        internal static XmlAttribute[] SetAnyAttributeValue(XmlAttribute[] attrs, string name, string value)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return attrs;

            if (attrs == null)
                attrs = new XmlAttribute[0];

            XmlAttribute attr = Array.Find(attrs, delegate(XmlAttribute a)
            {
                return
                    a.Name.Equals(name,
                                  StringComparison.
                                    InvariantCultureIgnoreCase);
            });
            if (attr == null)
            {
                XmlAttribute[] newAttrs = new XmlAttribute[attrs.Length + 1];
                attrs.CopyTo(newAttrs, 0);
                newAttrs[attrs.Length] = new XmlDocument().CreateAttribute(name);
                attr = newAttrs[attrs.Length];
                attrs = newAttrs;
            }
            attr.Value = value;
            return attrs;
        }
    }

    public class DataSchemaFieldComparer : IComparer<DataSchemaField>, IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            return Compare((DataSchemaField)x, (DataSchemaField)y);
        }

        #endregion

        #region IComparer<DataSchemaField> Members

        public int Compare(DataSchemaField x, DataSchemaField y)
        {
            return x.Index.CompareTo(y.Index);
        }

        #endregion
    }
}