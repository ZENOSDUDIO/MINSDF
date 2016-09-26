<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StorageImport.aspx.cs" Inherits="PhysicalCount_StorageImport" %>

<%@ Register src="../Common/UCFileUpload.ascx" tagname="UCFileUpload" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr><td>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ImportTemplate/StorageImport.xls">����ģ������</asp:HyperLink></td></tr>
        <tr>
            <td>
            ֪ͨ���ţ�<asp:TextBox ID="txtNoticeNo" runat="server"></asp:TextBox><br />
        <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
        
            </td>
        </tr>
        <tr>
            <td>
            <div  class="divContainer" style="height:300px">
                <asp:GridView ID="gvStoreage" runat="server" AutoGenerateColumns="False" 
                    onprerender="gvStoreage_PreRender"  >
                    <Columns>
                         <asp:BoundField DataField="No" HeaderText="���" >                 
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                        <asp:BoundField DataField="PartNo" HeaderText="�����" >
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                        <asp:BoundField DataField="PlantCode" HeaderText="����" >
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                        <asp:BoundField DataField="StoreLocation" HeaderText="����ϵͳ�洢����" >
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                        <asp:BoundField DataField="Available" HeaderText="ϵͳAvailable" >
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                        <asp:BoundField DataField="QI" HeaderText="ϵͳQI" >
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                        <asp:BoundField DataField="Block" HeaderText="ϵͳBlock" >
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="����" DataFormatString="{0:c}" >
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                    </Columns>
                </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

