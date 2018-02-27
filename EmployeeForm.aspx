<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeForm.aspx.cs" Inherits="EmployeeForm" %>

<%@ Register Src="~/RewardInfoControl.ascx" TagPrefix="uc1" TagName="RewardInfoControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style6 {
            width: 354px;
        }
        .auto-style7 {
            text-decoration: underline;
        }
        .auto-style8 {
            position: absolute;
            top: 26px;
            left: 61px;
            z-index: 1;
            width: 130px;
            height: 120px;
        }
        .auto-style9 {
            text-decoration: underline;
            position: absolute;
            top: 161px;
            left: 56px;
            z-index: 1;
            width: 235px;
        }
        /*.auto-style10 {
            position: absolute;
            top: 191px;
            left: 56px;
            z-index: 1;
            width: 230px;
        }*/
        .auto-style11 {
            position: absolute;
            top: 216px;
            left: 56px;
            z-index: 1;
            width: 180px;
        }
        .auto-style12 {
            position: absolute;
            top: 241px;
            left: 56px;
            z-index: 1;
            width: 215px;
        }
        .auto-style16 {
            position: absolute;
            top: 271px;
            left: 56px;
            z-index: 1;
        width: 199px;
    }
        .auto-style17 {
            position: absolute;
            top: 331px;
            left: 56px;
            z-index: 1;
        }
        .auto-style20 {
            width: 235px;
            height: 215px;
            position: absolute;
            top: 165px;
            left: 415px;
            z-index: 1;
        }
        .auto-style34 {
            width: 240px;
            height: 210px;
            position: absolute;
            top: 170px;
            left: 960px;
            z-index: 1;
        }
        .auto-style35 {
            width: 235px;
            height: 210px;
            position: absolute;
            top: 170px;
            left: 685px;
            z-index: 1;
        }
        .auto-style36 {
            width: 235px;
            height: 205px;
            position: absolute;
            top: 395px;
            left: 420px;
            z-index: 1;
        }
        .auto-style37 {
            width: 235px;
            height: 205px;
            position: absolute;
            top: 405px;
            left: 955px;
            z-index: 1;
        }
        .auto-style38 {
            width: 235px;
            height: 205px;
            position: absolute;
            top: 400px;
            left: 680px;
            z-index: 1;
        }
        .auto-style39 {
            position: absolute;
            top: 130px;
            left: 700px;
            z-index: 1;
            }
        .auto-style40 {
            position: absolute;
            top: 11px;
            left: 611px;
            z-index: 1;
        }
        .auto-style41 {
            position: absolute;
            top: 10px;
            left: 475px;
            z-index: 1;
        }
        .auto-style42 {
            position: absolute;
            top: 45px;
            left: 500px;
            z-index: 1;
            width: 55px;
        }
        .auto-style43 {
            position: absolute;
            top: 45px;
            left: 650px;
            z-index: 1;
            width: 50px;
        }
        .auto-style44 {
            position: absolute;
            top: 11px;
            left: 781px;
            z-index: 1;
        }
        .auto-style45 {
            position: absolute;
            top: 45px;
            left: 805px;
            z-index: 1;
            width: 55px;
            height: 35px;
        }
        .auto-style46 {
            position: absolute;
            top: 45px;
            left: 940px;
            z-index: 1;
            width: 139px;
            height: 69px;
        }
        .auto-style47 {
            position: absolute;
            top: 11px;
            left: 941px;
            z-index: 1;
        }
        .auto-style48 {
            position: absolute;
            top: -145px;
            left: -540px;
            z-index: 1;
            width: 670px;
        }
        .auto-style49 {
            position: absolute;
            top: 651px;
            left: 1066px;
            z-index: 1;
            width: 150px;
        }
        .auto-style51 {
        position: absolute;
        top: 45px;
        left: 60px;
        z-index: 1;
        width: 130px;
        height: 120px;
    }
    .auto-style52 {
        text-decoration: underline;
        position: absolute;
        top: 165px;
        left: 55px;
        z-index: 1;
        width: 165px;
        height: 30px;
    }
        .auto-style53 {
            position: absolute;
            top: 190px;
            left: 55px;
            z-index: 1;
        }
                .auto-styleText1 {
            position: absolute;
            top: 230px;
            left: 1430px;
            height: 25px;
            z-index: 1;
        }
                .auto-styleText2 {
            position: absolute;
            top: 375px;
            left: 1430px;
            height: 25px;
            z-index: 1;
        }
 .auto-styleText3 {
            position: absolute;
            top: 520px;
            left: 1430px;
            height: 25px;
            z-index: 1;
        }
        </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblJobTitle" runat="server" CssClass="auto-style53" Text="Position"></asp:Label>
    <strong>
    <asp:Label ID="lblNotifications" runat="server" style="z-index: 1; left: 1400px; top: 155px; position: absolute; width: 134px;">Notifications</asp:Label>
    </strong>
    <asp:Label ID="lblRewardSender1" runat="server" style="z-index: 1; left: 1300px; top: 180px; position: absolute; width: 134px;">Reward Sender:</asp:Label>
    <asp:Label ID="lblActualRewardSender1" runat="server" style="z-index: 1; left: 1430px; top: 180px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblAmount1" runat="server" style="z-index: 1; left: 1300px; top: 205px; position: absolute; width: 134px;">Amount:</asp:Label>
    <asp:Label ID="lblActualAmount1" runat="server" style="z-index: 1; left: 1430px; top: 205px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblReason1" runat="server" style="z-index: 1; left: 1300px; top: 230px; position: absolute; width: 134px;">Reason:</asp:Label>
    <asp:TextBox ID="txtActualReason1" runat="server" CssClass="auto-styleText1" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblDate" runat="server" style="z-index: 1; left: 1300px; top: 275px; position: absolute; width: 134px;">Date:</asp:Label>
    <asp:Label ID="lblActualDate1" runat="server" style="z-index: 1; left: 1430px; top: 275px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblRewardSender2" runat="server" style="z-index: 1; left: 1300px; top: 325px; position: absolute; width: 134px;">Reward Sender:</asp:Label>
    <asp:Label ID="lblActualRewardSender2" runat="server" style="z-index: 1; left: 1430px; top: 325px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblAmount2" runat="server" style="z-index: 1; left: 1300px; top: 350px; position: absolute; width: 134px;">Amount:</asp:Label>
    <asp:Label ID="lblActualAmount2" runat="server" style="z-index: 1; left: 1430px; top: 350px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblReason2" runat="server" style="z-index: 1; left: 1300px; top: 375px; position: absolute; width: 134px;">Reason:</asp:Label>
    <asp:TextBox ID="txtActualReason2" runat="server" CssClass="auto-styleText2" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
    
    <asp:Label ID="lblDate2" runat="server" style="z-index: 1; left: 1300px; top: 420px; position: absolute; width: 134px;">Date:</asp:Label>
    <asp:Label ID="lblActualDate2" runat="server" style="z-index: 1; left: 1430px; top: 420px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblRewardSender3" runat="server" style="z-index: 1; left: 1300px; top: 470px; position: absolute; width: 134px;">Reward Sender:</asp:Label>
    <asp:Label ID="lblActualRewardSender3" runat="server" style="z-index: 1; left: 1430px; top: 470px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblAmount3" runat="server" style="z-index: 1; left: 1300px; top: 495px; position: absolute; width: 134px;">Amount:</asp:Label>
    <asp:Label ID="lblActualAmount3" runat="server" style="z-index: 1; left: 1430px; top: 495px; position: absolute; width: 134px;"></asp:Label>
    <asp:Label ID="lblReason3" runat="server" style="z-index: 1; left: 1300px; top: 520px; position: absolute; width: 134px;">Reason:</asp:Label>
    <asp:TextBox ID="txtActualReason3" runat="server" CssClass="auto-styleText3" TextMode="MultiLine"></asp:TextBox>
    
    <asp:Label ID="lblDate3" runat="server" style="z-index: 1; left: 1300px; top: 565px; position: absolute; width: 134px;">Date:</asp:Label>
    <asp:Label ID="lblActualDate3" runat="server" style="z-index: 1; left: 1430px; top: 565px; position: absolute; width: 134px;"></asp:Label>
    <asp:Image ID="notification1Image" runat="server" style="z-index: 1; left: 1650px; top: 180px; position: absolute; height: 132px; width: 142px" />
    <asp:Image ID="notification2Image" runat="server" style="z-index: 1; left: 1650px; top: 325px; position: absolute; height: 129px; width: 144px" />
    <asp:Image ID="notification3Image" runat="server" style="z-index: 1; left: 1650px; top: 470px; position: absolute; height: 125px; width: 142px" />
    <asp:Button ID="btnMoreNotifications" runat="server" OnClick="btnMoreNotifications_Click" style="z-index: 1; left: 1350px; top: 600px; position: absolute" Text="View More Notifications" Width="254px" />
    <strong>
    <asp:Label ID="lblRecentTransactions" runat="server" CssClass="auto-style39" Text="Recent Transactions" Font-Size="X-Large"></asp:Label>
    <asp:Label ID="lblRewardsSennt2" runat="server" CssClass="auto-style41" Text="Rewards Sent"></asp:Label>
    </strong>
    <asp:Label ID="lblRewardsSentNbr" runat="server" CssClass="auto-style42" Font-Bold="True" Font-Size="Large" ForeColor="#2988BC" Text="1"></asp:Label>
    <strong>
    <asp:Label ID="Label3" runat="server" CssClass="auto-style40" Text="Rewards Received"></asp:Label>
    </strong>
    <asp:Label ID="lblRewardsReceivedNbr" runat="server" CssClass="auto-style43" Font-Bold="True" Font-Size="Large" ForeColor="#2988BC" Text="2"></asp:Label>
    <asp:Label ID="lblCommonCategoryString" runat="server" CssClass="auto-style45" Font-Bold="True" Font-Size="Large" ForeColor="#2988BC" Text="3"></asp:Label>
    <asp:Label ID="lblCommonCategory" runat="server" CssClass="auto-style44" Font-Bold="True">Common Value</asp:Label>
    <asp:Label ID="lblBiggestFanString" runat="server" CssClass="auto-style46" Font-Bold="True" Font-Size="Large" ForeColor="#2988BC" Text="4"></asp:Label>
    <asp:Label ID="lblBiggestFan" runat="server" CssClass="auto-style47" Font-Bold="True" Text="Biggest Fan"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" CssClass="auto-style20">
        <uc1:RewardInfoControl runat="server" ID="RewardInfoControl" />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" CssClass="auto-style35">
        <uc1:RewardInfoControl runat="server" ID="RewardInfoControl1" />
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" CssClass="auto-style34">
        <uc1:RewardInfoControl runat="server" ID="RewardInfoControl2" />
        <asp:Label ID="Label4" runat="server" CssClass="auto-style48" Text="_______________________________________________________________________________________________"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="Panel4" runat="server" CssClass="auto-style36">
        <uc1:RewardInfoControl runat="server" ID="RewardInfoControl3" />
    </asp:Panel>
    <asp:Panel ID="Panel5" runat="server" CssClass="auto-style38">
        <uc1:RewardInfoControl runat="server" ID="RewardInfoControl4" />
    </asp:Panel>
    <asp:Panel ID="Panel6" runat="server" CssClass="auto-style37">
        <uc1:RewardInfoControl runat="server" ID="RewardInfoControl5" />
    </asp:Panel>
    <asp:Image ID="profileImage" runat="server" CssClass="auto-style51" />
    <strong>
    <asp:Label ID="lblFullName" runat="server" CssClass="auto-style52" Text="Full Name"></asp:Label>
    </strong>
    <asp:Label ID="lblRewardBalance" runat="server" CssClass="auto-style12" Text="Reward Balance"></asp:Label>
    <asp:Label ID="lblStartDate" runat="server" CssClass="auto-style11" Text="StartDate"></asp:Label>
    <asp:Button ID="btnSendReward" runat="server" CssClass="auto-style16" Text="Send Reward" OnClick="btnSendReward_Click1" />
    <asp:Label ID="lblStatus" runat="server" CssClass="auto-style17"></asp:Label>

  
    <asp:Button ID="btnMoreTransactions" runat="server" CssClass="auto-style49" OnClick="btnMoreTransactions_Click" Text="More Transactions " />

                    

    
    
   
    
</asp:Content>

