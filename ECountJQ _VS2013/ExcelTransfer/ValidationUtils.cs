using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using ECount.Infrustructure.Utilities;

namespace ECount.Infrustructure.Data.Integration
{
    internal static class ValidationUtils
    {
        internal const string DomainInvalidMessage = "列'{0}'的值[{1}]应当位于({2})中. ";
        internal const string NotContainsCharactersInvalidMessage = "列'{0}'的值[{1}]不能包含以下任意字符({2}). ";
        internal const string NotNullInvalidMessage = "列'{0}'的值不允许为空. ";
        internal const string RangeInvalidMessage = "列'{0}'的值[{1}]应当位于 ({2}) - ({3})之间. ";
        internal const string RegexInvalidMessage = "列'{0}'的值[{1}]不能满足正则表达式({2}). ";
        internal const string StringLengthInvalidMessage = "列'{0}'的值[{1}]的字符长度应当为 ({2}). ";
        internal const string StringLengthRangeInvalidMessage = "列'{0}'的值[{1}]的字符长度应当位于 ({2} - {3})之间. ";
        internal const string TypeConversionInvalidMessage = "列'{0}'的值[{1}]不能转换为({2}). ";

        public static void DoStringLengthValidate(string objectToValidate,
                                                  string key,
                                                  int minLength,
                                                  int maxLength,
                                                  ValidationResults validationResults)
        {
            if (objectToValidate == null || objectToValidate.Length < minLength || objectToValidate.Length > maxLength)
                if (minLength == maxLength)
                    LogValidationResult(validationResults,
                                        GetMessage(StringLengthInvalidMessage, key, objectToValidate, minLength), key);
                else
                    LogValidationResult(validationResults,
                                        GetMessage(StringLengthRangeInvalidMessage, key, objectToValidate, minLength, maxLength), key);
        }

        public static void DoNotNullValidate(object objectToValidate,
                                             string key,
                                             ValidationResults validationResults)
        {
            if ((null == objectToValidate) || objectToValidate.ToString().Trim().Length == 0)
                LogValidationResult(validationResults, GetMessage(NotNullInvalidMessage, key), key);
        }

        public static void DoRangeValidate<T>(T objectToValidate,
                                              string key,
                                              T min, T max,
                                              ValidationResults validationResults) where T : IComparable
        {
            bool isObjectToValidateNull;
            if (!objectToValidate.GetType().IsValueType)
                isObjectToValidateNull = objectToValidate == null;
            else
                isObjectToValidateNull = false;

            bool logError = !isObjectToValidateNull && !IsInRange(objectToValidate, min, max);

            if (isObjectToValidateNull || logError)
                LogValidationResult(validationResults,
                                    GetMessage(RangeInvalidMessage, key, objectToValidate, min, max), key);
        }

        public static void DoRegexValidate(string objectToValidate,
                                           string key,
                                           string pattern,
                                           ValidationResults validationResults)
        {
            bool logError = false;
            bool isObjectToValidateNull = objectToValidate == null;

            if (!isObjectToValidateNull)
            {
                Regex regex = new Regex(pattern);
                logError = !regex.IsMatch(objectToValidate);
            }

            if (isObjectToValidateNull || logError)
                LogValidationResult(validationResults, GetMessage(RegexInvalidMessage, key, objectToValidate, pattern), key);
        }

        public static void DoDomainValidate<T>(T objectToValidate, string key, IEnumerable<T> domain,
                                               ValidationResults validationResults)
        {
            bool logError = true;
            bool isObjectToValidateNull;
            if (!objectToValidate.GetType().IsValueType)
                isObjectToValidateNull = objectToValidate == null;
            else
                isObjectToValidateNull = false;

            if (!isObjectToValidateNull)
                foreach (T element in domain)
                {
                    if (element.Equals(objectToValidate))
                    {
                        logError = false;
                        break;
                    }
                }

            if (isObjectToValidateNull || logError)
            {
                StringBuilder sb = new StringBuilder();
                foreach (T element in domain)
                {
                    sb.Append(element).Append(",");
                }
                LogValidationResult(validationResults,
                                    GetMessage(DomainInvalidMessage, key, objectToValidate, sb.ToString().TrimEnd(',')),
                                    key);
            }
        }

        public static void DoTypeConversionValidate(string objectToValidate, string key, Type targetType,
                                                    ValidationResults validationResults)
        {
            bool logError = false;
            bool isObjectToValidateNull = objectToValidate == null;

            if (!isObjectToValidateNull)
                if (objectToValidate.Length == 0 && IsTheTargetTypeAValueTypeDifferentFromString(targetType))
                    logError = true;
                else
                {
                    object v;
                    logError = !TryTypeConversion(objectToValidate, targetType, out v);

                }
            if (isObjectToValidateNull || logError)
            {
                string typename = targetType.ToString();
                if (targetType == typeof(int))
                    typename = "整数";
                else if (targetType == typeof(decimal))
                    typename = "小数";
                else if (targetType == typeof(DateTime))
                    typename = "日期";
                LogValidationResult(validationResults,
                                    GetMessage(TypeConversionInvalidMessage, key, objectToValidate, typename),
                                    key);
                return;
            }
        }

        public static bool TryTypeConversion<T>(string original, out T output)
        {
            object o;
            bool returnvalue = TryTypeConversion(original, typeof(T), out o);
            if (returnvalue)
                output = (T)o;
            else
                output = default(T);

            return returnvalue;
        }

        public static bool TryTypeConversion(string original, Type targetType, out object output)
        {
            try
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(targetType);
                output = typeConverter.ConvertFromString(null, CultureInfo.CurrentCulture, original);
                if (output == null)
                    return false;
            }
            catch (Exception)
            {
                output = null;
                return false;
            }
            return true;
        }

        public static void DoNotContainsCharactersValidate(string objectToValidate, string key, string characterSet,
                                                           ValidationResults validationResults)
        {
            bool logError = false;
            bool isObjectToValidateNull = objectToValidate == null;

            if (!isObjectToValidateNull)
            {
                List<char> characterSetArray = new List<char>(characterSet);
                bool containsCharacterFromSet = false;
                foreach (char ch in objectToValidate)
                {
                    if (characterSetArray.Contains(ch))
                    {
                        containsCharacterFromSet = true;
                        break;
                    }
                }
                logError = containsCharacterFromSet;
            }

            if (isObjectToValidateNull || logError)
                LogValidationResult(validationResults,
                                    GetMessage(NotContainsCharactersInvalidMessage, key, objectToValidate, characterSet),
                                    key);
        }

        internal static void LogValidationResult(ValidationResults validationResults, string message, string key)
        {
            validationResults.AddResult(new ValidationResult(message, key));
        }

        private static string GetMessage(string messageTemplate, params object[] args)
        {
            return string.Format(CultureInfo.CurrentUICulture,
                                 messageTemplate,
                                 args);
        }

        private static bool IsInRange<T>(T target, T min, T max) where T : IComparable
        {
            int lowerBoundComparison = min.CompareTo(target);
            if (lowerBoundComparison > 0)
                return false;
            int upperBoundComparison = max.CompareTo(target);
            if (upperBoundComparison < 0)
                return false;

            return true;
        }

        internal static bool IsTheTargetTypeAValueTypeDifferentFromString(Type targetType)
        {
            TypeCode targetTypeCode = Type.GetTypeCode(targetType);
            return targetTypeCode != TypeCode.Object && targetTypeCode != TypeCode.String;
        }

        internal static DataSchema GetDataSchemaFromFile(string schemapath)
        {
            string filepath = MiscUtil.ResolveFilePath(schemapath);

            if (string.IsNullOrEmpty(filepath))
                throw new ArgumentNullException(string.Format("argument [schemapath]'{0}' is not valid!", schemapath));

            XmlSerializer serializer = new XmlSerializer(typeof(DataSchema));

            /* If the XML document has been altered with unknown 
            nodes or attributes, handle them with the 
            UnknownNode and UnknownAttribute events.*/
            serializer.UnknownNode += delegate(object sender, XmlNodeEventArgs e) { throw new ArgumentOutOfRangeException(string.Format("Unknown Node:{0}\t{1}", e.Name, e.Text)); };
            serializer.UnknownAttribute += delegate(object sender, XmlAttributeEventArgs e)
            {
                XmlAttribute attr = e.Attr;
                throw new ArgumentOutOfRangeException(string.Format("Unknown attribute {0}='{1}'", attr.Name, attr.Value));
            };

            // A FileStream is needed to read the XML document.
            using (Stream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // Use the Deserialize method to restore the object's state with data from the XML document. 
                DataSchema dataSchema = (DataSchema)serializer.Deserialize(fs);
                return dataSchema;
            }
        }
    }
}