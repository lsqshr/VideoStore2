<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VideoStore.WebClient.Models.Cart>" %>
<% if(Model.OrderItems.Count > 0) { %>
    <div id="cart">
        <span class="caption">
            <b>Your Cart: </b>
            <%= Model.OrderItems.Sum(x =>x.Quantity) %> item(s),
            <%= Model.ComputeTotalValue().ToString("c") %>
        </span>
        <%= Html.ActionLink("Check out", "Index", "Cart", new { returnUrl=Request.Url.PathAndQuery }, null) %>
    </div>
<% } %>
