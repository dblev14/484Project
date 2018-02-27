<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateReports.aspx.cs" Inherits="GenerateReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
       
        .auto-style11 {
            position: absolute;
            top: 101px;
            left: 451px;
            z-index: 1;
            text-decoration: underline;
        }
        .auto-style12 {
            position: absolute;
            top: 231px;
            left: 236px;
            z-index: 1;
        width: 149px;
    }
        .auto-style13 {
            width: 187px;
            height: 133px;
            position: absolute;
            top: 236px;
            left: 986px;
            z-index: 1;
        }
        .auto-style17 {
            position: absolute;
            top: 101px;
            left: 261px;
            z-index: 1;
            text-decoration: underline;
        }
        .auto-style18 {
            position: absolute;
            top: 246px;
            left: 441px;
            z-index: 1;
            width: 255px;
        }
        .auto-style19 {
            height: 50px;
            width: 150px;
            left: 450px;
            top: 130px;
        }
    .auto-style20 {
        position: absolute;
        top: 130px;
        left: 245px;
        z-index: 1;
        width: 137px;
        height: 19px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <strong>
    <asp:Label ID="lblReportType" runat="server" CssClass="auto-style17" Text="ReportType"></asp:Label>
    </strong>
    <asp:DropDownList ID="dropReportType" runat="server" CssClass="auto-style20">
        <asp:ListItem>Rewards Sent</asp:ListItem>
        <asp:ListItem>Gifts Bought</asp:ListItem>
</asp:DropDownList>
    
    <asp:DropDownList ID="drpReportTimePeriod" runat="server" CssClass="auto-style19">
        <asp:ListItem Value="0">Past 7 Days</asp:ListItem>
        <asp:ListItem Value="1">Past Month</asp:ListItem>
        <asp:ListItem Value="2">Past Year</asp:ListItem>
        <asp:ListItem>All</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="lblStatus" runat="server" CssClass="auto-style18"></asp:Label>
    <asp:Button ID="btnGenerateReport" runat="server" CssClass="auto-style12" OnClick="btnGenerateReport_Click" Text="Generate Report" />
    <asp:GridView ID="gridReport" runat="server" CssClass="auto-style13">
    </asp:GridView>
    <strong>
    <asp:Label ID="lblReportTimePeriod" runat="server" CssClass="auto-style11" Text="Time Period"></asp:Label>
    </strong>
</asp:Content>

