<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeTransactions.aspx.cs" Inherits="EmployeeTransactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-styleGridTransactions {
            width: 100%;
            height: 100%;
            position: absolute;
            top: 0px;
            left: 71px;
            z-index: 1;
        }
        .auto-style9 {
            position: absolute;
            top: 0px;
            left: 0px;
        }
        .auto-style11 {
            position:absolute;
            width: 1000px;
            height: auto;
            left: 250px;
            top: 100px;
        }
        .auto-style13 {
            z-index: 1;
            left: 320px;
            top: 10px;
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblTitle" runat="server" Text="All Transactions" Font-Bold="true" Font-Size="XX-Large" CssClass="auto-style13"></asp:Label>
    <div ID="container" class="auto-style11">

      
       <asp:GridView ID="grdAllTransactions" runat="server" CssClass="auto-styleGridTransactions">
    </asp:GridView>
   </div>
    
</asp:Content>

