<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchEmployeeResults.aspx.cs" Inherits="SearchEmployeeResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style11 {
            position: absolute;
            top: 40px;
            left: 695px;
            z-index: 1;
            width: 110px;
            height: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <asp:Image ID="imgProfile" runat="server" CssClass="auto-style11" />
        <br />
    </p>
    <p>
        <asp:ListBox ID="lstEmployee" runat="server" OnSelectedIndexChanged="lstEmployee_SelectedIndexChanged" Rows="10" style="z-index: 1; left: 6px; top: 34px; position: absolute; height: 255px; width: 586px" AutoPostBack="True"></asp:ListBox>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
        <asp:Button ID="btnViewPage" runat="server" OnClick="btnViewPage_Click" style="z-index: 1; left: 8px; top: 306px; position: absolute" Text="View Page" Width="115px" />
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Large" style="z-index: 1; left: 0px; top: 361px; position: absolute; width: 1041px; height: 71px"></asp:Label>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

