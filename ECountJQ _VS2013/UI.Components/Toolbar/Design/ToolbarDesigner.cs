/*
Copyright (c) 2007 Bill Davidsen (wdavidsen@yahoo.com)

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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
//using EnvDTE;

namespace SCS.Web.UI.WebControls.Design
{
    public class ToolbarDesigner : ControlDesigner
    {
        private DesignerVerbCollection designerVerbs;

        public override void Initialize(IComponent component)
        {
            if (!(component is Toolbar))
            {
                throw new ArgumentException("Component must be a Toolbar control.", "component");
            }
            base.Initialize(component);
        }
        
        public override string GetDesignTimeHtml()
        {
            string html = string.Empty;

            Toolbar toolbar = (Toolbar)Component;

            if (toolbar.Items.Count == 0)
            {
                html = this.GetEmptyDesignTimeHtml();
            }
            else
            {
                try
                {
                    // setup toolbar
                    this.SetupToolbar(toolbar);

                    // render html
                    StringWriter stringWriter = new StringWriter();
                    HtmlTextWriter writer = new HtmlTextWriter(stringWriter);

                    toolbar.WriteToolbar(writer);
                    html += stringWriter.ToString();

                    // for debugging purposes
                    //html += "<br/><br/>" + Toolbar.DesignTimeBasePath;
                }
                catch (Exception ex)
                {
                    html = this.GetErrorDesignTimeHtml(ex);
                }
            }

            return html;
        }

        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            return "Error: " + base.GetErrorDesignTimeHtml(e);
        }
        protected override string GetEmptyDesignTimeHtml()
        {
            string html = string.Empty;

            try
            {
                html = @"<div class=""toolbar"">
	                        <span class=""button button_enabled""><a href=""#"">Item 1</a></span>
                            <span class=""button button_enabled""><a href=""#"">Item 2</a></span>  
                            <span class=""button button_enabled""><a href=""#"">Item 3</a></span>     
                        </div>";
            }
            catch (Exception ex)
            {
                html = this.GetErrorDesignTimeHtml(ex);
            }

            return html;
        }

        private void SetupToolbar(Toolbar toolbar)
        {
            
        }
    }
}
