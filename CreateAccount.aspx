<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style6 {
        width: 298px;
    }
    .auto-style7 {
        font-weight: bold;
    }
        .auto-style8 {
            position: absolute;
            top: 21px;
            left: 11px;
            z-index: 1;
            width: 125px;
        }
        .auto-style9 {
            position: absolute;
            top: 101px;
            left: 11px;
            z-index: 1;
            width: 150px;
        }
        
        .auto-style11 {
            position: absolute;
            top: 26px;
            left: 516px;
            z-index: 1;
            width: 125px;
        }
        .auto-style12 {
            position: absolute;
            top: 25px;
            left: 265px;
            z-index: 1;
            width: 135px;
        }
        .auto-style13 {
            position: absolute;
            top: 46px;
            left: 11px;
            z-index: 1;
        }
        .auto-style15 {
            position: absolute;
            top: 46px;
            left: 266px;
            z-index: 1;
            width: 155px;
        }
        .auto-style16 {
            position: absolute;
            top: 120px;
            left: 260px;
            z-index: 1;
            width: 165px;
            right: 1218px;
        }
        .auto-style17 {
            position: absolute;
            top: 46px;
            left: 526px;
            z-index: 1;
            width: 61px;
        }
        .auto-style20 {
            position: absolute;
            top: 206px;
            left: 11px;
            z-index: 1;
        }
        .auto-style21 {
            position: absolute;
            top: 187px;
            left: 12px;
            z-index: 1;
            width: 130px;
            height: 19px;
        }
        .auto-style23 {
            position: absolute;
            top: 266px;
            left: 16px;
            z-index: 1;
        }
        .auto-style24 {
            position: absolute;
            top: 326px;
            left: 16px;
            z-index: 1;
            width: 251px;
        }
        .auto-style25 {
            position: absolute;
            top: 25px;
            left: 960px;
            z-index: 1;
        }
        .auto-style26 {
            position: absolute;
            top: 46px;
            left: 666px;
            z-index: 1;
        width: 224px;
    }
        .auto-style27 {
            position: absolute;
            top: 45px;
            left: 960px;
            z-index: 1;
        }
        .auto-style28 {
            position: absolute;
            top: 25px;
            left: 670px;
            z-index: 1;
            width: 130px;
        }
    .auto-style29 {
        z-index: 1;
        left: 960px;
        top: 85px;
        position: absolute;
    }
    .auto-style30 {
        z-index: 1;
        left: 960px;
        top: 85px;
        position: absolute;
        bottom: 271px;
    }
    .auto-style31 {
        z-index: 1;
        left: 671px;
        top: 86px;
        position: absolute;
    }
    .auto-style32 {
        z-index: 1;
        left: 666px;
        top: 86px;
        position: absolute;
        bottom: 270px;
    }
    .auto-style33 {
        z-index: 1;
        left: 506px;
        top: 86px;
        position: absolute;
    }
    .auto-style34 {
        z-index: 1;
        left: 296px;
        top: 86px;
        position: absolute;
    }
    .auto-style35 {
        z-index: 1;
        left: 281px;
        top: 86px;
        position: absolute;
    }
    .auto-style38 {
        position: absolute;
        top: 30px;
        left: 15px;
        z-index: 1;
        width: 500px;
    }
    .auto-style39 {
        position: absolute;
        top: 105px;
        left: 10px;
        z-index: 1;
        width: 165px;
        height: 18px;
    }
    .auto-style40 {
        position: absolute;
        top: 105px;
        left: 265px;
        z-index: 1;
    }
    .auto-style41 {
        z-index: 1;
        left: 265px;
        top: 160px;
        position: absolute;
        height: 23px;
    }
    .auto-style42 {
        z-index: 1;
        left: 260px;
        top: 165px;
        position: absolute;
        bottom: 191px;
    }
        .auto-style43 {
            position: absolute;
            top: 120px;
            left: 15px;
            z-index: 1;
            width: 115px;
        }
        .auto-style44 {
            left: 335px;
            top: 195px;
            width: 40px;
        }
        .auto-style45 {
            position: absolute;
            top: 205px;
            left: 265px;
            z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>
<asp:Label ID="lblPosition" runat="server" CssClass="auto-style40" Text="Position:"></asp:Label>
    <asp:Label ID="lblFirstName" runat="server" CssClass="auto-style38" Text="First Name:*"></asp:Label>
    </strong>
    <asp:TextBox ID="txtMiddleInitial" runat="server" CssClass="auto-style17" TabIndex="3"></asp:TextBox>
    <asp:TextBox ID="txtFirstName" runat="server" CssClass="auto-style13" TabIndex="1"></asp:TextBox>
    <asp:TextBox ID="txtLastName" runat="server" CssClass="auto-style15" TabIndex="2"></asp:TextBox>
    <strong>
    <asp:Label ID="lblUsername" runat="server" CssClass="auto-style21" Text="Username:*"></asp:Label>
    <asp:Button ID="btnCreateAccount" runat="server" CssClass="auto-style23" OnClick="btnCreateAccount_Click1" Text="Create Account" TabIndex="10" />
    </strong>
    <asp:Label ID="lblStatus" runat="server" CssClass="auto-style24"></asp:Label>
    <input id="chkAdminFlag" name="chkAdminFlag" runat="server" class="auto-style44" type="checkbox" /><strong><asp:Label ID="Label1" runat="server" CssClass="auto-style45" Text="Admin "></asp:Label>
    </strong>
    <asp:TextBox ID="txtUsername" runat="server" CssClass="auto-style20" TabIndex="9"></asp:TextBox>
    <asp:TextBox ID="txtPosition" runat="server" CssClass="auto-style16" TabIndex="7"></asp:TextBox>
    <asp:TextBox ID="txtStartDate" runat="server" CssClass="auto-style43" TabIndex="6"></asp:TextBox>
    <strong>
    <asp:Label ID="lblLastName" runat="server" CssClass="auto-style12" Text="Last Name:*"></asp:Label>
    <asp:Label ID="lblMiddleInitial" runat="server" CssClass="auto-style11" Text="Middle Initial:"></asp:Label>
    <asp:Label ID="lblPhoneNumber" runat="server" CssClass="auto-style28" Text="Phone Number:*"></asp:Label>
    <asp:Label ID="lblEmail" runat="server" CssClass="auto-style25" Text="Email:*"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" CssClass="auto-style27" TabIndex="5"></asp:TextBox>
    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="auto-style26" TabIndex="4"></asp:TextBox>
    <asp:Label ID="lblStartDate" runat="server" CssClass="auto-style39" Text="Start Date:*"></asp:Label>
    </strong>


    <asp:RequiredFieldValidator
        id="reqName1"
        ControlToValidate="txtFirstName"
        Text="Required"
        Runat="server" ForeColor="Red" style="z-index: 1; left: 14px; top: 78px; position: absolute" />

    <asp:RequiredFieldValidator
        id="reqName2"
        ControlToValidate="txtLastName"
        Text="Required"
        Runat="server" ForeColor="Red" CssClass="auto-style35" />

    <asp:RequiredFieldValidator
        id="reqPhone"
        ControlToValidate="txtPhoneNumber"
        Text="Required"
        Runat="server" ForeColor="Red" CssClass="auto-style32" />

    <asp:RequiredFieldValidator
        id="reqEmail"
        ControlToValidate="txtEmail"
        Text="Required"
        Runat="server" ForeColor="Red" CssClass="auto-style30" />

    <asp:RequiredFieldValidator
        id="reqStartDate"
        ControlToValidate="txtStartDate"
        Text="Required"
        Runat="server" ForeColor="Red" style="z-index: 1; left: 20px; top: 155px; position: absolute; bottom: 201px;" />

    <asp:RequiredFieldValidator
        id="reqPosition"
        ControlToValidate="txtPosition"
        Text="Required"
        Runat="server" ForeColor="Red" style="z-index: 1; left: 14px; top: 244px; position: absolute; bottom: 112px;" />

    <asp:RequiredFieldValidator
        id="reqUsername"
        ControlToValidate="txtUsername"
        Text="Required"
        Runat="server" ForeColor="Red" CssClass="auto-style42" />

    
    
    
    
    <asp:RegularExpressionValidator
            id="maxFirst"
            ControlToValidate="txtFirstName"
            Text="Invalid"
            ValidationExpression="[a-zA-Z0-9/s]{0,30}"
            runat="server" style="z-index: 1; left: 28px; top: 78px; position: absolute" />
    <asp:RegularExpressionValidator
            id="maxLast"
            ControlToValidate="txtLastName"
            Text="Invalid"
            ValidationExpression="[a-zA-Z0-9/s]{0,50}"
            runat="server" CssClass="auto-style34" />
    <asp:RegularExpressionValidator
            id="maxMI"
            ControlToValidate="txtMiddleInitial"
            Text="One Character Only"
            ValidationExpression="[a-zA-Z0-9]{0,1}"
            runat="server" CssClass="auto-style33" />
    <asp:RegularExpressionValidator
            id="maxPhone"
            ControlToValidate="txtPhoneNumber"
            Text="Invalid"
            ValidationExpression="[0-9]{10}"
            runat="server" CssClass="auto-style31" />
    <asp:RegularExpressionValidator
            id="maxEmail"
            ControlToValidate="txtEmail"
            Text="Invalid"
            ValidationExpression="[a-zA-Z0-9@.]{1,30}"
            runat="server" CssClass="auto-style29" />
    <asp:RegularExpressionValidator
            id="maxPosition"
            ControlToValidate="txtPosition"
            Text="Invalid"
            ValidationExpression="[a-zA-Z0-9,/s]{1,50}"
            runat="server" CssClass="auto-style41" />
    <asp:RegularExpressionValidator
            id="maxUsername"
            ControlToValidate="txtUsername"
            Text="Invalid"
            ValidationExpression="[a-zA-Z0-9]{5,30}"
            runat="server" style="z-index: 1; left: 25px; top: 243px; position: absolute" />
                <asp:CompareValidator
                id="cmpDOB"
                ControlToValidate="txtStartDate"
                Text="Invalid Date"
                Operator="DataTypeCheck"
                Type="Date"
                Runat="server" style="z-index: 1; left: 16px; top: 155px; position: absolute" />
</asp:Content>

