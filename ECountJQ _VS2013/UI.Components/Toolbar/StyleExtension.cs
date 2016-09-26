/*
Copyright (c) 2009 Bill Davidsen (wdavidsen@yahoo.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SCS.Web.UI.WebControls
{
    public static class StyleExtension
    {
        public static string ToStyleString(this Style style)
        {
            StringBuilder sb = new StringBuilder(256);
            if (style == null)
            {
                return "";
            }

            Color c;

            c = style.ForeColor;
            if (!c.IsEmpty)
            {
                sb.Append("color:");
                sb.Append(ColorTranslator.ToHtml(c));
                sb.Append(";");
            }
            else
            {
                sb.Append("color:black;");
            }

            FontInfo fi = style.Font;
            string s;

            s = fi.Name;
            if (s.Length != 0)
            {
                sb.Append("font-family:'");
                sb.Append(s);
                sb.Append("';");
            }
            if (fi.Bold)
            {
                sb.Append("font-weight:bold;");
            }
            else
            {
                sb.Append("font-weight:normal;");
            }

            if (fi.Italic)
            {
                sb.Append("font-style:italic;");
            }
            else
            {
                sb.Append("font-style:normal;");
            }

            s = String.Empty;
            if (fi.Underline)
                s += "underline";

            if (fi.Strikeout)
                s += " line-through";

            if (fi.Overline)
                s += " overline";

            if (s.Length != 0)
            {
                sb.Append("text-decoration:");
                sb.Append(s);
                sb.Append(';');
            }
            else
            {
                sb.Append("text-decoration:none;");
            }

            FontUnit fu = fi.Size;
            if (fu.IsEmpty == false)
            {
                sb.Append("font-size:");
                sb.Append(fu.ToString(CultureInfo.InvariantCulture));
                sb.Append(';');
            }

            return sb.ToString();
        }
    }
}
