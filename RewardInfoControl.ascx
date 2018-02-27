<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RewardInfoControl.ascx.cs" Inherits="RewardInfoControl" %>
<style type="text/css">

        .auto-style27 {
            position: absolute;
            top: 5px;
            left: 5px;
            z-index: 1;
            width: 50px;
            height: 40px;
        right: 1430px;
    }
        tr,
  img {
    page-break-inside: avoid;
  }
  
img {
  vertical-align: middle;
  border-style: none;
}

  *,
  *::before,
  *::after {
    text-shadow: none !important;
    box-shadow: none !important;
  }
  
*,
*::before,
*::after {
  box-sizing: border-box;
}

        .auto-style28 {
            position: absolute;
            top: 0px;
            left: 65px;
            z-index: 1;
            width: 265px;
        }
        .auto-style29 {
            position: absolute;
            top: 15px;
            left: 65px;
            z-index: 1;
            bottom: 634px;
            width: 265px;
        }
        .auto-style30 {
            position: absolute;
            top: 40px;
            left: 0px;
            z-index: 1;
            width: 210px;
            height: 35px;
        bottom: 573px;
    }
        
button,
input {
  overflow: visible;
}

input,
button,
select,
optgroup,
textarea {
  margin: 10px 0 0 0;
  font-family: inherit;
  font-size: inherit;
  line-height: inherit;
}

        .auto-style31 {
            position: absolute;
            top: 30px;
            left: 65px;
            z-index: 1;
        }
        .auto-style33 {
            position: absolute;
            top: 160px;
            left: 5px;
            z-index: 1;
            width: 250px;
        height: 20px;
    }
        .auto-style32 {
            position: absolute;
            top: 105px;
            left: 5px;
            z-index: 1;
            width: 255px;
            height: 40px;
        }
        </style>
                        <asp:Label ID="lblRewardReceiver" runat="server" CssClass="auto-style28" Text="Reward Receiver: " Font-Size="Small"></asp:Label>
                        <asp:Label ID="lblRewardSender" runat="server" CssClass="auto-style29" Text="Reward Sender:" Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="txtRewardReason" runat="server" AutoPostBack="True" CssClass="auto-style30" ReadOnly="True" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
                        <asp:Label ID="lblRewardDate" runat="server" CssClass="auto-style31" Text="Date: " Font-Size="Small"></asp:Label>
                        <asp:Label ID="lblRewardCategory" runat="server" CssClass="auto-style33" Text="Reward Category:" Font-Size="Small"></asp:Label>
                        <asp:Label ID="lblCompanyValue" runat="server" CssClass="auto-style32" Text="Company Value: " Font-Size="Small"></asp:Label>

                        <asp:Image ID="rewardImage" runat="server" CssClass="auto-style27" />
                        
