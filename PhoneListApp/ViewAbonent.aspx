<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="ViewAbonent.aspx.cs" Inherits="PhoneListApp.ViewAbonent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    ID: <asp:Label ID="lbID" runat="server"></asp:Label><br />
    ФИО: <asp:Label ID="lbFIO" runat="server"></asp:Label><br />
    День рождения: <asp:Label ID="lbBirthday" runat="server"></asp:Label><br />
    Паспорт(номер и серия): <asp:Label ID="lbPassport" runat="server"></asp:Label><br />
    ИНН: <asp:Label ID="lbINN" runat="server"></asp:Label><br />
    Работа: <asp:Label ID="lbWork" runat="server"></asp:Label><br />

    Образование: <asp:Label ID="lbEducation" runat="server"></asp:Label><br />
    Адрес: <asp:Label ID="lbAddress" runat="server"></asp:Label><br />

    Пол: <asp:Label ID="lbSex" runat="server"></asp:Label><br />

    <br />
    <a href="EditAbonent.aspx?id=<%=lbID.Text%>">Редактировать</a><br />
    <a href="EditAbonent.aspx?id=<%=lbID.Text%>&remove=true">Удалить</a><br />
    
    <br />
    <a href="Index.aspx">Вернуться</a>
</asp:Content>
