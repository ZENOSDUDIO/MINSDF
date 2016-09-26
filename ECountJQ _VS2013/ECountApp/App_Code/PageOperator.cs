using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using AjaxControlToolkit;

/// <summary>
/// Summary description for PageOperator
/// </summary>
public class PageOperator
{
    private PageOperator()
    {
    }


    /// <summary>
    /// 打开指定页面
    /// </summary>
    /// <param name="control"></param>
    /// <param name="url"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    public static void OpenWindow(Control control, string url, string height, string width)
    {
        string script = string.Format("window.showModalDialog('{0}','','height={1}, width={2}, top=20, left=20, toolbar=no, menubar=no, scrollbars=yes, resizable=yes,location=no, status=no');", url, height, width);
        ToolkitScriptManager.RegisterStartupScript(control, typeof(System.Web.UI.Page), "open", script, true);
    }

    public static void Alert(Control control, string message)
    {
        new Page().ClientScript.RegisterStartupScript(new Page().GetType(), "AlertMessage", string.Format("alert('{0}' );", message), true);
        //System.Web.UI.ScriptManager.RegisterStartupScript(control, typeof(System.Web.UI.Page), "提示", string.Format("alert('{0}');", message), true);
    }


    /// <summary>
    /// get checkListValue
    /// </summary>
    /// <param name="cbl"></param>
    /// <returns></returns>
    public static string GetCheckBoxListValue(CheckBoxList cbl)
    {
        string strValue = string.Empty;

        for (int i = 0; i < cbl.Items.Count; i++)
        {
            if (cbl.Items.FindByValue(cbl.Items[i].Value).Selected)
            {
                strValue += cbl.Items[i].Value;
                strValue += ",";
            }
        }
        if (strValue.Length == 0)
        {
            return string.Empty;
        }
        strValue = strValue.Substring(0, strValue.Length - 1);
        return strValue;
    }


   
}
