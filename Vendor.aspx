<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Vendor.aspx.cs" Inherits="Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <br />
    </p>
    <p>
    </p>
    <p>
        <asp:Label ID="lblDescription" runat="server" style="z-index: 1; left: 8px; top: 98px; position: absolute; width: 698px"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        
    </p>
    <p>
       
        
       
        &nbsp;</p>
    <p>
        <asp:Label ID="lblName" runat="server" style="z-index: 1; left: 8px; top: 62px; position: absolute; width: 156px"></asp:Label>
        <asp:Image ID="vendorImage" runat="server" style="z-index: 1; left: 359px; top: 29px; position: absolute; height: 187px; width: 239px" />
        <asp:Button ID="btnVisitWebsite" runat="server" Text="Visit Website" Width="167px" OnClick="btnVisitWebsite_Click" style="z-index: 1; left: 0px; top: 266px; position: absolute" />
        <asp:GridView ID="gridGift" runat="server" OnSelectedIndexChanged="gridGift_SelectedIndexChanged" style="z-index: 1; left: 3px; top: 350px; position: absolute; height: 133px; width: 1184px" AllowSorting="True" AutoGenerateSelectButton="True">
</asp:GridView>
    <p>
        &nbsp;</p>
    <p>
        
    </p>
    <p>
        &nbsp;</p>
    <p>
        
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

