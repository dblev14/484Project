<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddGift.aspx.cs" Inherits="AddGift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style24 {
            position: absolute;
            top: 90px;
            left: 350px;
            z-index: 1;
        }
        .auto-style25 {
            position: absolute;
            top: 165px;
            left: 350px;
            z-index: 1;
            height: 57px;
            width: 210px;
        }
                .auto-style26 {
            position: absolute;
            top: 240px;
            left: 350px;
            z-index: 1;
            height: 30px;
        }
            .auto-style27 {
            position: absolute;
            top: 390px;
            left: 350px;
            z-index: 1;
            height: 30px;
        }
                        .auto-style28 {
            position: absolute;
            top: 572px;
            left: 351px;
            z-index: 1;
            height: 30px;
        }
        .auto-style29 {
            position: absolute;
            top: 80px;
            left: 800px;
            z-index: 1;
            height: 350px;
            width: 500px;
        }
     .auto-style30 {
            position: absolute;
            top: 450px;
            left: 350px;
            z-index: 1;
            height: 30px;
        }
     .auto-style31 {
            position: absolute;
            top: 500px;
            left: 950px;
            z-index: 1;
            height: 30px;
            width: 180px;
        }


        .auto-style32 {
            position: absolute;
            top: 568px;
            left: 546px;
            z-index: 1;
            width: 150px;
        }
        .auto-style33{
            position: absolute;
            top: 515px;
            left: 350px;
            z-index: 1;
            width: 180px;
        }


        .auto-style34 {
            z-index: 1;
            left: 175px;
            top: 590px;
            position: absolute;
            width: 177px;
        }


        .auto-style35 {
            z-index: 1;
            left: 175px;
            top: 530px;
            position: absolute;
            width: 177px;
        }
        .auto-style36 {
            z-index: 1;
            left: 175px;
            top: 465px;
            position: absolute;
            width: 177px;
        }
        .auto-style37 {
            position: absolute;
            top: 325px;
            left: 175px;
            z-index: 1;
        }
        .auto-style38 {
            z-index: 1;
            left: 175px;
            top: 405px;
            position: absolute;
            width: 177px;
        }
        .auto-style39 {
            position: absolute;
            top: 310px;
            left: 350px;
            z-index: 1;

        }
        .auto-styleClear25 {
            position: absolute;

        }


        .auto-style40 {
            position: absolute;
            left: 715px;
            top: 570px;
        }


        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lbl1" runat="server" style="z-index: 1; left: 850px; top: 25px; position: absolute; width: 177px" Text="Gift Management" Font-Bold="True" Font-Size="Large" Font-Underline="True"></asp:Label>
    <asp:Label ID="lbl2" runat="server" style="z-index: 1; left: 175px; top: 100px; position: absolute; width: 177px" Text="Gift Name:" Font-Bold="True" Font-Size="Medium" Font-Underline="False"></asp:Label>
    <asp:Label ID="lbl3" runat="server" style="z-index: 1; left: 175px; top: 175px; position: absolute; width: 177px" Text="Gift Description:" Font-Bold="True" Font-Size="Medium" Font-Underline="False"></asp:Label>
    <asp:Label ID="lbl4" runat="server" style="z-index: 1; left: 175px; top: 250px; position: absolute; width: 177px" Text="Gift Amount:" Font-Bold="True" Font-Size="Medium" Font-Underline="False"></asp:Label>
    <asp:TextBox ID="txtGiftName" runat="server" CssClass="auto-style24"></asp:TextBox>
    <asp:Button ID="btnAddGift" runat="server" CssClass="auto-style28" Text="Add Gift" OnClick="btnAddGift_Click" />
    <asp:Button ID="btnDeleteGift" runat="server" CssClass="auto-style31" Text="Delete Selected Gift" OnClick="btnDeleteGift_Click" />
    <asp:TextBox ID="txtGiftDescription" runat="server" CssClass="auto-style25" TextMode="MultiLine"></asp:TextBox>
    <asp:TextBox ID="txtGiftAmount" runat="server" CssClass="auto-style26"></asp:TextBox>
    <asp:TextBox ID="txtGiftQuantity" runat="server" CssClass="auto-style27"></asp:TextBox>
    <asp:Label ID="lbl5" runat="server" Text="Gift Quantity:" Font-Bold="True" Font-Size="Medium" Font-Underline="False" CssClass="auto-style38"></asp:Label>
    <strong>
    <asp:Label ID="lblGiftCost" runat="server" CssClass="auto-style37" Text="Gift Cost:"></asp:Label>
    </strong>
    <asp:TextBox ID="txtGiftCost" runat="server" CssClass="auto-style39"></asp:TextBox>
    <asp:DropDownList ID="ddlVendor" runat="server" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" CssClass ="auto-style30">
    </asp:DropDownList>
    <asp:Label ID="lbl6" runat="server" Text="Vendor Name:" Font-Bold="True" Font-Size="Medium" Font-Underline="False" CssClass="auto-style36"></asp:Label>
    <asp:FileUpload ID="fileImageUpload" runat="server" cssClass="auto-style33"/>
    <asp:Label ID="lbl8" runat="server" Text="Image:" Font-Bold="True" Font-Size="Medium" Font-Underline="False" CssClass="auto-style35"></asp:Label>
    <asp:Label ID="lbl7" runat="server" Text="" Font-Bold="False" Font-Size="Medium" Font-Underline="False" CssClass="auto-style34"></asp:Label>
    <asp:ListBox ID="listGifts" runat="server" CssClass= "auto-style29" OnSelectedIndexChanged="listGifts_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
    <asp:Button ID="btnEditGift" runat="server" CssClass="auto-style32" OnClick="btnEditGift_Click" Text="Edit Selected Gift" />
    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="auto-style40" />
</asp:Content>

