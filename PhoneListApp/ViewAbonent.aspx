<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="ViewAbonent.aspx.cs" Inherits="PhoneListApp.ViewAbonent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="leftblock">ID:</div><div><asp:Label ID="lbID" runat="server"></asp:Label></div>
    <div class="leftblock">ФИО:</div><div><asp:Label ID="lbFIO" runat="server"></asp:Label></div>
    <div class="leftblock">День рождения:</div><div><asp:Label ID="lbBirthday" runat="server"></asp:Label></div>
    <div class="leftblock">Паспорт(номер и серия):</div><div><asp:Label ID="lbPassport" runat="server"></asp:Label></div>
    <div class="leftblock">ИНН:</div><div><asp:Label ID="lbINN" runat="server"></asp:Label></div>
    <div class="leftblock">Работа:</div><div><asp:Label ID="lbWork" runat="server"></asp:Label></div>
    <div class="leftblock">Образование:</div><div><asp:Label ID="lbEducation" runat="server"></asp:Label></div>
    <div class="leftblock">Адрес:</div><div><asp:Label ID="lbAddress" runat="server"></asp:Label></div>
    <div class="leftblock">Пол:</div><div><asp:Label ID="lbSex" runat="server"></asp:Label></div>

    <br />
    <a href="EditAbonent.aspx?id=<%=lbID.Text%>">Редактировать</a><br />
    <a href="EditAbonent.aspx?id=<%=lbID.Text%>&remove=true">Удалить</a><br />

</asp:Content>

<asp:Content ID="FooterContent" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <br />
    <a href="Index.aspx">Вернуться</a>
</asp:Content>