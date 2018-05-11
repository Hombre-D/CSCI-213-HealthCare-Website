<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="ProtectedContent_SendMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ProtectedContent/Messenger.aspx">Back to Messenger home</asp:HyperLink>
    </p>
    <h1>Compose your message below</h1>
    <p>

        To:
        <asp:DropDownList ID="DropDownList1" runat="server" Width="162px">
        </asp:DropDownList>
&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Please select a recipient." style="color: #CC0000"></asp:RequiredFieldValidator>

    </p>
    <p>

        Subject:
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Please enter a subject." style="color: #CC0000"></asp:RequiredFieldValidator>

    </p>
    <p>

        Body:&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please enter your message." style="color: #CC0000"></asp:RequiredFieldValidator>

    </p>
    <p>

        <asp:TextBox ID="TextBox1" runat="server" Height="106px" TextMode="MultiLine" Width="524px"></asp:TextBox>

    </p>
    <p>

        <asp:Button ID="Button1" runat="server" Text="Send Message" OnClick="Button1_Click" />

    </p>
</asp:Content>

