<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style8 {
            z-index: 1;
            left: 2000px;
            top: 21px;
            position: absolute;
            height: 20px;
            width: 386px;
        }
        .auto-style9 {
            z-index: 1;
            left: 1696px;
            top: 21px;
            position: absolute;
            height: 20px;
            width: 160px;
        }

                .auto-style209 {
            z-index: 1;
            left: 1300px;
            top: 21px;
            position: absolute;
            height: 20px;
            width: 300px;
        }
                                .auto-style219 {
            z-index: 1;
            left: 1600px;
            top: 21px;
            position: absolute;
            height: 20px;
            width: 160px;
        }

        .auto-style220 {
            z-index: 1;
            left: 315px;
            top: 21px;
            position: absolute;
            height: 29px;
            width: 175px;
        }

        .auto-style221 {
            position: absolute;
            top: 210px;
            left: 1310px;
            z-index: 1;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblStatus" runat="server" CssClass="auto-style221"></asp:Label>
    <asp:Image ID="imgItem1" runat="server" style="z-index: 1; top: 21px; position: absolute; height: 163px; width: 192px; right: 1089px; left: 96px" />
    <asp:Label ID="lblGift1" runat="server" CssClass="auto-style220"></asp:Label>
    <asp:TextBox ID="txtGift1" runat="server" style="z-index: 1; left: 315px; top: 46px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount1" runat="server" style="z-index: 1; left: 315px; top: 155px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift1Qty" runat="server" style="z-index: 1; left: 315px; top: 179px; position: absolute; width: 130px"></asp:Label>
    <asp:Button ID="btnRemove1" runat="server" OnClick="btnRemove1_Click" Text="Remove" style="z-index: 1; left: 96px; top: 200px; position: absolute" />
    <asp:Image ID="imgItem2" runat="server" style="z-index: 1; top: 21px; position: absolute; height: 163px; width: 192px; right: 2000px; left: 500px" />
    <asp:Label ID="lblGift2" runat="server" style="z-index: 1; left: 719px; top: 21px; position: absolute; height: 21px; width: 190px"></asp:Label>
    <asp:TextBox ID="txtGift2" runat="server" style="z-index: 1; left: 719px; top: 46px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount2" runat="server" style="z-index: 1; left: 719px; top: 155px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift2Qty" runat="server" style="z-index: 1; left: 719px; top: 179px; position: absolute; width: 130px"></asp:Label>
    <asp:Button ID="btnRemove2" runat="server" OnClick="btnRemove2_Click" Text="Remove" style="z-index: 1; left: 500px; top: 200px; position: absolute" />
    <asp:Image ID="imgItem3" runat="server" style="z-index: 1; top: 21px; position: absolute; height: 163px; width: 192px; right: 2000px; left: 904px" />
    <asp:Label ID="lblGift3" runat="server" style="z-index: 1; left: 1123px; top: 21px; position: absolute; height: 21px; width: 175px"></asp:Label>
    <asp:TextBox ID="txtGift3" runat="server" style="z-index: 1; left: 1123px; top: 46px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount3" runat="server" style="z-index: 1; left: 1123px; top: 155px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift3Qty" runat="server" style="z-index: 1; left: 1123px; top: 179px; position: absolute; width: 130px"></asp:Label>
    <asp:Button ID="btnRemove3" runat="server" OnClick="btnRemove3_Click" Text="Remove" style="z-index: 1; left: 904px; top: 200px; position: absolute" />
    <asp:Image ID="imgItem4" runat="server" style="z-index: 1; top: 275px; position: absolute; height: 163px; width: 192px; right: 1089px; left: 96px" />
    <asp:Label ID="lblGift4" runat="server" style="z-index: 1; left: 315px; top: 275px; position: absolute; height: 21px; width: 155px"></asp:Label>
    <asp:TextBox ID="txtGift4" runat="server" style="z-index: 1; left: 315px; top: 300px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount4" runat="server" style="z-index: 1; left: 315px; top: 409px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift4Qty" runat="server" style="z-index: 1; left: 315px; top: 433px; position: absolute; width: 130px"></asp:Label>
    <asp:Button ID="btnRemove4" runat="server" OnClick="btnRemove4_Click" Text="Remove" style="z-index: 1; left: 96px; top: 451px; position: absolute" />
    <asp:Image ID="imgItem5" runat="server" style="z-index: 1; top: 275px; position: absolute; height: 163px; width: 192px; right: 2000px; left: 500px" />
    <asp:Label ID="lblGift5" runat="server" style="z-index: 1; left: 719px; top: 275px; position: absolute; height: 21px; width: 190px"></asp:Label>
    <asp:TextBox ID="txtGift5" runat="server" style="z-index: 1; left: 719px; top: 300px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount5" runat="server" style="z-index: 1; left: 719px; top: 409px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift5Qty" runat="server" style="z-index: 1; left: 719px; top: 433px; position: absolute; width: 130px"></asp:Label>
    <asp:Label ID="lblNothing" runat="server" style="z-index: 1; left: 475px; top: 433px; position: absolute; width: 639px" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <asp:Button ID="btnRemove5" runat="server" OnClick="btnRemove5_Click" Text="Remove" style="z-index: 1; left: 500px; top: 451px; position: absolute" />
    <asp:Image ID="imgItem6" runat="server" style="z-index: 1; top: 275px; position: absolute; height: 163px; width: 192px; right: 2000px; left: 904px" />
    <asp:Label ID="lblGift6" runat="server" style="z-index: 1; left: 1123px; top: 275px; position: absolute; height: 21px; width: 144px"></asp:Label>
    <asp:TextBox ID="txtGift6" runat="server" style="z-index: 1; left: 1123px; top: 300px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount6" runat="server" style="z-index: 1; left: 1123px; top: 409px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift6Qty" runat="server" style="z-index: 1; left: 1123px; top: 433px; position: absolute; width: 130px"></asp:Label>
    <asp:Button ID="btnRemove6" runat="server" OnClick="btnRemove6_Click" Text="Remove" style="z-index: 1; left: 904px; top: 451px; position: absolute" />
    <asp:Image ID="imgItem7" runat="server" style="z-index: 1; top: 529px; position: absolute; height: 163px; width: 192px; right: 1089px; left: 96px" />
    <asp:Label ID="lblGift7" runat="server" style="z-index: 1; left: 315px; top: 529px; position: absolute; height: 21px; width: 155px"></asp:Label>
    <asp:TextBox ID="txtGift7" runat="server" style="z-index: 1; left: 315px; top: 554px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount7" runat="server" style="z-index: 1; left: 315px; top: 663px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift7Qty" runat="server" style="z-index: 1; left: 315px; top: 687px; position: absolute; width: 130px"></asp:Label>
    <asp:Button ID="btnRemove7" runat="server" OnClick="btnRemove7_Click" Text="Remove" style="z-index: 1; left: 96px; top: 710px; position: absolute" />
    <asp:Image ID="imgItem8" runat="server" style="z-index: 1; top: 529px; position: absolute; height: 163px; width: 192px; right: 2000px; left: 500px" />
    <asp:Label ID="lblGift8" runat="server" style="z-index: 1; left: 719px; top: 529px; position: absolute; height: 21px; width: 190px"></asp:Label>
    <asp:TextBox ID="txtGift8" runat="server" style="z-index: 1; left: 719px; top: 554px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount8" runat="server" style="z-index: 1; left: 719px; top: 663px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift8Qty" runat="server" style="z-index: 1; left: 719px; top: 687px; position: absolute; width: 130px"></asp:Label>
    <asp:Button ID="btnRemove8" runat="server" OnClick="btnRemove8_Click" Text="Remove" style="z-index: 1; left: 500px; top: 710px; position: absolute" />   
    <asp:Image ID="imgItem9" runat="server" style="z-index: 1; top: 529px; position: absolute; height: 163px; width: 192px; right: 2000px; left: 904px" />
    <asp:Label ID="lblGift9" runat="server" style="z-index: 1; left: 1123px; top: 529px; position: absolute; height: 21px; width: 144px"></asp:Label>
    <asp:TextBox ID="txtGift9" runat="server" style="z-index: 1; left: 1123px; top: 554px; position: absolute; height: 95px; width: 135px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblAmount9" runat="server" style="z-index: 1; left: 1123px; top: 663px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblGift9Qty" runat="server" style="z-index: 1; left: 1123px; top: 687px; position: absolute; width: 130px"></asp:Label>
    <asp:Button ID="btnRemove9" runat="server" OnClick="btnRemove9_Click" Text="Remove" style="z-index: 1; left: 904px; top: 710px; position: absolute" />
    <asp:Label ID="lblSubtotal" runat="server" style="z-index: 1; left: 1300px; top: 60px; position: absolute; height: 20px; width: 89px" Font-Bold="True" Font-Size="Larger">Total:</asp:Label>
    <asp:Label ID="lblSubtotalAmount" runat="server" style="z-index: 1; left: 1600px; top: 60px; position: absolute; height: 20px; width: 89px" Font-Bold="True" Font-Size="Larger"></asp:Label>
    <asp:Button ID="btnCheckout" runat="server" OnClick="btnCheckout_Click" Text="Checkout" style="z-index: 1; left: 1305px; top: 150px; position: absolute" />
    <asp:Label ID="lblPersonTotal" runat="server" Font-Bold="True" Font-Size="Larger" CssClass="auto-style209"></asp:Label>
    <asp:Label ID="lblPersonActualTotal" runat="server" Font-Bold="True" Font-Size="Larger" CssClass="auto-style219"></asp:Label>
     <asp:Label ID="lblProjectedBalance" runat="server" style="z-index: 1; left: 1300px; top: 109px; position: absolute; height: 20px; width: 300px" Font-Bold="True" Font-Size="Larger">Projected Balance</asp:Label>
    <asp:Label ID="lblActualProjectedBalance" runat="server" style="z-index: 1; left: 1600px; top: 109px; position: absolute; height: 20px; width: 175px" Font-Bold="True" Font-Size="Larger"></asp:Label>
<asp:Label ID="lblMessage" runat="server" style="z-index: 1; left: 1400px; top: 300px; position: absolute; height: 21px; width: 190px" Font-Bold="True" Font-Size="Larger"></asp:Label>
</asp:Content>

