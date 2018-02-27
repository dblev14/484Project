<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gift.aspx.cs" Inherits="Gift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style11 {
            position: absolute;
            top: 85px;
            left: 10px;
            z-index: 1;
        }
        .auto-style12 {
            z-index: 1;
            left: 0px;
            top: 355px;
            position: absolute;
            width: 360px;
        }
        .auto-style13 {
            z-index: 1;
            left: 10px;
            top: 235px;
            position: absolute;
        }
        .auto-styleShoppingQuant {
            left: 190px;
            height: 25px;
            top: 220px;
            width: 47px;
        }
        .auto-style15 {
            z-index: 1;
            left: 10px;
            top: 195px;
            position: absolute;
            width: 99px;
        }
        .auto-style16 {
            z-index: 1;
            left: 10px;
            top: 160px;
            position: absolute;
            width: 235px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <br />
    </p>
    <p>
    </p>
    <p>
        <asp:TextBox ID="txtDescription" runat="server" CssClass="auto-style11" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lblVendor" runat="server" CssClass="auto-style16"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblPrice" runat="server" CssClass="auto-style15"></asp:Label>
    </p>
    <p>
        
        <asp:Label ID="lblQuantity" runat="server" Text="Quantity:" CssClass="auto-style13"></asp:Label>
        <asp:DropDownList ID="ddlQuantity" runat="server" OnSelectedIndexChanged="ddlQuantity_SelectedIndexChanged" CssClass="auto-styleShoppingQuant">
        </asp:DropDownList>
        
    </p>
    <p>
       
        <asp:Label ID="lblNothing" runat="server" style="z-index: 1; left: 490px; top: 433px; position: absolute; width: 508px" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
       
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" style="z-index: 1; left: 0px; top: 250px; position: absolute" Text="Add to Shopping Cart" Width="254px" />
       
        
       
    </p>
    <p>
        <strong>
        <asp:Label ID="lblName" runat="server" style="z-index: 1; left: 8px; top: 62px; position: absolute; width: 425px"></asp:Label>
        
        </strong>
        
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" style="z-index: 1; left: 0px; top: 290px; position: absolute" Text="Save for Later" Width="254px" />
        
    <p>
        <asp:Button ID="btnViewVendorPage" runat="server" OnClick="btnViewVendorPage_Click" CssClass="auto-style12" />
</p>
    <p>
        
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Image ID="giftImage" runat="server" style="z-index: 1; left: 450px; top: 29px; position: absolute; height: 187px; width: 239px" />
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p style="margin-top: 0px">
    </p>
</asp:Content>

