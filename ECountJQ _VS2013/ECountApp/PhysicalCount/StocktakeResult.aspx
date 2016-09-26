<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StocktakeResult.aspx.cs"
    Inherits="PhysicalCount_StocktakeResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />

    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbarFixed" EnableClientApi="true"
        OnButtonClicked="Toolbar1_ButtonClicked" Width = "1050px">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/save.gif"
                Text="保存"   />
            <%--<SCS:ToolbarButton CausesValidation="True" CommandName="import" ImageUrl="~/App_Themes/Images/Toolbar/import.gif"
                Text="����" />--%>
        </Items>
    </SCS:Toolbar>
    <div class="divContainerFixed">
        <asp:CheckBox ID="cbUnfulfilled" runat="server" AutoPostBack="True" OnCheckedChanged="cbUnfulfilled_CheckedChanged"
            Text="未录入" /></div>
    <asp:MultiView ID="mvResult" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewNoneCSMT" runat="server">
            <div class="divContainerFixed" style="height: 397px;">
                <asp:GridView ID="gvItems" runat="server" ShowHeader="False" OnRowCreated="gvItems_RowCreated"
                    OnRowDataBound="gvItems_RowDataBound" AutoGenerateColumns="False" DataKeyNames="DetailsID,SGMItemID,RDCItemID,GeneralItemID,RepairItemID"
                    OnPreRender="gvItems_PreRender">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="序号" />
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
                                <asp:GridView ID="gvStore" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtStore" Text='<%# Eval("Store" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvLine" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLine" Text='<%# Eval("Line", "{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvMachining" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMachining" Text='<%# Eval("Machining" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvQI" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSGMQI" Text='<%# Eval("SGMQI","{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvBlock" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSGMBlock" Text='<%# Eval("SGMBlock" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvStartCSN" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate><asp:TextBox ID="txtStartCSN" Text='<%# Eval("StartCSN") %>' runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvEndCSN" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate><asp:TextBox ID="txtEndCSN" Text='<%# Eval("EndCSN") %>' runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvWorkshop" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        
                        <asp:BoundField DataField="WorkshopCode" HeaderText="WorkshopCode" />
                                    </Columns>
                                </asp:GridView></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Segments" HeaderText="Segments" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtRDCAvailable" Text='<%# Eval("RDCAvailable" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtRDCQI" Text='<%# Eval("RDCQI" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtRDCBlock" Text='<%# Eval("RDCBlock" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtRepairAvailable" Text='<%# Eval("RepairAvailable" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtRepairQI" Text='<%# Eval("RepairQI" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtRepairBlock" Text='<%# Eval("RepairBlock" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RepairDUNS" HeaderText="RepairDUNS" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtGenAvailable" Text='<%# Eval("GeneralAvailable" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtGenQI" Text='<%# Eval("GeneralQI" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txtGenBlock" Text='<%# Eval("GeneralBlock" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Amount" DataFormatString="{0:0.####}"/>
                        <asp:BoundField DataField="WorkLocation" />
                        
                    </Columns>
                </asp:GridView>
            </div>
        </asp:View>
        <asp:View ID="viewCSMT" runat="server">
            <div class="divContainerFixed" style="height: 397px;">
                <asp:GridView ID="gvCSMTItems" runat="server" OnRowDataBound="gvCSMTItems_RowDataBound"
                    AutoGenerateColumns="False" DataKeyNames="DetailsID,RDCItemID,CSMTItemID" OnPreRender="gvCSMTItems_PreRender">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="序号" />
                        <asp:BoundField DataField="PartCode" HeaderText="零件号" />
                        <asp:BoundField DataField="PartPlantCode" HeaderText="工厂" />
                        <asp:BoundField DataField="PartChineseName" HeaderText="零件名称" />
                        <asp:BoundField DataField="DUNS" HeaderText="DUNS" />
                        <asp:BoundField DataField="CategoryName" HeaderText="物料类别" />
                        <asp:BoundField DataField="LevelName" HeaderText="循环级别" />
                        <asp:BoundField DataField="RequestNumber" HeaderText="申请单号" />
                        <asp:BoundField DataField="UserName" HeaderText="申请人" />
                        <asp:BoundField DataField="TypeName" HeaderText="盘点类别" />
                        <asp:BoundField DataField="CSMTDUNS" HeaderText="外协供应商DUNS" />
                        <asp:TemplateField HeaderText="外协区域Available">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCSMTAvailable" Text='<%# Eval("CSMTAvailable" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="外协区域QI">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCSMTQI" Text='<%# Eval("CSMTQI" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="外协区域Block">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCSMTBlock" Text='<%# Eval("CSMTBlock" ,"{0:0.####}") %>' runat="server"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:View>
        <asp:View ID="viewResult" runat="server">
            <div class="divContainerFixed" style="height: 416px;">
                <asp:GridView ID="gvResult" runat="server" ShowHeader="False" OnRowCreated="gvResult_RowCreated"
                    OnRowDataBound="gvResult_RowDataBound" AutoGenerateColumns="False" DataKeyNames="DetailsID,SGMItemID,RDCItemID,GeneralItemID,RepairItemID"
                    OnPreRender="gvResult_PreRender" OnDataBound="gvResult_DataBound">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="序号" />
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
                                <asp:GridView ID="gvStore" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID" BorderStyle="None">
                                    <Columns>
                                       <asp:BoundField DataField="Store" HeaderText="Store"  DataFormatString="{0:0.####}" />
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvLine" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID" BorderStyle="None">
                                    <Columns>
                                        <asp:BoundField DataField="Line" HeaderText="Line"   DataFormatString="{0:0.####}"/>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvMachining"  BorderStyle="None" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID">
                                    <Columns>
                                        <asp:BoundField DataField="Machining" HeaderText="Machining"  DataFormatString="{0:0.####}" />
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvQI" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID" BorderStyle="None">
                                    <Columns><asp:BoundField DataField="SGMQI" HeaderText="SGMQI"  DataFormatString="{0:0.####}" />
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvBlock" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID" BorderStyle="None">
                                    <Columns>
                                        <asp:BoundField DataField="SGMBlock" HeaderText="SGMBlock"  DataFormatString="{0:0.####}" />
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvStartCSN" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID" BorderStyle="None">
                                    <Columns>
                                    <asp:BoundField DataField="StartCSN" HeaderText="StartCSN" />
                                    </Columns>
                                </asp:GridView></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvEndCSN" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID" BorderStyle="None">
                                    <Columns><asp:BoundField DataField="EndCSN" HeaderText="EndCSN" />
                                    </Columns>
                                </asp:GridView></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvWorkshop" runat="server" ShowHeader="False" DataKeyNames="ItemDetailID" BorderStyle="None">
                                    <Columns>
                                        <asp:BoundField DataField="WorkshopCode" HeaderText="WorkshopCode" />
                                    </Columns>
                                </asp:GridView></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Segments" HeaderText="Segments" />
                        <asp:BoundField DataField="RDCAvailable" HeaderText="RDCAvailable"  DataFormatString="{0:0.####}" />
                        <asp:BoundField DataField="RDCQI" HeaderText="RDCQI"  DataFormatString="{0:0.####}" />
                        <asp:BoundField DataField="RDCBlock" HeaderText="RDCBlock"   DataFormatString="{0:0.####}"/>
                        <asp:BoundField DataField="RepairAvailable" HeaderText="RepairAvailable"  DataFormatString="{0:0.####}" />
                        <asp:BoundField DataField="RepairQI" HeaderText="RepairQI"   DataFormatString="{0:0.####}"/>
                        <asp:BoundField DataField="RepairBlock" HeaderText="RepairBlock"  DataFormatString="{0:0.####}" />
                        <asp:BoundField DataField="RepairDUNS" HeaderText="RepairDUNS" />
                        <asp:BoundField DataField="GeneralAvailable" HeaderText="GeneralAvailable" />
                        <asp:BoundField DataField="GeneralQI" HeaderText="GeneralQI" />
                        <asp:BoundField DataField="GeneralBlock" HeaderText="GeneralBlock" />
                        <asp:BoundField DataField="CSMTAvailable" HeaderText="CSMTAvailable"  DataFormatString="{0:0.####}" />
                        <asp:BoundField DataField="CSMTQI" HeaderText="CSMTQI"   DataFormatString="{0:0.####}"/>
                        <asp:BoundField DataField="CSMTBlock" HeaderText="CSMTBlock"   DataFormatString="{0:0.####}"/>
                        <asp:BoundField DataField="CSMTDUNS" HeaderText="CSMTDUNS"  DataFormatString="{0:0.####}" />
                        <asp:BoundField DataField="Amount"  DataFormatString="{0:0.####}"/>
                        <asp:BoundField DataField="WorkLocation" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:View>
    </asp:MultiView>
    </form>
</body>
</html>
