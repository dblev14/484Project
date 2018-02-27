<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddVendor.aspx.cs" Inherits="AddVendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style11 {
            left: 260px;
            top: 105px;
            width: 225px;
        }
        .auto-style12 {
            z-index: 1;
            top: 60px;
            position: absolute;
            left: 260px;
            width: 222px;
        }
        .auto-style13 {
            z-index: 1;
            left: 100px;
            top: 135px;
            position: absolute;
        }
        .auto-style14 {
            z-index: 1;
            top: 185px;
            position: absolute;
            left: 260px;
            width: 224px;
        }
        .auto-style15 {
            left: 257px;
            top: 323px;
        }
        .auto-style16 {
            left: 265px;
            top: 240px;
            width: 227px;
        }

        .auto-style17 {
            left: 259px;
            top: 379px;
            width: 173px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtVenName" runat="server" style="left 175px; " CssClass="auto-style12"></asp:TextBox>
    <asp:FileUpload ID="uploadVendorImage" runat="server" CssClass="auto-style16" />
    <asp:TextBox ID="txtVenURL" runat="server" style="left 195px; " CssClass="auto-style14"></asp:TextBox>
    <asp:Label ID="lblAddVendor" runat="server" Text="Add/Edit Gift Vendor" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <asp:Label ID="lblVenName" runat="server" style="z-index: 1; left: 95px; top: 68px; position: absolute; margin-bottom: 30px" Text="Vendor Name: "></asp:Label>
    <asp:Label ID="lblVenDesc" runat="server" Text="Vendor Description: " CssClass="auto-style13"></asp:Label>
    <asp:Label ID="lblVenURL" runat="server" style="z-index: 1; left: 100px; top: 192px; position: absolute" Text="Vendor URL: "></asp:Label>
    <asp:TextBox ID="txtVenDesc" runat="server" TextMode="MultiLine" CssClass="auto-style11"></asp:TextBox>
    <asp:Button ID="btnAddVendor" runat="server" OnClick="btnAddVendor_Click" Text="Add Vendor" CssClass="auto-style15" />
    <asp:Label ID="lblStatus" runat="server" style="z-index: 1; left: 567px; top: 264px; position: absolute"></asp:Label>
    <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 102px; top: 243px; position: absolute" Text="Vendor Photo:"></asp:Label>
    <asp:ListBox ID="lstVendor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstVendor_SelectedIndexChanged1"></asp:ListBox>
    <asp:Button ID="btnEditVendor" runat="server" OnClick="btnEditVendor_Click" Text="Edit Vendor" />
   
    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="auto-style17" OnClick="btnClear_Click" />
   
</asp:Content>

