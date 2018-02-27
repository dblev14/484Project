<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GiftBoard.aspx.cs" Inherits="GiftBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<p>
    &nbsp;</p>
<asp:GridView ID="gridGift" runat="server" OnSelectedIndexChanged="gridGift_SelectedIndexChanged" style="z-index: 1; left: 71px; top: 97px; position: absolute; height: 133px; width: 1184px" AllowSorting="True" AutoGenerateSelectButton="True" AutoGenerateColumns="False">
    <Columns>
        <asp:boundfield datafield="GiftName" headertext="Gift Name" />
        <asp:boundfield datafield="VendorName" headertext="Vendor Name" />
        <asp:boundfield datafield="GiftAmount" HeaderText="Gift Amount" DataFormatString="${0:F2}" />
    </Columns>
</asp:GridView>
    <asp:Label ID="lblGiftBoard" runat="server" style="z-index: 1; left: 12px; top: 9px; position: absolute" Text="Gift Board" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <asp:Label ID="lblNothing" runat="server" style="z-index: 1; left: 490px; top: 433px; position: absolute; width: 508px" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
</asp:Content>

