<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RewardFeed.aspx.cs" Inherits="RewardFeed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style19 {
            width: 651px;
        }
        .auto-style20 {
            width: 100%;
            height: 676px;
        }
    .auto-style21 {
        left: 3px;
        top: 148px;
    }
    .auto-style22 {
        left: 771px;
        top: 151px;
            width: 474px;
            height: 599px;
        }
    .auto-style23 {
        left: 1091px;
        top: 116px;
            width: 157px;
            height: 19px;
        }
        .auto-style24 {
            position: absolute;
            top: 85px;
            left: 955px;
            z-index: 1;
            }
        .auto-style25 {
            position: absolute;
            top: 80px;
            left: 325px;
            z-index: 1;
        }
        .auto-style26 {
            position: absolute;
            top: 130px;
            left: 1020px;
            z-index: 1;
            width: 70px;
        }
        .auto-style27 {
            position: absolute;
            top: 166px;
            left: 1281px;
            z-index: 1;
            width: 70px;
            height: 70px;
        }
        .auto-style28 {
            position: absolute;
            top: 171px;
            left: 1366px;
            z-index: 1;
            width: 265px;
        }
        .auto-style29 {
            position: absolute;
            top: 206px;
            left: 1366px;
            z-index: 1;
            bottom: 148px;
            width: 265px;
        }
        .auto-style30 {
            position: absolute;
            top: 291px;
            left: 1281px;
            z-index: 1;
            width: 345px;
            height: 45px;
        }
        .auto-style31 {
            position: absolute;
            top: 251px;
            left: 1286px;
            z-index: 1;
        }
        .auto-style32 {
            position: absolute;
            top: 376px;
            left: 1286px;
            z-index: 1;
            width: 345px;
            height: 65px;
        }
        .auto-style33 {
            position: absolute;
            top: 456px;
            left: 1286px;
            z-index: 1;
            width: 345px;
        }
        .auto-style34 {
            position: absolute;
            top: 536px;
            left: 1288px;
            z-index: 1;
            width: 60px;
        }
        .auto-style35 {
            position: absolute;
            top: 5px;
            left: 675px;
            z-index: 1;
        }
        .auto-styleBanner {
            position: absolute;
            top: 0px;
            left: 0px;
            height: 150px;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <strong>
    <asp:Label ID="lblHeader" runat="server" CssClass="auto-style35" Text="Welcome to Sander &amp; Sons!"></asp:Label>
    </strong>
    <table class="auto-style20">
        <tr>
            <td class="auto-style19">
                 
                <asp:Panel ID="Panel1" runat="server" Width="751px" Height="601px">
                    <asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick">
                    </asp:Timer>
                    <asp:Label ID="lblRewardFeed" runat="server" Font-Bold="True" Text="Reward Feed" CssClass="auto-style25" Font-Size="X-Large"></asp:Label>
                    <asp:UpdatePanel runat="server">
                    <ContentTemplate>             
                     <asp:ListBox ID="lstRewardFeed" runat="server" Width="743px" Height="601px" CssClass="auto-style21" AutoPostBack="True" OnSelectedIndexChanged="lstRewardFeed_SelectedIndexChanged"></asp:ListBox>
                        
                        
                        
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />

                    </Triggers>

                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
            <td>
                
                <%--<asp:Panel ID="Panel2" runat="server" Width="497px" Height="601px">
                    <asp:Timer ID="Timer2" runat="server" Interval="20000" OnTick="Timer1_Tick">
                    </asp:Timer>
                    <asp:Label ID="lblSortBy" runat="server" CssClass="auto-style26" Text="Sort By"></asp:Label>
                    <asp:Label ID="lblLeaderboard" runat="server" Font-Bold="True" Text="Leaderboard" CssClass="auto-style24"></asp:Label>
                    <asp:DropDownList ID="drpLeaderFilter" runat="server" OnSelectedIndexChanged="drpLeaderFilter_SelectedIndexChanged" CssClass="auto-style23" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:UpdatePanel runat="server">
                    <ContentTemplate>             
                     <asp:ListBox ID="lstLeaderBoard" runat="server" CssClass="auto-style22"></asp:ListBox>
                        <asp:Image ID="Image1" runat="server" CssClass="auto-style27" />
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />

                    </Triggers>

                    </asp:UpdatePanel>
                    <asp:Label ID="lblRewardReceiver" runat="server" CssClass="auto-style28" Text="Reward Receiver: "></asp:Label>
                    <asp:Label ID="lblRewardSender" runat="server" CssClass="auto-style29" Text="Reward Sender:"></asp:Label>
                    <asp:TextBox ID="txtRewardReason" runat="server" AutoPostBack="True" CssClass="auto-style30" ReadOnly="True"></asp:TextBox>
                    <asp:Label ID="lblRewardDate" runat="server" CssClass="auto-style31" Text="Date: "></asp:Label>
                    <asp:Label ID="lblRewardCategory" runat="server" CssClass="auto-style33" Text="Reward Category:"></asp:Label>
                    <asp:Label ID="lblCompanyValue" runat="server" CssClass="auto-style32" Text="Company Value: "></asp:Label>
                </asp:Panel>--%>


                <asp:Panel ID="Panel2" runat="server" Width="497px" Height="601px">
                    
                     
                    <asp:Label ID="lblSortBy" runat="server" CssClass="auto-style26" Text="Sort By"></asp:Label>
                    <asp:Label ID="lblLeaderboard" runat="server" Font-Bold="True" Text="Leaderboard" CssClass="auto-style24" Font-Size="X-Large"></asp:Label>
                    <asp:DropDownList ID="drpLeaderFilter" runat="server" OnSelectedIndexChanged="drpLeaderFilter_SelectedIndexChanged" CssClass="auto-style23" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:UpdatePanel runat="server">
                    <ContentTemplate>             
                     <asp:ListBox ID="lstLeaderBoard" runat="server" CssClass="auto-style22"></asp:ListBox>
                        <asp:Image ID="rewardImage" runat="server" CssClass="auto-style27" />
                        <asp:Label ID="lblRewardReceiver" runat="server" CssClass="auto-style28" Text="Reward Receiver: "></asp:Label>
                        <asp:Label ID="lblRewardSender" runat="server" CssClass="auto-style29" Text="Reward Sender:"></asp:Label>
                        <asp:TextBox ID="txtRewardReason" runat="server" AutoPostBack="True" CssClass="auto-style30" ReadOnly="True"></asp:TextBox>
                        <asp:Label ID="lblRewardDate" runat="server" CssClass="auto-style31" Text="Date: "></asp:Label>
                        <asp:Label ID="lblRewardCategory" runat="server" CssClass="auto-style33" Text="Reward Category:"></asp:Label>
                        <asp:Label ID="lblCompanyValue" runat="server" CssClass="auto-style32" Text="Company Value: "></asp:Label>
                        
                        

                    </ContentTemplate>
                    

                    </asp:UpdatePanel>
                    

                   
                </asp:Panel>
                <asp:Button ID="btnLike" runat="server" BackColor="White" CssClass="auto-style34" OnClick="btnLike_Click" style="z-index: 1" Text="Like" BorderStyle="None" ForeColor="White" />
                        

                        <asp:Button ID="btnUnlike" runat="server" BackColor="White" CssClass="auto-style34" OnClick="btnUnlike_Click" Text="Unlike" ForeColor="#2F496E" Visible="False" />

                
                
            </td>
        </tr>
        </table>
    
   
        
    
    
</asp:Content>

