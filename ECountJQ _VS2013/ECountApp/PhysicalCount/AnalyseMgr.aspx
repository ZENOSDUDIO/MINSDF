<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnalyseMgr.aspx.cs" Inherits="PhysicalCount_AnalyseMgr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
        OnButtonClicked="Toolbar1_ButtonClicked">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/save.gif"
                Text="保存" />
            <%--<SCS:ToolbarButton CausesValidation="True" CommandName="import" ImageUrl="~/App_Themes/Images/Toolbar/import.gif"
                Text="导入" />--%>
        </Items>
    </SCS:Toolbar>
    <table>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer" style="height:400px;">
                    <asp:GridView ID="gvItems" runat="server" ShowHeader="False" OnRowCreated="gvItems_RowCreated"
                        OnRowDataBound="gvItems_RowDataBound" AutoGenerateColumns="False" DataKeyNames="DetailsID,SGMItemID,RDCItemID,GeneralItemID,CSMTItemID,RepairItemID">
                        <Columns>
                            <asp:BoundField DataField="PartCode" HeaderText="PartCode" />
                            <asp:BoundField DataField="PartPlantCode" HeaderText="PartPlantCode" />
                            <asp:BoundField DataField="PartChineseName" HeaderText="PartChineseName" />
                            <asp:BoundField DataField="DUNS" HeaderText="DUNS" />
                            <asp:BoundField DataField="CategoryName" HeaderText="CategoryName" />
                            <asp:BoundField DataField="LevelName" HeaderText="LevelName" />
                            <asp:BoundField DataField="RequestNumber" HeaderText="RequestNumber" />
                            <asp:BoundField DataField="UserName" HeaderText="UserName" />
                            <asp:BoundField DataField="TypeName" HeaderText="TypeName" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSGMAvailable" Text='<%# Eval("SGMAvailableAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSGMQI" Text='<%# Eval("SGMQIAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSGMBlock" Text='<%# Eval("SGMBlockAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("StartCSN") %>' ID="lblStartCSN" runat="server"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("EndCSN") %>' ID="lblEndCSN" runat="server"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Workshops" HeaderText="Workshops" />
                            <asp:BoundField DataField="Segments" HeaderText="Segments" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRDCAvailable" Text='<%# Eval("RDCAvailableAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRDCQI" Text='<%# Eval("RDCQIAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRDCBlock" Text='<%# Eval("RDCBlockAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRepairAvailable" Text='<%# Eval("RepairAvailableAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRepairQI" Text='<%# Eval("RepairQIAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRepairBlock" Text='<%# Eval("RepairBlockAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RepairDUNS" HeaderText="RepairDUNS" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCSMTAvailable" Text='<%# Eval("CSMTAvailableAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCSMTQI" Text='<%# Eval("CSMTQIAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCSMTBlock" Text='<%# Eval("CSMTBlockAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CSMTDUNS" HeaderText="CSMTDUNS" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGenAvailable" Text='<%# Eval("GenAvailableAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGenQI" Text='<%# Eval("GenQIAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGenBlock" Text='<%# Eval("GenBlockAdjust") %>' runat="server"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField />
                            <asp:BoundField DataField="WorkLocation" />
                            <asp:BoundField Visible="False" DataField="SGMItemID" />
                            <asp:BoundField Visible="False" DataField="RDCItemID" />
                            <asp:BoundField Visible="False" DataField="RepairItemID" />
                            <asp:BoundField Visible="False" DataField="CSMTItemID" />
                            <asp:BoundField Visible="False" DataField="GeneralItemID" />
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
