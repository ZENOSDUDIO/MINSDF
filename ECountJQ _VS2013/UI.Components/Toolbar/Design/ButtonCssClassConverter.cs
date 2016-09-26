using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace SCS.Web.UI.WebControls.Design
{
	public class ButtonCssClassConverter : ExpandableObjectConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
		{
			if (t == typeof(string))
				return true;

			return base.CanConvertFrom(context, t);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) 
		{
			if (destType == typeof(InstanceDescriptor) || destType == typeof(string)) 
			{
				return true;
			}
			return base.CanConvertTo(context, destType);
		}	
		
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
		{
			if (value is string)
			{
				string styleClassList = (string)value;
					
				try
				{
					string[] classes = styleClassList.Split(info.TextInfo.ListSeparator[0]);
					string[] args = new string[4];

                    for (int i = 0; i < classes.Length; i++)
                    {
                        args[i] = classes[i].Trim();
                    }

					return new ButtonClasses(args[0], args[1], args[2], args[3]);
				}
				catch
				{
					return new ButtonClasses();
				}				
			}
			return base.ConvertFrom(context,info,value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo info, object value, Type destType )
		{
			if (destType == typeof(string))
			{
				ButtonClasses style = (ButtonClasses)value;
				
				string text = string.Empty;

				if (!string.IsNullOrEmpty(style.CssClassEnabled))
					text += string.Format("{0} {1}", 
						info.TextInfo.ListSeparator[0], style.CssClassEnabled);

                if (!string.IsNullOrEmpty(style.CssClassDisabled))
                    text += string.Format("{0} {1}",
                        info.TextInfo.ListSeparator[0], style.CssClassDisabled);

                if (!string.IsNullOrEmpty(style.CssClassSelected))
                    text += string.Format("{0} {1}",
                        info.TextInfo.ListSeparator[0], style.CssClassSelected);
                							
				if (text.Length > 0)
					text = text.Substring(2);

				return text;
			}
			else if (destType == typeof(InstanceDescriptor)) 
			{
				Type[] types = new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) };
				ConstructorInfo constructorInfo = typeof(ButtonClasses).GetConstructor(types);
				
				ButtonClasses style = (ButtonClasses)value;
				object[] args = new object[] { 
                    style.CssClassEnabled, 
                    style.CssClassDisabled, 
                    style.CssClassSelected };

				InstanceDescriptor descriptor = new InstanceDescriptor(constructorInfo, args, true);

				return descriptor;
			}
			return base.ConvertTo(context, info, value, destType);
		}
	}
}



