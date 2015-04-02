<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ASP6.Master" CodeBehind="Login.aspx.vb" Inherits="ASP6.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ASP5StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="ASP5StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="center">
    
        <br />
        <asp:Label ID="Label13" runat="server" Text="EmpID"></asp:Label>
        <br />
        <asp:TextBox ID="txtEmpID" runat="server" CssClass="textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label14" runat="server" Text="Password"></asp:Label>
        <br />
        <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" />
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <br />
      </div>
</asp:Content>
