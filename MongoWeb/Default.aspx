<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="MongoWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <b>Mongodb Connection Status</b> :<div id="pnlConnection" runat="server"></div>

    <asp:MultiView ID="mvMongo" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwMain" runat="server">
            <asp:GridView ID="grdListCustomer" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Bil.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" CommandName="EditMe" runat="server">Edit</asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" CommandName="DeleteMe" runat="server">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </asp:View>
        <asp:View ID="vwEdit" runat="server">
            <asp:HiddenField ID="_id" runat="server" />
            <asp:TextBox ID="name" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="gender" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="age" runat="server"></asp:TextBox><br />

            <asp:Button ID="btnUpdate" runat="server" Text="Update" />
            
        </asp:View>
    </asp:MultiView>
    

    <table>
        <thead>
            <tr>
                <th>Name</th>
                <th>Gender</th>
                <th>Age</th>
            </tr>
        </thead>
        <tbody>
            <% For Each customer In Me.GetListCustomers%>
                <tr>
                    <td><%=customer.Item("name") %></td>
                    <td><%=customer.Item("gender") %></td>
                    <td><%=customer.Item("age") %></td>
                </tr>
            <% Next %>
        </tbody>
    </table>

</asp:Content>
