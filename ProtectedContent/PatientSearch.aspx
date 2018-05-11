<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PatientSearch.aspx.cs" Inherits="ProtectedContent_PatientSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <table style="width:100%;">
            <tr>
                <td style="width: 476px; font-size: medium; font-weight: 700;">Find a Patient:</td>
                <td style="font-size: medium; font-weight: 700">Search by Name:</td>
            </tr>
            <tr>
                <td style="width: 476px">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
&nbsp;
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Display Info" />
                    <br />
                </td>
                <td>First:
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;&nbsp; Last:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Search" />
                </td>
            </tr>
            <tr>
                <td style="width: 476px">
                    <asp:ListBox ID="ListBox1" runat="server" Width="268px"></asp:ListBox>
                </td>
                <td>
                    <asp:ListBox ID="ListBox2" runat="server" Width="268px"></asp:ListBox>
                </td>
            </tr>
        </table>
    </p>
</asp:Content>

