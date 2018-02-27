<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Analytics.aspx.cs" Inherits="Analytics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <html>
    
    
    <script type="text/javascript" src="http://www.google.com/jsapi"></script>

     <script type="text/javascript" src="dashboard1.js"></script>


<body>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
        <div>
        </div>
    


<!--
        Containers for visualizations and maps
-->
    
<div id="container1" style="position:absolute;top:0px; left:100px; width: 500px; height: 400px;">
    </div>

    <div id="container2" style="position:absolute;top:0px; left:700px; width: 500px; height: 400px;">
    </div>

    <div id="container3" style="position:absolute;top:500px; left:100px; width: 500px; height: 400px;">
    </div>

    <div id="container4" style="position:absolute;top:0px; left:1300px; width: 500px; height: 400px;">
    </div>

    <div id="container5" style="position:absolute;top:500px; left:700px; width: 500px; height: 400px;">
    </div>

    <div id="container6" style="position:absolute;top:500px; left:1300px; width: 500px; height: 400px;">
    </div>
    <div id="container7" style="position:absolute;top:1000px; left:100px; width: 500px; height: 400px;">
    </div>

    <div id="container8" style="position:absolute;top:1000px; left:700px; width: 500px; height: 400px;">
    </div>

    <div id="container9" style="position:absolute;top:1000px; left:1300px; width: 500px; height: 400px;">
    </div>

    <div id="container10" style="position:absolute;top:1500px; left:100px; width: 500px; height: 400px;">
    </div>

    <div id="container11" style="position:absolute;top:1500px; left:700px; width: 500px; height: 400px;">
    </div>

    <div id="container12" style="position:absolute;top:1500px; left:1300px; width: 500px; height: 400px;">
    </div>

    <asp:Label ID="lblStuff1" runat="server" style="z-index: 1; left: 800px; top: 450px; position: absolute; width: 425px" Font-Bold="True" Font-Size="X-Large">Employee Gift Information:</asp:Label>
    <asp:Label ID="lblStuff2" runat="server" style="z-index: 1; left: 800px; top: 950px; position: absolute; width: 425px" Font-Bold="True" Font-Size="X-Large">Employee Reward Information:</asp:Label>
    <asp:Label ID="lblStuff3" runat="server" style="z-index: 1; left: 800px; top: 1450px; position: absolute; width: 425px" Font-Bold="True" Font-Size="X-Large">Employee Monetary Information:</asp:Label>
</body>
        </html>
</asp:Content>

