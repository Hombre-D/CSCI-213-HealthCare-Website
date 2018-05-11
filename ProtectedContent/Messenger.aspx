<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Messenger.aspx.cs" Inherits="ProtectedContent_Messenger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Welcome to the ACHC Messaging System!</h1>
    <span style="font-size: xx-large">
<p>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ProtectedContent/ViewMessages.aspx">View Messages</asp:HyperLink>
</p>
    <p>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/ProtectedContent/SendMessage.aspx">Send Message</asp:HyperLink>
</p>
        </span>
</asp:Content>

