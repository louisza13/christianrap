<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DDLandGrid.aspx.vb" Inherits="Exam2Prep.DDLandGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" Height="23px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnShowAll" runat="server" Text="Show All" />
        <br />
        <br />
        <asp:TextBox ID="txtCustomerCount" runat="server"></asp:TextBox>
        <asp:GridView ID="gvCustomers" runat="server">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
