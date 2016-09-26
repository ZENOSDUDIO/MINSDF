<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PartEdit.aspx.cs" Inherits="BizDataMaintain_PartEdit" %>

<%@ Register src="UserControl/PartDetails.ascx" tagname="PartDetails" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <fieldset class="Edit">
        <legend class="mainTitle">零件明细</legend>
        <div>
            <uc1:PartDetails ID="PartDetails1" runat="server" />
        </div>
    </fieldset>
</asp:Content>

