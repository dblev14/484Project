<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .auto-style9 {
            position: absolute;
            top: 36px;
            left: 331px;
            z-index: 1;
            margin-top: 0px;
        }
        /*.auto-style10 {
            position: absolute;
            top: 56px;
            left: 76px;
            z-index: 1;
        }*/
        .auto-style11 {
            position: absolute;
            top: 51px;
            left: 336px;
            z-index: 1;
            margin-top: 10px;
        }
        .auto-style12 {
            position: absolute;
            top: 30px;
            left: 80px;
            z-index: 1;
        }
        .auto-style13 {
            position: absolute;
            left: 76px;
            z-index: 1;
            top: 121px;
            width: 159px;
        }
        .auto-style17 {
            position: absolute;
            top: 141px;
            left: 341px;
            z-index: 1;
            height: 19px;
        }
        .auto-style17 {
            position: absolute;
            top: 121px;
            left: 315px;
            z-index: 1;
            height: 56px;
            width: 165px;
            margin-top: 4px;
        }
        .auto-style18 {
            position: absolute;
            top: 26px;
            left: 345px;
            z-index: 1;
            height: 5px;
            width: 165px;
            margin-top: 4px;
        }
        .auto-style19 {
            position: absolute;
            top: 50px;
            left: 75px;
            z-index: 1;
            height: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>
    <asp:Label ID="lblOldPassword" runat="server" CssClass="auto-style12" Text="Old Password"></asp:Label>
    <asp:Label ID="lblNewPassword" runat="server" CssClass="auto-style18" Text="New Password"></asp:Label>
    </strong>
    <asp:TextBox ID="txtOldPassword" runat="server" CssClass="auto-style19"></asp:TextBox>
    <asp:Button ID="btnUpdatePassword" runat="server" CssClass="auto-style13" Text="Update Password" OnClick="btnUpdatePassword_Click" />
    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="auto-style11"></asp:TextBox>
    
    <asp:Label ID="lblStatus" runat="server" CssClass="auto-style17"></asp:Label>
        
    </asp:Content>

