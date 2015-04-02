<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Practice.aspx.vb" Inherits="Exam2Prep.Practice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <asp:TextBox ID="txtSearch1" runat="server"></asp:TextBox>
        &nbsp;<asp:Button ID="btnSearch1" runat="server" Text="Search 1" />
        &nbsp;&nbsp;<asp:Label ID="lblMessage1" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtSearch2" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch2" runat="server" Text="Search2" />
        <asp:Label ID="lblMessage2" runat="server"></asp:Label>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBoth" runat="server" Text="Both" />
        &nbsp;<asp:RadioButtonList ID="radAndOr" runat="server">
            <asp:ListItem Selected="True">AND</asp:ListItem>
            <asp:ListItem>OR</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:RadioButtonList ID="radTypeOfSearch" runat="server">
            <asp:ListItem Selected="True">Exact</asp:ListItem>
            <asp:ListItem>Partial</asp:ListItem>
            <asp:ListItem>Keyword</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        Search1 by:<br />
        <asp:RadioButtonList ID="radSearch1" runat="server">
            <asp:ListItem Selected="True">Make</asp:ListItem>
            <asp:ListItem>Color</asp:ListItem>
            <asp:ListItem>Price</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        Search2 by:<asp:RadioButtonList ID="radSearch2" runat="server">
            <asp:ListItem Selected="True">Make</asp:ListItem>
            <asp:ListItem>Color</asp:ListItem>
            <asp:ListItem>Price</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        Sort:<br />
        <asp:RadioButtonList ID="radSort" runat="server" AutoPostBack="True">
            <asp:ListItem Selected="True">Make</asp:ListItem>
            <asp:ListItem>StockNumber</asp:ListItem>
            <asp:ListItem>Color</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="btnShowAll" runat="server" Text="Show All" />
        <br />
        <br />
        <br />
        <br />
        <asp:TextBox ID="txtCount" runat="server"></asp:TextBox>
        <asp:GridView ID="gvVehicles" runat="server">
        </asp:GridView>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <p style="margin-left: 80px">
        </p>
    
    </div>
    </form>
</body>
</html>
