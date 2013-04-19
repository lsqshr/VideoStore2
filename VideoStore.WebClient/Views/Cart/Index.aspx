<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VideoStore.WebClient.Models.Cart>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Your Cart:</h2>
    <table width="90%" align="center">
        <thead>
            <tr>
                <th align=center>Quantity</th>
                <th align=left>Item</th>
                <th align=right>Price</th>
                <th align=right>Subtotal</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var lItem in Model.OrderItems)
               { %>
                <tr>
                    <td align=center><%= lItem.Quantity %></td>
                    <td align=left> <%=lItem.Media.Title %></td>
                    <td align=right><%=lItem.Media.Price.ToString("c") %></td>
                    <td align=right><%=(lItem.Quantity * lItem.Media.Price).ToString("c") %></td>
                    <td>
                        <% using (Html.BeginForm("RemoveFromCart", "Cart"))
                           { %>
                            <%= Html.Hidden("pMediaId", lItem.Media.Id)%>
                            <%= Html.Hidden("returnUrl", ViewData["returnUrl"])%>
                            <input type="submit" value=Remove />
                        <% } %>
                    </td>
                </tr>
            <% } %>
        </tbody>
        <tfoot>
            <tr>
                <td colspan=3 align=right> Total: </td>
                <td align=right>
                    <%= Model.ComputeTotalValue().ToString("c") %>
                </td>
            </tr>
        </tfoot>
    </table>
    <p align=center class="actionButtons">
        <a href="<%= Html.Encode(ViewData["returnUrl"])  %>">Continue Shopping</a>
        <%= Html.ActionLink("Check out now", "CheckOut") %> 
    </p>
</asp:Content>
