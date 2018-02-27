<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
     
   
        .auto-style12 {
            position: absolute;
            top: 1px;
            left: 0px;
        }
        .auto-style13 {
            position: absolute;
            top: 80px;
            left: 660px;
            z-index: 1;
            text-decoration: underline;
        }
        .auto-style14 {
            position: absolute;
            top: 115px;
            left: 700px;
            z-index: 1;
            width: 75px;
            height: 30px;
        }
        .auto-style15 {
            position: absolute;
            top: 236px;
            left: 414px;
            z-index: 1;
            height: 35px;
            width: 165px;
        }
    .auto-style16 {
        height: 35px;
        left: 600px;
        top: 235px;
        width: 275px;
    }
    .auto-style17 {
        z-index: 1;
        left: 600px;
        top: 10px;
        position: absolute;
    }
    .auto-style18 {
        position: absolute;
        top: 235px;
        left: 890px;
        z-index: 1;
        bottom: 109px;
            width: 150px;
        }
            .auto-style19 {
            position: absolute;
            top: 300px;
            left: 414px;
            z-index: 1;
            height: 35px;
            width: 165px;
        }
              .auto-style20 {
            position: absolute;
            top: 300px;
            left: 600px;
            z-index: 1;
            height: 35px;
            width: 165px;
        }
                 .auto-style48 {
        position: absolute;
        top: 300px;
        left: 890px;
        z-index: 1;
        bottom: 36px;
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" CssClass="auto-style12" style="z-index: 1"></asp:Label>
    <asp:Label ID="lblTitle" runat="server" Text="Hello, Administrator!" style="font-size: xx-large; font-family: 'Segoe UI';" CssClass="auto-style17"></asp:Label>
    <strong>
    <asp:Label ID="lblFundAmount" runat="server" CssClass="auto-style14"></asp:Label>
    <asp:Label ID="lblRewardFundTitle" runat="server" CssClass="auto-style13"></asp:Label>
    </strong>    
<asp:Button ID="btnGenerateReports" runat="server" CssClass="auto-style18" OnClick="btnGenerateReports_Click1" Text="Generate Reports" />
    <asp:Button ID="btnViewAnalytics" runat="server" CssClass="auto-style48" OnClick="btnAnalytics_Click1" Text="View Analytics" />
<asp:Button ID="btnEditDeleteEmployees" runat="server" CssClass="auto-style16" OnClick="btnEditDeleteEmployees_Click" Text="View, Edit or Delete Employees" />
<asp:Button ID="btnCreateAccount" runat="server" CssClass="auto-style15" OnClick="btnCreateAccount_Click" Text="Create New Account" />
<asp:Button ID="btnCreateAccount0" runat="server" CssClass="auto-style15" OnClick="btnCreateAccount_Click" Text="Create New Account" />
<asp:Button ID="btnCreateAccount1" runat="server" CssClass="auto-style15" OnClick="btnCreateAccount_Click" Text="Create New Account" />
    <asp:Button ID="btnManageGift" runat="server" CssClass = "auto-style19" OnClick="btnManageGift_Click" Text="Manage Gifts" />
    <asp:Button ID="btnManageVendor" runat="server" CssClass = "auto-style20" OnClick="btnManageVendor_Click" Text="Manage Vendors" />
</asp:Content>

