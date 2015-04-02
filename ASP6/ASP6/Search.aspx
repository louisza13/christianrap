<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ASP6.Master" CodeBehind="Search.aspx.vb" Inherits="ASP6.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label14" runat="server" Text="Search By: "></asp:Label>
        <asp:Button ID="btnState" runat="server" Text="State" />
        <asp:Button ID="btnLastname" runat="server" Text="Lastname" />
        <asp:Button ID="btnCity" runat="server" Text="City" />
        <asp:Button ID="btnUsername" runat="server" Text="Username" />
    </p>
    <p>
        <asp:Button ID="btnShowAll" runat="server" Text="Show All" />
    </p>
    <p>
        <asp:RadioButtonList ID="radSort" runat="server">
            <asp:ListItem Selected="True">Sort By Name</asp:ListItem>
            <asp:ListItem>Sort By Username</asp:ListItem>
        </asp:RadioButtonList>
    </p>
    <p>
        <asp:Label ID="Label15" runat="server" Text="Number of Records:"></asp:Label>
        <asp:Label ID="lblRecords" runat="server"></asp:Label>
        <br />
    </p>
    
       
    
        <asp:GridView ID="gvCustomers" runat="server">
        </asp:GridView>
    
       
    
        <p>
    </p>
</asp:Content>
