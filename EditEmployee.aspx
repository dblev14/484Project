<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditEmployee.aspx.cs" Inherits="EditEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style88 {
            position: absolute;
            top: 20px;
            left: 20px;
            z-index: 1;
            width: 700px;
            height: 700px;
        }
                .auto-style8108 {
            position: absolute;
            top: 363px;
            left: 970px;
            z-index: 1;
            width: 141px;
            height: 35px;
        }
                 .auto-style8109 {
            position: absolute;
            top: 20px;
            left: 800px;
            z-index: 1;
            width: 112px;
            height: 30px;
        }   
.auto-style8110 {
            position: absolute;
            top: 20px;
            left: 1050px;
            z-index: 1;
            width: 112px;
            height: 30px;
        } 
.auto-style8111 {
            position: absolute;
            top: 20px;
            left: 1200px;
            z-index: 1;
            width: 112px;
            height: 30px;
        } 
.auto-style8112 {
            position: absolute;
            top: 50px;
            left: 800px;
            z-index: 1;
            width: 175px;
            height: 30px;
        } 
.auto-style8113 {
            position: absolute;
            top: 50px;
            left: 1050px;
            z-index: 1;
            width: 40px;
            height: 30px;
        } 
.auto-style8114 {
            position: absolute;
            top: 50px;
            left: 1200px;
            z-index: 1;
            width: 175px;
            height: 30px;
        } 
.auto-style8115 {
            position: absolute;
            top: 110px;
            left: 800px;
            z-index: 1;
            width: 112px;
            height: 30px;
        } 
.auto-style8116 {
            position: absolute;
            top: 110px;
            left: 1050px;
            z-index: 1;
            width: 112px;
            height: 30px;
        } 
.auto-style8117 {
            position: absolute;
            top: 110px;
            left: 1200px;
            z-index: 1;
            width: 112px;
            height: 30px;
        } 
.auto-style8118 {
            position: absolute;
            top: 130px;
            left: 800px;
            z-index: 1;
            width: 175px;
            height: 30px;
        } 
.auto-style8119 {
            position: absolute;
            top: 130px;
            left: 1050px;
            z-index: 1;
            width: 120px;
            height: 30px;
        } 
.auto-style8120 {
            position: absolute;
            top: 130px;
            left: 1200px;
            z-index: 1;
            width: 175px;
            height: 30px;
        } 
.auto-style8121 {
            position: absolute;
            top: 210px;
            left: 800px;
            z-index: 1;
            width: 40px;
            height: 30px;
        } 
.auto-style8122 {
            position: absolute;
            top: 210px;
            left: 1050px;
            z-index: 1;
            width: 175px;
            height: 30px;
        } 
.auto-style8124 {
            position: absolute;
            top: 180px;
            left: 800px;
            z-index: 1;
            width: 40px;
            height: 30px;
        } 
.auto-style8123 {
            position: absolute;
            top: 193px;
            left: 1051px;
            z-index: 1;
            width: 175px;
            height: 26px;
        } 
.auto-style8125 {
            position: absolute;
            top: 500px;
            left: 900px;
            z-index: 1;
            width: 688px;
            height: 39px;
        } 
.auto-style90000{
    position: absolute;
    top: 365px;
    left:750px;
    z-index: 1;
    width: 200px;
    height: 35px;
}

.auto-style2796{
    position: absolute;
    top: 178px;
    left:1203px;
    z-index: 1;
    width: 100px;
    height: 25px;
}

.auto-style3169{
    position: absolute;
    top: 175px;
    left:1053px;
    z-index: 1;
    width: 100px;
    height: 25px;
}

.auto-style3399{
    position: absolute;
    top: 96px;
    left:1025px;
    z-index: 1;
    width: 146px;
    height: 25px;
}

.auto-style4444{
    position: absolute;
    top: 261px;
    left:763px;
    z-index: 1;
    width: 264px;
    height: 98px;
}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="btnDelete" runat="server" CssClass="auto-style90000" OnClick="btnDelete_Click" Text="Delete Employee" />
    <asp:Button ID="btnUpdate" runat="server" CssClass="auto-style8108" OnClick="btnUpdate_Click" Text="Update Employee" />
    <asp:TextBox ID="txtFirstName" runat="server" CssClass="auto-style8112"></asp:TextBox>
    <asp:TextBox ID="txtMI" runat="server" CssClass="auto-style8113"></asp:TextBox>
    <asp:TextBox ID="txtLastName" runat="server" CssClass="auto-style8114"></asp:TextBox>
    <asp:ListBox ID="listEmployees" runat="server" OnSelectedIndexChanged="listEmployees_SelectedIndexChanged" CssClass="auto-style88" AutoPostBack="True"></asp:ListBox>
    <asp:Label ID="lblFirstName" runat="server" CssClass="auto-style8109" Text="First Name:"></asp:Label>
    <asp:Label ID="lblMI" runat="server" CssClass="auto-style8110" Text="MI:"></asp:Label>
    <asp:Label ID="lblLastName" runat="server" CssClass="auto-style8111" Text="Last Name:"></asp:Label>
    <asp:Label ID="Position" runat="server" CssClass="auto-style8115" Text="Position:"></asp:Label>
    <asp:Label ID="Phone" runat="server" CssClass="auto-style8116" Text="Phone:"></asp:Label>
    <asp:Label ID="Email" runat="server" CssClass="auto-style8117" Text="Email:"></asp:Label>
    <asp:TextBox ID="txtPosition" runat="server" CssClass="auto-style8118"></asp:TextBox>
    <asp:TextBox ID="txtPhone" runat="server" CssClass="auto-style8119"></asp:TextBox>
    <asp:TextBox ID="txtEmail" runat="server" CssClass="auto-style8120"></asp:TextBox>
    <asp:TextBox ID="txtAdmin" runat="server" CssClass="auto-style8121"></asp:TextBox>
    <asp:TextBox ID="txtTerminationDate" runat="server" CssClass="auto-style8122"></asp:TextBox>
    <asp:Label ID="lblTermination" runat="server" CssClass="auto-style8123" Text="TerminationDate:"></asp:Label>
    <asp:Label ID="lblAdmin" runat="server" CssClass="auto-style8124" Text="Admin:"></asp:Label>
    <asp:Label ID="lblMessage" runat="server" CssClass="auto-style8125" Font-Bold="True" Font-Size="Larger"></asp:Label>


     <asp:CompareValidator
            id="valTermDate"
            ControlToValidate="txtTerminationDate"
            Text="Invalid Date"
            Operator="DataTypeCheck"
            Type="Date"
            Runat="server" style="z-index: 1; left: 1053px; top: 263px; position: absolute" ForeColor="Red" />

        <asp:RegularExpressionValidator
            id="maxEmail"
            ControlToValidate="txtEmail"
            Text="Invalid Email"
            ValidationExpression="[a-zA-Z0-9@.]{1,30}"
            runat="server" CssClass="auto-style2796" ForeColor="Red" />

        <asp:RegularExpressionValidator
            id="maxPhone"
            ControlToValidate="txtPhone"
            Text="Invalid Phone"
            ValidationExpression="[0-9]{10}"
            runat="server" CssClass="auto-style3169" ForeColor="Red" />

        <asp:RegularExpressionValidator
            id="maxMI"
            ControlToValidate="txtMI"
            Text="One Character Only"
            ValidationExpression="[a-zA-Z]{0,1}"
            runat="server" CssClass="auto-style3399" ForeColor="Red" />
        
        <asp:RegularExpressionValidator
            id="valFlag"
            ControlToValidate="txtAdmin"
            Text="One character only: 0 for Employee, 1 for Admin"
            ValidationExpression="[0-1]{0,1}"
            runat="server" CssClass="auto-style4444" ForeColor="Red" />

</asp:Content>

