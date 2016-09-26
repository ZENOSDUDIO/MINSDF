<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="vertical-align: middle; text-align: center; width: 100%; height: 100%;
        font-weight: 700; color: #0066FF;">
        <table>
            <tr>
                <td>
                    <img alt="" src="App_Themes/Images/error.gif" />
                </td>
                <td>
                    <span class="style1">出错啦！请联系系统管理员或稍后再试</span>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
        }
    </style>
</asp:Content>
