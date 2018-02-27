<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style24 {
            position: absolute;
            top: 35px;
            left: 300px;
            z-index: 1;
        }
        .auto-style25 {
            position: absolute;
            top: 60px;
            left: 300px;
            z-index: 1;
        }
        .auto-style27 {
            position: absolute;
            top: 120px;
            left: 530px;
            z-index: 1;
        }
        .auto-style28 {
            position: absolute;
            top: 60px;
            left: 530px;
            z-index: 1;
        }
        .auto-style29 {
            position: absolute;
            top: 35px;
            left: 535px;
            z-index: 1;
        }
        .auto-style30 {
            position: absolute;
            top: 105px;
            left: 300px;
            z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>
    <asp:Label ID="lblUsername" runat="server" CssClass="auto-style24" Text="Username:"></asp:Label>
    </strong>
    <asp:TextBox ID="txtUsername" runat="server" CssClass="auto-style25"></asp:TextBox>
    <asp:TextBox ID="txtPassword" runat="server" CssClass="auto-style28" TextMode="Password"></asp:TextBox>
    <strong>
    <asp:Label ID="lblPassword" runat="server" CssClass="auto-style29" Text="Password:"></asp:Label>
    </strong>
    <asp:Label ID="lblStatus" runat="server" CssClass="auto-style27"></asp:Label>
    <asp:Button ID="btnLogin" runat="server" CssClass="auto-style30" OnClick="btnLogin_Click1" Text="Login" />
</asp:Content>

