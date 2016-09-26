using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ECount.Infrustructure.Utilities
{
    public static class StringUtil
    {
        private const string EMAIL_ADDRESS_PATTERN =
            "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";

        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// </summary>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        public static Encoding GetFileEncoding(string srcFile)
        {
            // Ansi CodePage
            Encoding enc = Encoding.Default;
            // Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[5];
            using (FileStream file = new FileStream(srcFile, FileMode.Open))
            {
                file.Read(buffer, 0, 5);
                file.Close();
            }
            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                enc = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                enc = Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                enc = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                enc = Encoding.UTF7;
            return enc;
        }

        /// <summary>
        /// Opens a stream reader with the appropriate text encoding applied.
        /// </summary>
        /// <param name="srcFile"></param>
        public static StreamReader OpenStreamReaderWithEncoding(string srcFile)
        {
            Encoding enc = GetFileEncoding(srcFile);
            return new StreamReader(srcFile, enc);
        }

        public static string EnsureDateTimeValue(string itemfield, string format)
        {
            DateTime d;
            if (!TryParseDateTime(itemfield, out d))
            {
                d = new DateTime(1900, 1, 1);
            }
            else
            {
                if (d.Year < 1900)
                    d = new DateTime(1900, 1, 1);
            }

            return string.IsNullOrEmpty(format) ? d.ToString() : d.ToString(format);
        }

        public static bool TryParseDateTime(string itemfield, out DateTime value)
        {
            value = new DateTime(1900, 1, 1);
            if (string.IsNullOrEmpty(itemfield))
                return false;
            if (!DateTime.TryParse(itemfield, out value) && !TryParseDateItemSub1(itemfield, out value))
                return false;
            return true;
        }

        private static bool TryParseDateItemSub1(string itemfield, out DateTime value)
        {
            value = DateTime.MinValue;
            if (itemfield.Length == 8)
            {
                if (!TryParseDateTimeSub2(itemfield, out value))
                    return false;
            }
            else
                //throw new InvalidDataException(
                //    string.Format("fields value [{0}] can not parse as type of schema: name [{1}] index[{2}] type [{3}]", itemfield,
                //                  schemafield.Name, schemafield.Index, schemafield.Type), ex);
                return false;
            return true;
        }

        private static bool TryParseDateTimeSub2(string itemfield, out DateTime value)
        {
            value = DateTime.MinValue;
            try
            {
                int i1 = int.Parse(itemfield);
                int i2 = i1 / 10000;
                int i3 = i1 % 10000;
                if (i2 > 1990 && i2 < 2020)
                    // exception 1, itemfield may like yyyymmdd
                    value = new DateTime(i2, i3 / 100, i3 % 100);
                else if (i3 > 1990 && i3 < 2020)
                    // exception 2, itemfield may like mmddyyyy
                    value = new DateTime(i3, i2 / 100, i2 % 100);
                else
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// try to fix the unnormal string to right value
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryParseInt32(string source, out int value)
        {
            value = 0;
            if (string.IsNullOrEmpty(source) || source.Trim().Length == 0)
                return false;

            if (!int.TryParse(source, out value))
            {
                const string pattern = @"^\s*((0+)|(0*(\d+)))\s*$";
                Match m = Regex.Match(source, pattern);
                if (m.Length < 2)
                    return false;
                value = int.Parse(m.Groups[1].Value);
            }
            return true;
        }

        /// <summary>
        /// try to fix the unnormal string to right value
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryParseDouble(string source, out double value)
        {
            value = 0;
            if (string.IsNullOrEmpty(source) || source.Trim().Length == 0)
                return false;

            if (!double.TryParse(source, out value))
            {
                const string pattern = @"^\s*((0+(\.\d+){0,1})|(0*(\d+(\.\d+){0,1})))\s*$";
                Match m = Regex.Match(source, pattern);
                if (m.Length < 2)
                    return false;
                value = double.Parse(m.Groups[1].Value);
            }
            return true;
        }


        /// <summary>
        /// in case the string mixed with multibytes.
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static int GetByteLength(string Text, Encoding enc)
        {
            int len = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                byte[] byte_len = enc.GetBytes(Text.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 2; // multi byte
                else
                    len += 1; // ascii byte
            }

            return len;
        }

        /// <summary>
        /// get fixed with multibytes substring.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enc"></param>
        /// <param name="startindex"></param>
        /// <returns></returns>
        public static string GetSubByte(string text, Encoding enc, int startindex)
        {
            return GetSubByte(text, enc, startindex, GetByteLength(text, enc) - startindex);
        }

        /// <summary>
        /// get fixed with multibytes substring.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enc"></param>
        /// <param name="startindex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSubByte(string text, Encoding enc, int startindex, int length)
        {
            byte[] bytes = enc.GetBytes(text);
            string returnvalue = null;
            try
            {
                returnvalue = enc.GetString(bytes, startindex, length);
            }
            catch { }
            return returnvalue;
        }

        public static bool TryGetSubByte(string text, Encoding enc, int startindex, int length, out string bytestr)
        {
            byte[] bytes = enc.GetBytes(text);
            bytestr = null;
            try
            {
                bytestr = enc.GetString(bytes, startindex, length);
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// 去除sql中无效字符
        /// </summary>
        /// <param name="sqlCondition"></param>
        /// <returns></returns>
        public static string ReplaceSqlInvalidWords(string sqlCondition)
        {
            if (string.IsNullOrEmpty(sqlCondition)) return "";

            //过滤非法字符 wangzhen 2007-9-26
            sqlCondition = sqlCondition.Replace("[", "[[]");
            sqlCondition = sqlCondition.Replace("]", "[]]");
            sqlCondition = sqlCondition.Replace("'", "");

            return sqlCondition;
        }

        public static string GetDelimitedReadyString(string s, char delimiter)
        {
            if (string.IsNullOrEmpty(s))
                return delimiter.ToString();
            string s1 = s;
            bool shouldAddQuote = false;
            // for the value start with 0, in excel which should be escaped with ' starting to avoid treated as integer.
            int i1;
            if (s1.StartsWith("0") && int.TryParse(s1, out i1))
                s1 = "\t" + s1;
            if (s1.Contains("\n"))
            {
                shouldAddQuote = true;
            }
            // restore the '\n'
            if (s1.Contains("\\n"))
            {
                shouldAddQuote = true;
                s1 = s1.Replace("\\n", "\n");
            }
            // restore the quote "
            if (-1 != s1.IndexOf('"'))
            {
                shouldAddQuote = true;
                s1 = s1.Replace("\"", "\"\"");
            }
            // comma exists
            if (-1 != s1.IndexOf(delimiter))
                shouldAddQuote = true;

            if (shouldAddQuote)
                s1 = string.Concat("\"", s1, "\"");
            s1 += delimiter.ToString();

            return s1;
        }

        public static bool TryParseGuid(string s, out Guid result)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            Regex format = new Regex(
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");

            Match match = format.Match(s);

            if (match.Success)
            {
                result = new Guid(s);
                return true;
            }
            result = Guid.Empty;
            return false;
        }

        public static string TruncateString(string s, int length)
        {
            if (s.Length <= length)
                return s;

            return s.Substring(0, length - 3) + "...";
        }
    }
}