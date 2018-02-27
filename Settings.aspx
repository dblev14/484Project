<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">

        .auto-style31 {
            z-index: 1;
            left: 207px;
            top: 191px;
            position: absolute;
        }

        .auto-style2789 {
            z-index: 1;
            left: 207px;
            top: 191px;
            position: absolute;
        }


        .auto-style2785 {
            z-index: 1;
            left: 192px;
            top: 117px;
            position: absolute;
            right: 1348px;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:FileUpload ID="fileImageUpload" runat="server" style="z-index: 1; left: 161px; top: 304px; position: absolute" />
    <asp:Label ID="lblEmail" runat="server" Text="Preferred Email:" style="z-index: 1; left: 6px; top: 89px; position: absolute"></asp:Label>
    <asp:Label ID="lblPhone" runat="server" Text="Preferred Phone:" style="z-index: 1; left: 7px; top: 160px; position: absolute"></asp:Label>
    <asp:Label ID="lblPassword" runat="server" Text="Password:" style="z-index: 1; left: 9px; top: 231px; position: absolute; height: 19px; width: 61px;"></asp:Label>
    <asp:TextBox ID="txtPhone" runat="server" style="z-index: 1; left: 161px; top: 148px; position: absolute"></asp:TextBox>
    <asp:Button ID="btnUpdate" runat="server" Text="Update Settings" style="z-index: 1; left: 161px; top: 377px; position: absolute; height: 29px; width: 148px;" OnClick="btnUpdate_Click" />
    <asp:Button ID="btnPassword" runat="server" Text="Update Password" CausesValidation="False" style="z-index: 1; left: 161px; top: 219px; position: absolute; height: 30px; width: 163px;" OnClick="btnPassword_Click" />
    <asp:TextBox ID="txtEmail" runat="server" style="z-index: 1; left: 161px; top: 79px; position: absolute"></asp:TextBox>
    <asp:Label ID="lblSettings" runat="server" Text="Settings" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
    <asp:Label ID="lblOutput" runat="server" style="z-index: 1; left: 169px; top: 460px; position: absolute"></asp:Label>
    

    <asp:Label ID="lblChangeImage" runat="server" Text="Change Image:" style="z-index: 1; left: 7px; top: 312px; position: absolute"></asp:Label>
        

<%--    <asp:RequiredFieldValidator
        id="reqEmail"
        ControlToValidate="txtEmail"
        Text="Required"
        Runat="server" ForeColor="Red" style="z-index: 1; left: 205px; top: 117px; position: absolute" />
    <asp:RequiredFieldValidator
        id="reqPhone"
        ControlToValidate="txtPhone"
        Text="Required"
        Runat="server" ForeColor="Red" style="z-index: 1; left: 198px; top: 191px; position: absolute" />--%>
        <asp:RegularExpressionValidator
            id="maxPhone"
            ControlToValidate="txtPhone"
            Text="Invalid"
            ValidationExpression="[0-9]{10}"
            runat="server" CssClass="auto-style31" ForeColor="Red" />

        <asp:RegularExpressionValidator
            id="maxEmail"
            ControlToValidate="txtEmail"
            Text="Invalid Email"
            ValidationExpression="[a-zA-Z0-9@.]{1,30}"
            runat="server" CssClass="auto-style2785" ForeColor="Red" />



</asp:Content>


