﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="PhoneListApp.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/search.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    ID: <asp:TextBox ID="tbID" runat="server"></asp:TextBox><br />
    ФИО: <asp:TextBox ID="tbFIO" runat="server"></asp:TextBox><br />
    День рождения: <asp:TextBox ID="tbBirthday" runat="server"></asp:TextBox><br />
    Паспорт(номер и серия): <asp:TextBox ID="tbPassport" runat="server"></asp:TextBox><br />
    ИНН: <asp:TextBox ID="tbINN" runat="server"></asp:TextBox><br />
    Работа: <asp:TextBox ID="tbWork" runat="server"></asp:TextBox><br />

    Образование: <asp:DropDownList ID="ddlEducation" runat="server">
        <asp:ListItem Value="1">Начальное</asp:ListItem>
        <asp:ListItem Value="2">Среднее</asp:ListItem>
        <asp:ListItem Value="3">Высшее</asp:ListItem>
        <asp:ListItem Value="4">Доктор наук</asp:ListItem>
        <asp:ListItem Selected="True" Value="-1">Не выбрано</asp:ListItem>
    </asp:DropDownList><br />

    Адрес: <asp:TextBox ID="tbAddress" runat="server"></asp:TextBox><br />

    Пол: <asp:DropDownList ID="ddlSex" runat="server">
        <asp:ListItem Value="M">Мужской</asp:ListItem>
        <asp:ListItem Value="F">Женский</asp:ListItem>
        <asp:ListItem Selected="True" Value="N">Не выбрано</asp:ListItem>
    </asp:DropDownList>

    <br />
    <asp:Button ID="btnSubmitSearch" runat="server" Text="Search it" />
</asp:Content>

<asp:Content ID="FooterContent" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <br />
    <a href="Index.aspx">Вернуться</a>
</asp:Content>