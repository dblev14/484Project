<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SendReward.aspx.cs" Inherits="SendReward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    

    <style type="text/css">
    .auto-style6 {
        position: absolute;
        top: 16px;
        left: 356px;
        z-index: 1;
    }
    .auto-style7 {
        position: absolute;
        top: 46px;
        left: 61px;
        z-index: 1;
    }
    .auto-style9 {
        position: absolute;
        top: 36px;
        left: 356px;
        z-index: 1;
        height: 49px;
            width: 289px;
        }
    /*.auto-style10 {
        position: absolute;
        top: 96px;
        left: 356px;
        z-index: 1;
    }*/
    .auto-style11 {
        left: 358px;
        top: 168px;
        height: 24px;
        position: absolute;
        z-index: 1;
        width: 297px;
    }
    .auto-style12 {
        position: absolute;
        top: 225px;
        left: 357px;
        z-index: 1;
    }
    .auto-style13 {
        position: absolute;
        top: 263px;
        left: 356px;
        z-index: 1;
        width: 297px;
        height: 27px;
    }
    .auto-style15 {
        position: absolute;
        top: 388px;
        left: 354px;
        z-index: 1;
    }
    .auto-style16 {
        position: absolute;
        top: 403px;
        left: 512px;
        z-index: 1;
    }
    .auto-style17 {
        position: absolute;
        top: 302px;
        left: 356px;
        z-index: 1;
    }
    .auto-style18 {
        position: absolute;
        top: 340px;
        height: 25px;
        left: 355px;
        z-index: 1;
    }
    .auto-style20 {
        position: absolute;
        top: 74px;
        left: 357px;
        z-index: 1;
    }
    .auto-style69 {
        position: absolute;
        top: 73px;
        left: 751px;
        z-index: 1;
    }
    .auto-style70 {
        position: absolute;
        top: 94px;
        left: 752px;
        z-index: 1;
    }
        .auto-style71 {
            position: absolute;
            top: 94px;
            left: 56px;
            z-index: 1;
        width: 197px;
        height: 33px;
    }
    .auto-style72 {
        position: absolute;
        top: 73px;
        left: 56px;
        z-index: 1;
    }
        .auto-style76 {
        position: absolute;
        top: 93px;
        left: 357px;
        z-index: 1;
        height: 39px;
        width: 309px;
    }
    .auto-style77 {
        position: absolute;
        top: 147px;
        left: 358px;
        z-index: 1;
    }
        .auto-style78 {
            z-index: 1;
            left: 355px;
            top: 320px;
            position: absolute;
        }
        .auto-styleCool{
        position: absolute;
        top: 400px;
        left: 600px;
        z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <strong>
<asp:Label ID="lblRewardReason" runat="server" CssClass="auto-style20" Text="Reward Reason:"></asp:Label>
</strong>
<asp:TextBox ID="txtRewardReason" runat="server" CssClass="auto-style76" TextMode="MultiLine"></asp:TextBox>
<strong>
<asp:Label ID="lblDateOfDeed" runat="server" CssClass="auto-style69" Text="Date of Deed:"></asp:Label>
</strong>
<asp:TextBox ID="txtDateOfDeed" runat="server" CssClass="auto-style70" TextMode="SingleLine"></asp:TextBox>


<strong>
<asp:Label ID="lblRewardAmount" runat="server" CssClass="auto-style17" Text="Reward Amount:"></asp:Label>
</strong>
<asp:DropDownList ID="dropRewardAmount" runat="server" CssClass="auto-style18">
</asp:DropDownList>
<strong>
<asp:Label ID="Label2" runat="server" CssClass="auto-style77" Text="Company Value:"></asp:Label>
<asp:Label ID="lblRewardCategory" runat="server" CssClass="auto-style12" Text="Reward Category:"></asp:Label>
</strong>
<asp:DropDownList ID="dropValues" runat="server" CssClass="auto-style11">
</asp:DropDownList>
<asp:DropDownList ID="dropCategory" runat="server" CssClass="auto-style13">
</asp:DropDownList>
<asp:Label ID="lblStatus" runat="server" CssClass="auto-styleCool"></asp:Label>
<asp:Button ID="btnSendReward" runat="server" CssClass="auto-style15" OnClick="btnSendReward_Click1" Text="Send Reward" />
<strong>
<asp:Label ID="Label1" runat="server" CssClass="auto-style72" Text="Select Employee To Reward"></asp:Label>
</strong>
    <asp:Label ID="lblRewardEmp" runat="server" style="z-index: 1; left: 6px; top: 3px; position: absolute" Text="Reward an Employee" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
    <asp:DropDownList ID="dropEmployees" runat="server" CssClass="auto-style71" AutoPostBack="True">
    </asp:DropDownList>


    <asp:CompareValidator
        id="valDate"
        ControlToValidate="txtDateOfDeed"
        Text="Please enter a date in the format of MM/DD/YYYY."
        Operator="DataTypeCheck"
        Type="Date"
        runat="server" style="z-index: 1; left: 758px; top: 128px; position: absolute" ForeColor="Red" />

    <asp:RequiredFieldValidator
        id="reqReason"
        ControlToValidate="txtRewardReason"
        Text="Please enter a reason for this reward."
        Runat="server" ForeColor="Red" style="z-index: 1; left: 478px; top: 74px; position: absolute; height: 26px;"  />

    
    <asp:RequiredFieldValidator
        id="reqDate"
        ControlToValidate="txtDateOfDeed"
        Text="(Required Field)"
        Runat="server" ForeColor="Red" style="z-index: 1; left: 753px; top: 128px; position: absolute"  />

    <asp:RequiredFieldValidator 
         ID="reqEmployee" 
         ControlToValidate="dropEmployees" 
         ErrorMessage="Please select an employee to reward."
         runat="server" InitialValue="none" ForeColor="Red" style="z-index: 1; left: 57px; top: 139px; position: absolute"></asp:RequiredFieldValidator>

    <asp:RequiredFieldValidator 
         ID="reqValue" 
         ControlToValidate="dropValues" 
         ErrorMessage="Please select a company value that corresponds with the reward reason."
         runat="server" InitialValue="none" ForeColor="Red" style="z-index: 1; left: 356px; top: 203px; position: absolute"></asp:RequiredFieldValidator>

    <asp:RequiredFieldValidator 
         ID="reqCategory" 
         ControlToValidate="dropCategory" 
         ErrorMessage="Please select a reward category that corresponds with the reward reason."
         runat="server" InitialValue="none" ForeColor="Red" style="z-index: 1; left: 357px; top: 250px; position: absolute"></asp:RequiredFieldValidator>

    <asp:RequiredFieldValidator 
         ID="reqAmount" 
         ControlToValidate="dropRewardAmount" 
         ErrorMessage="Please select a reward amount."
         runat="server" InitialValue="0" ForeColor="Red" CssClass="auto-style78"></asp:RequiredFieldValidator>


   
    <asp:TextBox ID="txtTodaysDate" runat="server" Visible="False"></asp:TextBox>


   
</asp:Content>

