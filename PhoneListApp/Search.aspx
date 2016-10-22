<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="PhoneListApp.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    ID = <asp:TextBox ID="TextBoxID" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="SubmitSearch" runat="server" Text="Search it" />
</asp:Content>
