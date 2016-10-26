<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="PhoneListApp.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/search.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    ID: <asp:TextBox ID="tbID" runat="server"></asp:TextBox><br />
    FIO: <asp:TextBox ID="tbFIO" runat="server"></asp:TextBox><br />
    Birthday: <asp:TextBox ID="tbBirthday" runat="server"></asp:TextBox><br />
    Passport: <asp:TextBox ID="tbPassport" runat="server"></asp:TextBox><br />
    INN: <asp:TextBox ID="tbINN" runat="server"></asp:TextBox><br />
    Work: <asp:TextBox ID="tbWork" runat="server"></asp:TextBox><br />

    Education: <asp:DropDownList ID="ddlEducation" runat="server">
        <asp:ListItem Value="1">Начальное</asp:ListItem>
        <asp:ListItem Value="2">Среднее</asp:ListItem>
        <asp:ListItem Value="3">Высшее</asp:ListItem>
        <asp:ListItem Value="4">Доктор наук</asp:ListItem>
        <asp:ListItem Selected="True" Value="-1">Не выбрано</asp:ListItem>
    </asp:DropDownList><br />

    Address: <asp:TextBox ID="tbAddress" runat="server"></asp:TextBox><br />

    Sex: <asp:DropDownList ID="ddlSex" runat="server">
        <asp:ListItem Value="M">Мужской</asp:ListItem>
        <asp:ListItem Value="F">Женский</asp:ListItem>
        <asp:ListItem Selected="True" Value="N">Не выбрано</asp:ListItem>
    </asp:DropDownList>

    <br />
    <asp:Button ID="SubmitSearch" runat="server" Text="Search it" />
</asp:Content>
