<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1><span style="font-size: xx-large; color: #FF0000">♥♥♥</span><span style="font-size: xx-large"> Welcome to A Change of Heart Clinic! </span><span style="font-size: xx-large; color: #FF0000">♥♥♥</span></h1>
        <p>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/ProtectedContent/1h697p.jpg" />
        &nbsp;&nbsp;&nbsp;
            <asp:LoginView ID="LoginView2" runat="server">
                <LoggedInTemplate>
                    <asp:Button ID="Button1" runat="server" Text="Search for a Patient" OnClick="Button1_Click" />
                </LoggedInTemplate>
            </asp:LoginView>
        </p>
        <p>&nbsp;</p>
        <p>
            <asp:LoginView ID="LoginView1" runat="server">
                <AnonymousTemplate>
                    Log in to access our member services.
                </AnonymousTemplate>
                <LoggedInTemplate>
                    Welcome,
                    <asp:LoginName ID="LoginName1" runat="server" />
                    !<br />
                </LoggedInTemplate>
            </asp:LoginView>
        </p>
        <p>&nbsp;</p>
    </div>

    <div class="row">
    </div>
</asp:Content>
